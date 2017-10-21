using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    public class MozaicPanel
    {
        public IList<Mozaic> MozaicList;
        public OriginalImage Image { set; get; } // источник картинки



        public MozaicPanel()
        {
            MozaicList = new List<Mozaic>();

        }
        //матрица
        public int MatrixLines { get; set; } //высота матрицы, в мозаиках
        public int MatrixColumns { get; set; } //ширина матрицы, в мозаиках
        public double DesiredMozaicGap { set; get; } // ширина зазора между мозаиками в матрице, см

        //желаемые размеры
        public double DesiredWidth { set; get; }   //желаемая ширина, см
        public double DesiredHeight { set; get; } //желаем высота, см


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
