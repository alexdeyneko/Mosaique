using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    class MosaicPanel
    {
        public string SourceImg { set; get; } // источник картинки
        public string SourceMosaicSet { set; get; } //источник на файл с расширение lst

        public Matrix PanelMatrix { set; get; }

        public int DeciredWidth { set; get; }   //желаемая ширина см
        public int DeciredHeight { set; get; } //желаем высота см

        //реальные размеры
        public int RealWidth { set; get; }  
        public int RealHeight { set; get; }
        public int CountColumns { set; get; } // количество столбцов
        public int CountLines { set; get; } // количество строк
    }
}
