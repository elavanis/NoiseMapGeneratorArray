using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseMapGeneratorArray
{
    public static class GenerationTimes
    {
        private static DateTime start;
        private static int count;


        public static DateTime Start
        {
            get
            {
                return start;
            }
        }

        public static int Count
        {
            get
            {
                return count;
            }
        }

        public static void Add(DateTime dt)
        {
            count++;
        }


        public static void Reset()
        {
            start = DateTime.Now;
            count = 0;
        }
    }
}
