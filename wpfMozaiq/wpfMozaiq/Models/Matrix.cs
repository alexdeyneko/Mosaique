using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    public class Matrix
    {
        public int[,] mozaics;
        public Matrix(int x, int y)
        {
            mozaics = new int[x, y];
        }

        //public int X { get; set; }
        //public int Y { get; set; }
    }
}
