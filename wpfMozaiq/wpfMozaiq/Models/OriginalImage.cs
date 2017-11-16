using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    public class OriginalImage
    {
        public OriginalImage(string fullPath)
        {
            if (System.IO.File.Exists(fullPath) && (Path.GetExtension(fullPath) == ".bmp"))

            {
                Picture = new Bitmap(fullPath);
                SourcePath = fullPath;
            }

        }
        public Bitmap Picture { get; set; }
        public string SourcePath { get; set; }

        public void Resize(int width, int height)
        {
            Picture = new Bitmap(Picture, width, height);
        }
    }
}
