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

                   mozaicGrid[i, j] = //Panno.Catalog.Mozaics.First();
                        GenerateForOne(Panno.Catalog, Panno.Grid[i, j]);
                    //Panno.Catalog.Mozaics.Where(n => n.Name == mozaicGrid[i, j].Name).First().CountInPanno++;
                    mozaicGrid[i, j].CountInPanno++;
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




    }
}
