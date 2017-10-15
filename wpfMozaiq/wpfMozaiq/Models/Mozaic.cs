using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace wpfMozaiq.Models
{
    class Mozaic
    {
        public Mozaic(string name, string path)
        {
            try
            {
                Name = name;
                Picture = new Bitmap(path + "\\" + name);
            }
            catch
            {
            }
        }

        public Bitmap Picture { get; set; }
        public string Name { set; get; }

    }
}
