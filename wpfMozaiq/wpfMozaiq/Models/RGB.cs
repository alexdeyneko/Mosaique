using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    public class RGB
    {
        public RGB(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
    }
}
