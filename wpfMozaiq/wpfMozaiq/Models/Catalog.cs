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
        public IList<Mozaic> Mozaics;
        public int MozaicRealSize;
        public string ParentName;
        public string Name;
        public string CatalogPath;


        public Catalog(string parentName, int size, string name)
        {
            ParentName = parentName;
            Name = name;
            MozaicRealSize = size;
            CatalogPath =

               Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
            +
                "\\Catalog\\" + ParentName + "_" + MozaicRealSize + "\\" + Name;
            MozaicRealSize = size;
            Mozaics = new List<Mozaic>();
            this.ImportCatalog();
        }
        public void EnableMozaic(string name)
        {
            try
            {
                Mozaic temp = new Mozaic(name, CatalogPath);
                temp.CalculateAvrColors();
                Mozaics.Add(temp);
            }
            catch { }
        }
        private void ImportCatalog()
        {

            try
            {
                foreach (var item in Directory.GetFiles(CatalogPath, "*.bmp"))
                {
                    this.EnableMozaic(Path.GetFileName(item));

                }
            }
            catch { }
        }
        public void DisableMozaic(string name)
        {
            try
            {
                Mozaics.Remove(Mozaics
                    .Where(n => n.Name == name)
                    .First());
            }
            catch { }
        }
    }
}
