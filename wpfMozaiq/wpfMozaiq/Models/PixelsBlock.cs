using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace wpfMozaiq.Models
{
    public class PixelsBlock : ICalculateAvrRGB
    {

        public Bitmap Picture { get; set; }
        public RGB AverageColors { get; set; }

        public void CalculateAvrColors()
        {
            double red = 0;
            double green = 0;
            double blue = 0;

            for (int i = 0; i < Picture.Height; i++)
            {
                for (int j = 0; j < Picture.Width; j++)
                {
                    red += Picture.GetPixel(i, j).R;
                    green += Picture.GetPixel(i, j).G;
                    blue += Picture.GetPixel(i, j).B;
                }
            }

            AverageColors = new RGB(
                red / Picture.Width / Picture.Height,
                green / Picture.Width / Picture.Height,
                blue / Picture.Width / Picture.Height);

        }


    }
}
