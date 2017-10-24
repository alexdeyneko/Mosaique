using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models.Services
{
    public class MatrixSeparateService
    {
        public MozaicPanel Panno { get; set; }
        public MatrixSeparateService(MozaicPanel panno)
        {
            Panno = panno;
        }
        public void GenerateMatrixArray()
        {

            Panno.Matrixes = new Matrix[Panno.Grid.GetLength(0) / Panno.MatrixColumns + 1,

                Panno.Grid.GetLength(1) / Panno.MatrixLines + 1];

            for (int i = 0; i < Panno.Matrixes.GetLength(0); i++)
            {
                for (int j = 0; j < Panno.Matrixes.GetLength(1); j++)
                {
                    Panno.Matrixes[i, j] = new Matrix(Panno.MatrixColumns, Panno.MatrixLines);
                }
            }

            for (int i = 0; i < Panno.Grid.GetLength(0); i++)
            {

                for (int j = 0; j < Panno.Grid.GetLength(1); j++)
                {

                    Panno.Matrixes[i / Panno.MatrixColumns, j / Panno.MatrixLines].

                        mozaics[i % Panno.MatrixColumns,

                        j % Panno.MatrixLines]

                        = Panno.Grid[i, j].RatingId;


                }


            }
        }
    }
}
