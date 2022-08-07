using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class Settings
    {
        public string SaveLocation { get; set; } = @"\\10.1.0.5\Vol1\Data\DndMaps";

        public string SettingsDirectory
        {
            get
            {
                return $"{SaveLocation}\\Tile Size {Width}-{Height}\\Zoom {Zoom}\\Seed {Seed}\\{StartX}-{StartY}\\Tiles";
            }
        }
        public decimal StartX { get; set; } = 0;
        public decimal StartY { get; set; } = 0;
        public decimal MaxX { get; set; } = 100;
        public decimal MaxY { get; set; } = 100;

        public int Width { get; set; } = 1000;
        public int Height { get; set; } = 1000;
        public int Seed { get; set; } = 2;
        public decimal Zoom { get; set; } = 10000;
        public int HeightMultiplier = 1000;

        public int TileWidth { get; set; } = 10;
        public int TileHeight { get; set; } = 10;
        public int Iterations { get; set; } = 5000;
        public int IterationsDetailed { get; set; } = 5000;
        public double DetailPower { get; set; } = 1;

        public List<ColorPoint> ColorPointsElevation { get; set; }

        public int NoiseIterations { get; set; } = 0;
        public decimal NoiseZoom { get; set; } = .05m;
        public decimal NoiseOpacity { get; set; } = .075m;
        public List<ColorPoint> ColorPointsNoise { get; set; }

        public Settings()
        {
            ColorPointsElevation = new List<ColorPoint>();
            ColorPointsElevation.Add(new ColorPoint() { Point = 10000, Color = new Color(255, 255, 255) });
            ColorPointsElevation.Add(new ColorPoint() { Point = 1000, Color = new Color(255, 255, 255) });
            ColorPointsElevation.Add(new ColorPoint() { Point = 750, Color = new Color(211, 213, 218) });
            //ColorPointsElevation.Add(new ColorPoint() { Point = 750, Color = Color.FromArgb(168, 138, 102) });
            ColorPointsElevation.Add(new ColorPoint() { Point = 500, Color = new Color(109, 111, 105) });
            ColorPointsElevation.Add(new ColorPoint() { Point = 250, Color = new Color(71, 93, 72) });
            ColorPointsElevation.Add(new ColorPoint() { Point = 1, Color = new Color(43, 78, 44) });
            ColorPointsElevation.Add(new ColorPoint() { Point = 0, Color = new Color(100, 150, 255) });
            ColorPointsElevation.Add(new ColorPoint() { Point = -150, Color = new Color(0, 100, 255) });
            ColorPointsElevation.Add(new ColorPoint() { Point = -1000, Color = new Color(0, 0, 0) });
            ColorPointsElevation.Add(new ColorPoint() { Point = -10000, Color = new Color(0, 0, 0) });

            ColorPointsNoise = new List<ColorPoint>();
            ColorPointsNoise.Add(new ColorPoint() { Point = 10000, Color = new Color(255, 255, 255) });
            ColorPointsNoise.Add(new ColorPoint() { Point = 1, Color = new Color(255, 255, 255) });
            ColorPointsNoise.Add(new ColorPoint() { Point = 0, Color = new Color(0, 0, 0) });
            ColorPointsNoise.Add(new ColorPoint() { Point = -10000, Color = new Color(0, 0, 0) });
        }
    }
}
