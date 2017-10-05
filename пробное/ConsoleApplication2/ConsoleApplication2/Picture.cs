using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace ConsoleApplication2
{
    public class Picture
    {
        private Bitmap pic;
        public string path;
        public Picture(string path)
        {
            this.path = path;
            pic = new Bitmap(path);
        }
        public Bitmap Pic
        {
            get
            {
                return pic;
            }
            set
            {
                if (pic!=null) pic = value;
            }
        }
        /*
        public void ResizePicture()
        {

            pic = new Bitmap(pic, pic.Width / 10, pic.Height / 10);
        }
        public void SaveNewPicture()

        {
            pic.Save("C:\\Users\\Алексей\\Desktop\\mini.jpg");
        }
        */
    }
}
