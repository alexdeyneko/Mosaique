using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    class Mosaic
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public string SourceImg { set; get; }
        public string NamePack { set; get; } //имя папки , набора мозаек

        public int Width { set; get; }
        public int Height { set; get; }
    }
}
