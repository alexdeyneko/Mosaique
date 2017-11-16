using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models
{
    public class MozaicPanel
    {

        public OriginalImage Image { set; get; } // источник картинки

        public Catalog Catalog { set; get; } //каталог мозаик

        public Mozaic[,] Grid;


        public Matrix[,] Matrixes;

        public MozaicPanel(OriginalImage image, Catalog catalog, double desiredWidth,
            int matrixLines, int matrixColumns, double desiredMozaicGap,
            int computerMozaicGap, int computerMatrixGap)
        {
            Image = image;
            Catalog = catalog;
            DesiredWidth = desiredWidth;

            MatrixLines = matrixLines;
            MatrixColumns = matrixColumns;
            DesiredMozaicGap = desiredMozaicGap;
            ComputerMozaicGap = computerMozaicGap;
            ComputerMatrixGap = computerMatrixGap;


        }

        //матрица
        public int MatrixLines { get; set; } //высота матрицы, в мозаиках
        public int MatrixColumns { get; set; } //ширина матрицы, в мозаиках
        public double DesiredMozaicGap { set; get; } // ширина зазора между мозаиками в матрице, мм

        private double desiredWidth;
        private double desiresHeight;

        //желаемые размеры
        public double DesiredWidth
        {
            set
            {
                desiredWidth = value;
                desiresHeight = (int)(Image.Picture.Height * value / Image.Picture.Width);
            }
            get { return desiredWidth; }   //желаемая ширина, см
        }
        public double DesiredHeight
        {
            get { return desiresHeight; }
        }


        //компьютерное изображение
        public int ComputerMozaicGap { set; get; } // ширина зазора между ячейками (в пикс)
        //цвет между мозаиками
        public int ComputerMatrixGap { set; get; } // ширина зазора между матрицами (в пикс)
        //цвет между матрицами


        //реальные размеры
        public double RealWidth { set; get; }  //ширина см
        public double RealHeight { set; get; } //высота см
        public int CountColumns { set; get; } // количество столбцов
        public int CountLines { set; get; } // количество строк
    }
}
