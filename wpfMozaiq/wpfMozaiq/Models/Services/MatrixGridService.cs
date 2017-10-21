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
        public OriginalImage Image;
        public int MozaicSize;
        public MozaicPanel Panno;
        public PixelsBlock[,] Grid;

        public MatrixGridService(OriginalImage image, int size, MozaicPanel panno)
        {
            Image = image;
            MozaicSize = size;
            Panno = panno;


        }

        public int CalculateImageHeight()   //высота изображения в пикселях
        {
            return Convert.ToInt32(Math.Truncate(Panno.DesiredHeight * 10 / (MozaicSize + Panno.DesiredMozaicGap)));
        }
        public int CalculateImageWidth()    //ширина изображения в пикселях
        {
            return Convert.ToInt32(Math.Truncate(
                    Panno.DesiredWidth * 10 / (MozaicSize + Panno.DesiredMozaicGap)));
        }

        public void CreateImageGrid()
        {
            int height = CalculateImageHeight();        //кол-во блоков в высоту
            int width = CalculateImageWidth();          //кол-во блоков в ширину
            int pixelHeight = Image.Picture.Height / height;   //высота одного блока в пикселях
            int pixelWidth = Image.Picture.Width / width;      //ширина одного блока в пикселях
            Grid = new PixelsBlock[width, height];


            //Bitmap newMap = new Bitmap(width*pixelWidth,height*pixelHeight);
            //Graphics g = Graphics.FromImage(newMap);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    PixelsBlock block = new PixelsBlock();
                    block.Picture = Image.Picture.Clone(new Rectangle

                        (i * pixelWidth,
                                   j * pixelHeight,
                       pixelWidth, pixelHeight),

                       Image.Picture.PixelFormat);

                    block.CalculateAvrColors();

                    Grid[i, j] = block;

                    //g.DrawImage(block.Picture, i * pixelWidth, j * pixelHeight, pixelWidth, pixelHeight);

                }

            }
            //g.Dispose();

            //newMap.Save("C:\\Users\\Алексей\\Desktop\\пам5.bmp");

        }
    }
}
