using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapGenerator
{
    public class Serialization
    {
        public double[,] Deserialize(string filePath, int x, int y)
        {
            byte[] data = File.ReadAllBytes(filePath);

            double[,] array = ByteToDoubleArray(data, x, y);
            return array;
        }

        public void Serialize(FileStream fs, double[,] values)
        {
            byte[] bytes = DoubleToByteArray(values);

            fs.Write(bytes);
        }

        private byte[] DoubleToByteArray(double[,] values)
        {
            byte[] byteArray = new byte[values.Length * System.Runtime.InteropServices.Marshal.SizeOf(typeof(double))];
            Buffer.BlockCopy(values, 0, byteArray, 0, byteArray.Length);

            return byteArray;
        }

        private double[,] ByteToDoubleArray(byte[] bytes, int x, int y)
        {
            double[,] array = new double[x, y];
            Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);

            return array;
        }
    }
}