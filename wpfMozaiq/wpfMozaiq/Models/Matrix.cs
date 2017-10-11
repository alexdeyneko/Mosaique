using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    class Matrix
    {
        public int CountColumns { set; get; } // количество столбцов
        public int CountLines { set; get; } // количество строк
        public double GapWidth { set; get; } // ширина зазора в мм
    }
}
