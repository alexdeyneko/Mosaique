using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMozaiq.Interfaces;
namespace wpfMozaiq.Models.Services
{
    public class MozaicSelectService : ISelectMozaic
    {

        public MozaicPanel Panno { get; set; }
        public MozaicSelectService(MozaicPanel panno)
        {
            Panno = panno;
        }
        public void GenerateForGrid()
        {
            Mozaic[,] mozaicGrid = new Mozaic[Panno.Grid.GetLength(0), Panno.Grid.GetLength(1)];

            for (int i = 0; i < Panno.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Panno.Grid.GetLength(1); j++)
                {

                    mozaicGrid[i, j] = GenerateForOne(Panno.Catalog, Panno.Grid[i, j]);
                    Panno.Catalog.Mozaics.Where(n => n == mozaicGrid[i, j]).First().CountInPanno++;
                }


            }
            Panno.Grid = mozaicGrid;
        }
        public Mozaic GenerateForOne(Catalog catalog, PixelsBlock block)
        {
            double maxDelta = 255;
            Mozaic bestChoice = new Mozaic(catalog.Mozaics.First().Name, catalog.Mozaics.First().SubCatalog, catalog.CatalogPath);//первая мозаика в каталоге по умолчанию
            foreach (var thing in catalog.Mozaics)
            {
                double delta = Math.Sqrt(Math.Pow(block.AverageColors.Red - thing.AverageColors.Red, 2) +
                    Math.Pow(block.AverageColors.Green - thing.AverageColors.Green, 2) +
                    Math.Pow(block.AverageColors.Blue - thing.AverageColors.Blue, 2));

                if (delta < maxDelta)
                {
                    bestChoice = thing;
                    maxDelta = delta;
                }
            }

            return bestChoice;
        }

        //временно тут
        public void GeneratePicture()
        {
            int width = Panno.Grid[0, 0].Picture.Width;
            int height = Panno.Grid[0, 0].Picture.Height;
            Bitmap newMap = new Bitmap(Panno.Grid.GetLength(0) * width, Panno.Grid.GetLength(1) * height);
            Graphics g = Graphics.FromImage(newMap);

            for (int i = 0; i < Panno.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Panno.Grid.GetLength(1); j++)
                {
                    g.DrawImage(Panno.Grid[i, j].Picture, i * width, j * height, width, height); //просто посмотреть


                }

            }
            g.Dispose();

            newMap.Save("C:\\Users\\Алексей\\Desktop\\тестирую" + new Random().Next() + ".bmp");
        }



    }
}
