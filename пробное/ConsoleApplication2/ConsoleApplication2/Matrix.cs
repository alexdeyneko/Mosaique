using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Matrix
    {
        private Picture pic;
        public Picture Pic
        {
            get { return pic; }
            set
            {
                if (value != null) pic = value;
            }
        }
        public RGB[][] colorMatrix;


        public Matrix(Picture pic)
        {
            this.pic = pic;
        }


        public void GetBitMapPictureColorMatrix()
        {

            int hight = pic.Pic.Height;
            int width = pic.Pic.Width;

            colorMatrix = new RGB[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new RGB[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = new RGB();
                    colorMatrix[i][j].r = pic.Pic.GetPixel(i, j).R;
                    colorMatrix[i][j].b = pic.Pic.GetPixel(i, j).B;
                    colorMatrix[i][j].g = pic.Pic.GetPixel(i, j).G;


                    //= Math.Sqrt(pic.Pic.GetPixel(i, j).G ^ 2 + pic.Pic.GetPixel(i, j).R ^ 2 + pic.Pic.GetPixel(i, j).B ^ 2);

                }
            }
        }


    }
}
