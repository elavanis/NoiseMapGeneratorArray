using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MapGenerator
{

    public class MapArray 
    {
        public double[,]? Generate(Settings settings, BackgroundWorker bgw)
        {
            double[,] array = new double[settings.Width, settings.Height];

            List<OpenSimplexNoise> listNoise = new List<OpenSimplexNoise>();
            for (int i = 0; i < settings.IterationsDetailed; i++)
            {
                listNoise.Add(new OpenSimplexNoise(i + settings.Seed));
            }

#if DEBUG
            for (int x = 0; x < settings.Width; x++)
            {
                for (int y = 0; y < settings.Height; y++)
                {
                    GenerateArryData(settings, array, x, y, listNoise);
                }
                bgw.ReportProgress((x * 100) / settings.Width, "1");
            }

#endif
#if !DEBUG
            int xPos = 0;
            Parallel.For(0, settings.Width, x =>
            {
                Parallel.For(0, settings.Height, y =>
                {
                    GenerateArryData(settings, array, x, y, listNoise);
                });
                bgw.ReportProgress((Interlocked.Increment(ref xPos) * 100) / settings.Width, "1");
            });
            
#endif
                //bgw.ReportProgress((x * 100) / settings.Width, "1");



            if (Run.ContinueRunning)
            {
                return array;
            }
            else
            {
                return null;
            }
        }

        private void GenerateArryData(Settings settings, double[,] array, int x, int y, List<OpenSimplexNoise> listNoise)
        {
            double height = 0;

            decimal xPos = x / settings.Zoom + settings.StartX;
            decimal yPos = y / settings.Zoom + settings.StartY;

            double doubleXPos = ((double)xPos);
            double doubleYPos = ((double)yPos);

            for (int loopPostion = 0; loopPostion < settings.Iterations; loopPostion++)
            {
                OpenSimplexNoise noise = listNoise[loopPostion];
                double effectLayer = loopPostion + 1;
                height += (1 / effectLayer) * (noise.Evaluate(doubleXPos * effectLayer, doubleYPos * effectLayer));
            }

            double tempHeight = height;
            tempHeight = Math.Abs(tempHeight);
            tempHeight = Math.Max(0, tempHeight);
            tempHeight = Math.Min(1, tempHeight);
            double percent = 1 - tempHeight;
            int additionalLoops = (int)(settings.IterationsDetailed * Math.Pow(percent, settings.DetailPower));

            for (int loopPostion = settings.Iterations; loopPostion < additionalLoops; loopPostion++)
            {
                OpenSimplexNoise noise = listNoise[loopPostion];
                double effectLayer = loopPostion + 1;
                height += (1 / effectLayer) * (noise.Evaluate(doubleXPos * effectLayer, doubleYPos * effectLayer));
            }

            array[x, y] = height;
        }
    }
}
