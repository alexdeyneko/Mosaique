using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMozaiq.Interfaces;

namespace wpfMozaiq.Models.Services
{
    public class TxtTechDocWriter : ITechDocWriter
    {
        public MozaicPanel Panno { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string FullPath { get; set; }
        public TxtTechDocWriter(MozaicPanel panno, string path)
        {
            Panno = panno;
            FullPath = path;
            FileName = Path.GetFileName(path);
            FilePath = Directory.GetParent(path).FullName;
        }


        public void WriteDocumentation()
        {

            using (var sw = new StreamWriter(FullPath, false))
            {
                sw.WriteLine("Техническое задание на сборку мозаичной картинки {0}", FileName.Split('.')[0]);
                sw.WriteLine("");
                sw.Close();
            }
            WriteHeader();
            WriteCatalogRating();
            WriteMatrixes();

        }
        private void WriteHeader()
        {
            using (var sw = new StreamWriter(FullPath, true))
            {
                sw.WriteLine("Исходное изображение: {0}", Panno.Image.SourcePath);
               
                sw.WriteLine("Размер ячеек, мм: {0}", Panno.Catalog.MozaicRealSize);
                sw.WriteLine("Высота панно, см: {0}", Panno.RealHeight);
                sw.WriteLine("Ширина панно, мм: {0}", Panno.RealWidth);

                sw.WriteLine("Зазор между ячейками реальный, мм: {0}", Panno.DesiredMozaicGap);
                sw.WriteLine("Зазор между ячейками компьютерный, мм: {0}", Panno.ComputerMozaicGap);
                sw.WriteLine("Зазор между матрицами компьютерный, мм: {0}", Panno.ComputerMatrixGap);
                sw.WriteLine("Высота матрицы, мозаик: {0}", Panno.MatrixLines);
                sw.WriteLine("Ширина матрицы, мозаик: {0}", Panno.MatrixColumns);

                sw.WriteLine("===============================================");
                sw.WriteLine("");
                //sw.WriteLine("Размер ячеек, мм: {0}", Panno.Catalog.MozaicRealSize);
                sw.Close();
            }

        }
        private void WriteCatalogRating()
        {
            using (var sw = new StreamWriter(FullPath, true))
            {
                sw.WriteLine("Каталог: {0}", Panno.Catalog.Name);
                foreach (var item in Panno.Catalog.Mozaics)
                {
                    if (item.RatingId != 0)
                        sw.WriteLine("{0}. {1}/{2} ({3} шт.)", item.RatingId, item.SubCatalog, item.Name, item.CountInPanno);
                }
                sw.WriteLine("===============================================");
                sw.WriteLine("");

                sw.Close();
            }
        }

        private void WriteMatrixes()
        {
            using (var sw = new StreamWriter(FullPath, true))
            {
                sw.WriteLine("Матрицы: {0} [{1},{2}]",
                    Panno.Matrixes.GetLength(0) * Panno.Matrixes.GetLength(1),
                    Panno.MatrixLines,
                    Panno.MatrixColumns);
                sw.WriteLine("");

                for (int i = 0; i < Panno.Matrixes.GetLength(0); i++)
                {
                    for (int j = 0; j < Panno.Matrixes.GetLength(1); j++)
                    {
                        sw.WriteLine("[{0},{1}]", j, i);

                        WriteMatrix(sw, Panno.Matrixes[i, j]);
                        sw.WriteLine("");
                    }
                    sw.WriteLine("");
                }



                sw.Close();
            }
        }
        private void WriteMatrix(StreamWriter sw, Matrix matrix)
        {

            for (int i = 0; i < matrix.mozaics.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.mozaics.GetLength(0); j++)
                {
                    if (matrix.mozaics[j, i] != 0)
                    {
                        sw.Write(matrix.mozaics[j, i]);
                        sw.Write("\t");
                    }

                }
                sw.WriteLine("");
            }

        }


    }
}
