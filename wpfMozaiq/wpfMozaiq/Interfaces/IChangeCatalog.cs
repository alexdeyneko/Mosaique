using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Interfaces
{
    interface IChangeCatalog
    {
        void AddMozaic(Bitmap image, string mozaicName);
        void DeleteMozaic(string mozaicName);
        void AddCatalog();
        void DeleteCatalog();
    }
}
