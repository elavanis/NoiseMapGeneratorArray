using MapGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NoiseMapGeneratorArray
{
    public class ArrayPainter
    {
        public void Generate(BackgroundWorker backgroundWorker, Settings settings)
        {
            ImageBuilder imageBuilder = new ImageBuilder(); 
            Serialization serialization = new Serialization();
            MapColor color = new MapColor(settings.HeightMultiplier, settings.ColorPointsElevation);

            string settingsDir = $@"{settings.SaveLocation}\Tile Size {settings.Width}-{settings.Height}\Zoom {settings.Zoom}\Seed {settings.Seed}\";
            string[] array = Directory.GetFiles(settingsDir, "*.array", SearchOption.AllDirectories);

            List<string> sortedList = array.ToList();
            sortedList .Sort();
            array = sortedList.ToArray();

            int pos = 0;
#if DEBUG
            foreach (string file in array)
            {
                imageBuilder.BuildImage(file, settings, color, serialization);
                BuildImage(file, settings, color, serialization);
                backgroundWorker.ReportProgress((pos++ * 100) / array.Length, 1);
            }
#endif
#if !DEBUG

            Parallel.ForEach(array, (file) =>
            {
                imageBuilder.BuildImage(file, settings, color, serialization);
                backgroundWorker.ReportProgress((Interlocked.Increment(ref pos) * 100) / array.Length, 1);

            });
#endif
        }

        private void BuildImage(string file, Settings settings, MapColor color, Serialization serialization)
        {
            string fileName = Path.Combine(Path.GetDirectoryName(file)!, Path.GetFileNameWithoutExtension(file) + ".png");
            if (!File.Exists(fileName))
            {
                double[,] array = serialization.Deserialize(file, settings.Width, settings.Height);

                Bitmap bmp = new Bitmap(array.GetLength(0), array.GetLength(1));

                for (int x = 0; x < array.GetLength(0); x++)
                {
                    for (int y = 0; y < array.GetLength(1); y++)
                    {
                        MapGenerator.Color pixelcolor = color.GetColor(array[x, y]);
                        bmp.SetPixel(x, y, System.Drawing.Color.FromArgb(pixelcolor.Red, pixelcolor.Green, pixelcolor.Blue));
                    }
                }

                bmp.Save(fileName);
                bmp.Dispose();
            }
        }
    }
}
