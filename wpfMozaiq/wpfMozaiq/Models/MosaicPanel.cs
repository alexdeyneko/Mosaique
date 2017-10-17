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
        

        public List <Matrix> PanelMatrix { set; get; }

        //матрица
        public int MatrixLines { get; set; } //высота матрицы, в мозаиках
        public int MatrixColumns { get; set; } //ширина матрицы, в мозаиках
        public double DeciredMozaicGap { set; get; } // ширина зазора между мозаиками в матрице, см

        //желаемые размеры
        public double DeciredWidth { set; get; }   //желаемая ширина, см
        public double DeciredHeight { set; get; } //желаем высота, см
    

        //компьютерное изображение
        public double ComputerMozaicGap { set; get; } // ширина зазора между ячейками (в пикс)
        //цвет между мозаиками
        public double ComputerMatrixGap { set; get; } // ширина зазора между матрицами (в пикс)
        //цвет между матрицами


        //реальные размеры
        public double RealWidth { set; get; }  //ширина см
        public double RealHeight { set; get; } //высота см
        public int CountColumns { set; get; } // количество столбцов
        public int CountLines { set; get; } // количество строк
    }
}
