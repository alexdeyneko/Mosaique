using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMozaiq.Interfaces;

namespace wpfMozaiq.Models.Services
{
    public class CatalogChangeService : IChangeCatalog
    {


        private string _pathToMozaic;
        private int _pictureSize;
        public CatalogChangeService(string fullCatalogName, string subCatalogName)
        {

            if (subCatalogName != "")
                subCatalogName = "\\" + subCatalogName;

            _pathToMozaic = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
            + "\\Catalog\\" + fullCatalogName + subCatalogName;

            _pictureSize = Convert.ToInt32(fullCatalogName.Split('_')[1]) * 4;
        }

        public void AddMozaic(Bitmap image, string mozaicName)
        {

            int size;
            if (image.Width > image.Height)
                size = image.Height;
            else size = image.Width;

            new Bitmap(
                image.Clone(new Rectangle(0, 0, size, size), image.PixelFormat)
                ,
                _pictureSize, _pictureSize).Save(_pathToMozaic + "\\" + mozaicName);

        }
        public void DeleteMozaic(string mozaicName)
        {

            File.Delete(_pathToMozaic + "\\" + mozaicName);
        }

        public void AddCatalog()
        {
            Directory.CreateDirectory(_pathToMozaic);
        }
        public void DeleteCatalog()
        {
            Directory.Delete(_pathToMozaic, true);
        }
    }
}
