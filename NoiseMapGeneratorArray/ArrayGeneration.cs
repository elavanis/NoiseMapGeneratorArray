using MapGenerator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoiseMapGeneratorArray
{
    public class ArrayGeneration
    {
        public void Generate(BackgroundWorker backgroundWorker, Settings settings)
        {
            Serialization serialization = new Serialization();
            MapArray mapArray = new MapArray();

            List<Tuple<int, int>> workToDo = GenerateWork(settings);
            int total = workToDo.Count;
            GenerationTimes.Reset();

            while (workToDo.Count > 0)
            {
                Tuple<int, int> t = workToDo[0];
                workToDo.RemoveAt(0);
                settings.StartX = t.Item1 * (settings.Width / settings.Zoom);
                settings.StartY = t.Item2 * (settings.Height / settings.Zoom);


                Directory.CreateDirectory(settings.SettingsDirectory);
                string fileName = Path.Combine(settings.SettingsDirectory, string.Format("{0}-{1}.array", settings.StartX, settings.StartY));

                if (!File.Exists(fileName))
                {
                    SaveArray(settings, mapArray.Generate(settings, backgroundWorker), fileName, serialization);
                }

                backgroundWorker.ReportProgress(total - workToDo.Count, 2);
            }
        }

        private List<Tuple<int, int>> GenerateWork(Settings settings)
        {
            List<Tuple<int, int>> workToDo = new List<Tuple<int, int>>();

            for (int x = 0; x < settings.MaxX; x++)
            {
                for (int y = 0; y < settings.MaxY; y++)
                {
                    workToDo.Add(new Tuple<int, int>(x, y));
                }
            }

            Random rnd = new Random();
            int pos = workToDo.Count;
            while (pos > 1)
            {
                pos--;
                int swapPos = rnd.Next(pos + 1);
                Tuple<int, int> value = workToDo[swapPos];
                workToDo[swapPos] = workToDo[pos];
                workToDo[pos] = value;
            }
            return workToDo;
        }


        private ConcurrentQueue<Task> queue = new ConcurrentQueue<Task>();

        private void SaveArray(Settings settings, double[,]? result, string fileName, Serialization serialization)
        {
            int maxQueueLength = 10;
            while (queue.Count > maxQueueLength)
            {
                if (queue.Count > maxQueueLength)
                {
                    lock (queue)
                    {
                        while (queue.Count > maxQueueLength)
                        {
                            Task? task;
                            queue.TryPeek(out task);
                            if (task != null &&  task.IsCompleted)
                            {
                                queue.TryDequeue(out task);
                            }
                            else
                            {
                                Thread.Sleep(1);
                            }
                        }
                    }
                }
            }

            Task t = new Task(() => Write(fileName, result, serialization));
            t.Start();
            lock (queue)
            {
                queue.Enqueue(t);
            }
        }

        private void Write(string fileName, double[,]? result, Serialization serialization)
        {
            if (result != null)
            {
                using (FileStream fs = File.Create(fileName, 60000))
                {
                    serialization.Serialize(fs, result);
                }
            }
        }
    }
}
