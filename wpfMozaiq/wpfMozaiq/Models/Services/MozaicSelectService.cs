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
        public Mozaic[,] GenerateForGrid(Catalog catalog, PixelsBlock[,] grid)
        {
            Mozaic[,] mozaicGrid = new Mozaic[grid.GetLength(0), grid.GetLength(1)];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {

                    mozaicGrid[i, j] = GenerateForOne(catalog, grid[i, j]);

                }


            }
            return mozaicGrid;
        }
        public Mozaic GenerateForOne(Catalog catalog, PixelsBlock block)
        {
            double maxDelta = 255;
            Mozaic bestChoice = new Mozaic(catalog.Mozaics.First().Name, catalog.CatalogPath);//первая мозаика в каталоге по умолчанию
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
        public void GeneratePicture(Mozaic[,] grid)
        {
            Bitmap newMap = new Bitmap(grid.GetLength(0) * grid[0, 0].Picture.Width, grid.GetLength(1) * grid[0, 0].Picture.Height);
            Graphics g = Graphics.FromImage(newMap);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    g.DrawImage(grid[i, j].Picture, i * 40, j * 40, 40, 40); //просто посмотреть


                }

            }
            g.Dispose();

            newMap.Save("C:\\Users\\Алексей\\Desktop\\тестирую2.bmp");
        }



    }
}
