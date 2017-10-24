using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models.Services
{
    public class TechDocGenerateService
    {
        public MozaicPanel Panno { get; set; }

        public TechDocGenerateService(MozaicPanel panno)
        {
            Panno = panno;
        }


        public void GenerateMostUsedMozaics()
        {

            Panno.Catalog.Mozaics = Panno.Catalog.Mozaics
                .OrderByDescending(n => n.CountInPanno).ToList();


            int index = 1;
            foreach (var item in Panno.Catalog.Mozaics)
            {
                if (item.CountInPanno != 0)
                {
                    item.RatingId = index;
                    index++;
                }
            }



        }


        public void ReplaceMosaicNameToID()
        {

            for (int i = 0; i < Panno.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Panno.Grid.GetLength(1); j++)
                {

                    Panno.Grid[i, j].RatingId = Panno.Catalog.Mozaics
                        .Where(n => n.Name == Panno.Grid[i, j].Name)
                        .First()
                        .RatingId;
                }
            }

        }
    }
}
