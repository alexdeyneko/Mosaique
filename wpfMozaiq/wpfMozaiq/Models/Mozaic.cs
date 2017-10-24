using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace wpfMozaiq.Models
{
    public class Mozaic : PixelsBlock
    {

        public Mozaic(string name, string subCatalog, string pathToSubCatalog)
        {
            Picture = new Bitmap(pathToSubCatalog + "\\" + subCatalog + "\\" + name);
            Name = name;
            SubCatalog = subCatalog;
            CountInPanno = 0;

        }

        public Mozaic()
        {
        }
        public string Name { set; get; }
        public string SubCatalog { set; get; }
        public int RatingId { set; get; }
        public int CountInPanno { set; get; }
    }
}
