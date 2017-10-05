using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Mozaic
    {
       

        public Mozaic(string path)
        {
            image = new Bitmap(path);
            file = path;
            //avrСolor = 0.0;
            CalculateAVR();
        }


        public string file;
        public RGB color=new RGB();
        private Bitmap image;
        public Bitmap Image
        {
            get
            {
                return image;
            }
            set
            {
                if (value != null) image = value;
            }
        }
        public void CalculateAVR()
        {
            int red=0;
            int green=0;
            int blue=0;
            int count=0;
            for (int i=0;i<image.Width;i++)
            {
                for (int j = 0; j < image.Height; j++)
                {

                    red+=image.GetPixel(i, j).R;
                    blue += image.GetPixel(i, j).B;
                    green += image.GetPixel(i, j).G;
                    count++;

                }
              
            }
            color.g = green / count;
            color.r = red / count;
            color.b = blue / count;
            //avrСolor = Math.Sqrt(green ^ 2 + red ^ 2 + blue ^ 2);

        }

    }
}
