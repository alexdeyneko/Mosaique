using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    public class Catalog
    {
        private IList<Mozaic> Mozaics;
        public int MozaicRealSize;
        public string ParentName;
        public string Name;


        public Catalog(string parentName, int size, string name)
        {
            ParentName = parentName;
            Name = name;
            MozaicRealSize = size;
            Mozaics = new List<Mozaic>();
            this.importCatalog();
        }

        private void importCatalog()
        {
            string catalogPath =

               Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
            +
                "\\Catalog\\" + ParentName + "_" + MozaicRealSize + "\\" + Name;

            //"C:\\Users\\Алексей\\Documents\\Visual Studio 2015\\Projects\\ConsoleApplication3\\ConsoleApplication3\\Catalog\\Bisazza_10\\ORO";

            foreach (var item in Directory.GetFiles(catalogPath))
            {
                Mozaics.Add(new Mozaic(Path.GetFileName(item), catalogPath));

            }
        }
    }
}
