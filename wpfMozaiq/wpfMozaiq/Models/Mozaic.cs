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
            if (subCatalog == "")
            {
                Picture = new Bitmap(pathToSubCatalog + "\\" + name);
            }
            else Picture = new Bitmap(pathToSubCatalog + "\\" + subCatalog + "\\" + name);
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
        public Bitmap GetSmall()
        {
            Bitmap bmp = new Bitmap(Picture.Width / 10, Picture.Height / 10);

            Graphics g = Graphics.FromImage(bmp);
            g.DrawImage(Picture
                       , 0, 0,
                       bmp.Width, bmp.Height);

            return bmp;
        }
    }
}
