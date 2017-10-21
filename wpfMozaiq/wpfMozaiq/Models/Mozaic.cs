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

        public Mozaic(string name, string path)
        {
            Picture = new Bitmap(path + "\\" + name);
            Name = name;

        }


        public string Name { set; get; }


    }
}
