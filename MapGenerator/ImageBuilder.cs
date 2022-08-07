using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class ImageBuilder
    {
        public void BuildImage(string file, Settings settings, MapColor color, Serialization serialization)
        {
            string fileName = Path.Combine(Path.GetDirectoryName(file)!, Path.GetFileNameWithoutExtension(file) + ".png");
            if (!File.Exists(fileName))
            {
                double[,] array = serialization.Deserialize(file, settings.Width, settings.Height);


                Image<Rgb24> image = new Image<Rgb24>(array.GetLength(0), array.GetLength(1));
                image.ProcessPixelRows(pixelRowAccessor =>
                {
                    for (int y = 0; y < array.GetLength(1); y++)
                    {
                        Span<Rgb24> pixelRow = pixelRowAccessor.GetRowSpan(y);

                        for (int x = 0; x < array.GetLength(0); x++)
                        {
                            Color pixelColor = color.GetColor(array[x, y]);
                            Rgb24 rgb24 = new Rgb24(pixelColor.Red, pixelColor.Green, pixelColor.Blue);
                            pixelRow[x] = rgb24;
                        }
                    }
                });

                image.SaveAsPng(fileName);
                image.Dispose();
            }
        }
    }
}
