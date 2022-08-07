﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class Color
    {
        public byte Red { get; set; }
        public byte Blue { get; set; }
        public byte Green { get; set; }

        public Color(int red, int blue, int green)
        {
            Red = Convert.ToByte(red);
            Blue = Convert.ToByte(blue);
            Green = Convert.ToByte(green);
        }
    }
}
