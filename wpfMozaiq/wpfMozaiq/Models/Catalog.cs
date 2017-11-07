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
        public string Name;
        public string CatalogPath;


        public Catalog(string name, int size)
        {

            Name = name;
            MozaicRealSize = size;
            CatalogPath =

               Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
            +
                "\\Catalog\\" + Name + "_" + MozaicRealSize;
            MozaicRealSize = size;
            Mozaics = new List<Mozaic>();
            this.ImportCatalog();
        }
        public Catalog(string name, int size, List<Mozaic> mozaics)
        {
            Name = name;
            MozaicRealSize = size;
            Mozaics = mozaics;
        }

        public void EnableMozaic(string name, string subCatalog)
        {
            try
            {
                Mozaic temp = new Mozaic(name, subCatalog, CatalogPath);
                temp.CalculateAvrColors();
                if ((temp.Picture.Width == MozaicRealSize * 4) &&
                    (temp.Picture.Height == MozaicRealSize * 4)
                    )
                    Mozaics.Add(temp);

            }
            catch { }
        }

        private void ImportCatalog()
        {

            try
            {
                foreach (var directory in Directory.GetDirectories(CatalogPath))
                {
                    foreach (var file in Directory.GetFiles(directory))
                    {

                        this.EnableMozaic(Path.GetFileName(file), Path.GetFileName(directory));
                    }

                }
                foreach (var file in Directory.GetFiles(CatalogPath))
                {
                    this.EnableMozaic(Path.GetFileName(file), "");
                }
            }
            catch { }
        }
        public void DisableMozaic(string name, string subCatalog)
        {
            try
            {
                Mozaics.Remove(Mozaics
                    .Where(n => n.Name == name).Where(n => n.SubCatalog == subCatalog)
                    .First());
            }
            catch { }
        }
    }
}
