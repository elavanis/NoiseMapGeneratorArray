using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class MapColor
    {
        private int _heightMultiplier;
        private List<ColorPoint> _colorPoints;

        public MapColor(int heightMultiplier, List<ColorPoint> colorPoints)
        {
            _heightMultiplier = heightMultiplier;
            _colorPoints = colorPoints;
        }



        public Color GetColor(double elevation)
        {
            ColorPoint Above = null!;
            ColorPoint Below = null!;

            elevation *= _heightMultiplier;


            foreach (ColorPoint cp in _colorPoints)
            {
                if (cp.Point < elevation)
                {
                    if (Below == null
                        || cp.Point > Below.Point)
                    {
                        Below = cp;
                    }
                }
                else if (cp.Point >= elevation)
                {
                    if (Above == null
                        || cp.Point <= Above.Point)
                    {
                        Above = cp;
                    }
                }
            }

            Color blendedColor = MegaCalculateColors(elevation, Above!, Below!);
            return blendedColor;
        }

        private Color MegaCalculateColors(double elevation, ColorPoint above, ColorPoint below)
        {
            return MegaCalculateColors(elevation, above.Point, below.Point, above.Color, below.Color);
        }

        private Color MegaCalculateColors(double elevation, int startRange, int endRange, Color startColor, Color endColor)
        {
            int heightRange = CalculateRange(startRange, endRange);
            double percentThroughRange = CalculateThroughRange(startRange, heightRange, elevation);
            Color blendedColor = CalculateColorBlend(startColor, endColor, percentThroughRange);
            return blendedColor;
        }

        private int CalculateRange(int firstElevation, int secondElevation)
        {
            return Math.Abs(firstElevation - secondElevation);
        }

        private double CalculateThroughRange(int startElevation, int range, double elevation)
        {
            double valueThrough = elevation - startElevation;

            return Math.Abs(valueThrough / range);
        }

        private Color CalculateColorBlend(Color startColor, Color endColor, double percentThroughRange)
        {
            int redDifference = (int)((endColor.Red - startColor.Red) * percentThroughRange);
            int greenDifference = (int)((endColor.Green - startColor.Green) * percentThroughRange);
            int blueDifference = (int)((endColor.Blue - startColor.Blue) * percentThroughRange);

            Color c = new Color(startColor.Red + redDifference, startColor.Green + greenDifference, startColor.Blue + blueDifference);
            return c;
        }

    }
}
