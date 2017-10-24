using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMozaiq.Interfaces;
using wpfMozaiq.Models;

namespace wpfMozaiq.Models.Services
{
    public class MatrixGridService : ICreateImageGrid
    {


        public MozaicPanel Panno;

        public MatrixGridService(MozaicPanel panno)
        {

            Panno = panno;

        }

        public int CalculateOptimalHeight()   //оптимальная высота изображения в блоках
        {
            int height = (int)(Panno.DesiredHeight * 10 / (Panno.Catalog.MozaicRealSize + Panno.DesiredMozaicGap));//максимум блоков

            int pixelHeight = Panno.Image.Picture.Height / height;//высота одного блока в пикселях

            for (; (double)Panno.Image.Picture.Height / height - pixelHeight > 0.1;) //подбираем оптимальную высоту
            {

                pixelHeight = Panno.Image.Picture.Height / height;
                height--;

            }
            Panno.CountLines = height;
            return height;
        }

        public int CalculateOptimalWidth()    //оптимальная ширина изображения в блоках
        {
            int width = (int)(
            Panno.DesiredWidth * 10 / (Panno.Catalog.MozaicRealSize + Panno.DesiredMozaicGap));
            int pixelWidth = Panno.Image.Picture.Width / width;//ширина одного блока в пикселях
            for (; (double)Panno.Image.Picture.Width / width - pixelWidth > 0.1;)//подбираем оптимальную ширину
            {
                pixelWidth = Panno.Image.Picture.Width / width;
                width--;

            }
            Panno.CountColumns = width;

            return width;
        }

        public void CreateImageGrid()
        {
            int height = CalculateOptimalHeight();        //кол-во блоков в высоту
            int width = CalculateOptimalWidth();          //кол-во блоков в ширину
            int pixelHeight = Panno.Image.Picture.Height / height;//высота одного блока в пикселях
            int pixelWidth = Panno.Image.Picture.Width / width;//ширина одного блока в пикселях


            Panno.Grid = new Mozaic[width, height];
            SetRealHight(height);
            SetRealWidth(width);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Mozaic block = new Mozaic();
                    Rectangle section = new Rectangle(i *
                        pixelWidth, j * pixelHeight,
                          pixelWidth,
                       pixelHeight
               );


                    Bitmap bmp = new Bitmap(section.Width, section.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(Panno.Image.Picture, 0, 0, section, GraphicsUnit.Pixel);
                    }


                    block.Picture = bmp;

                    block.CalculateAvrColors();

                    Panno.Grid[i, j] = block;



                }

            }


        }

        public void SetRealWidth(int width)
        {
            Panno.RealWidth = width * (Panno.DesiredMozaicGap + Panno.Catalog.MozaicRealSize) / 10;
        }
        public void SetRealHight(int height)
        {
            Panno.RealHeight = height * (Panno.DesiredMozaicGap + Panno.Catalog.MozaicRealSize) / 10;
        }
    }
}
