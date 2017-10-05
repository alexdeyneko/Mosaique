using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Catalog
    {
        public Catalog()
        {
            mozaiclist = new List<Mozaic>();
            
        }
        public List<Mozaic> mozaiclist;
        public string path = "C:\\Users\\Алексей\\Documents\\ПО_Мозаика\\mosaic\\Bisazza10\\Vetricolor";
        public void inputCatalog()
        {
            string[] fullfilesPath =
            Directory.GetFiles(@path,"*.bmp");
            foreach(var item in fullfilesPath)
            {
                mozaiclist.Add(new Mozaic(item));
                
            }
            
        }

    }
}
