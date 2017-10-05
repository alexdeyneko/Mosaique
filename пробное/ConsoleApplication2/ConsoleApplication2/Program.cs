using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Picture tmp = new Picture("C:\\Users\\Алексей\\Desktop\\пам.jpg");
            //tmp.ResizePicture();
            //tmp.SaveNewPicture()
            Catalog cat = new Catalog();
            cat.inputCatalog();

            Matrix matrix = new Matrix(tmp);
            matrix.GetBitMapPictureColorMatrix();

            new Generator(cat, matrix).Generate();
        }
    }
}
