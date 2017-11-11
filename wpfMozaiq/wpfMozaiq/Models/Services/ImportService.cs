using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace wpfMozaiq.Models.Services
{
    public class ImportService
    {
        public string FilePath { get; set; }

        public ImportService(string filePath)
        {
            FilePath = filePath;
        }


        public MozaicPanel ImportPanno()
        {

            string[] parts = SplitToParts(GetFile());
            MozaicPanel tmp = GetPanno(GetCatalog(parts[1]), parts[0]);
            tmp = GetMatrixes(tmp, parts[2]);
            return tmp;
        }


        private string GetFile()
        {
            using (var reader = new StreamReader(FilePath))
            {
                return reader.ReadToEnd();
            }
        }

        private string[] SplitToParts(string file)
        {
            return file.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
        }


        private Catalog GetCatalog(string catalog)
        {
            List<Mozaic> mozaics = new List<Mozaic>();
            List<string> lines = Regex.Split(catalog, "\r\n")
            .Where(n => n != "").ToList();

            int mozaicRealSize = Convert.ToInt32(lines.First().Split('_')[1]);
            string catalogName =
            Regex.Match(lines.First(), @":(\s*)(\w*)_").Groups[2].Value;

            lines.Remove(lines.First());

            foreach (var item in lines)
            {
                var line = Regex.Match(item, @"(\d*).(\s*)(\w*)/(\S*)(\s*)[(](\w*)");

                Mozaic current = new Mozaic(line.Groups[4].Value,
                    line.Groups[3].Value,
                    Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))
            + "\\Catalog\\" + catalogName + "_" + mozaicRealSize
                    );
                current.RatingId = Convert.ToInt32(line.Groups[1].Value);
                current.CountInPanno = Convert.ToInt32(line.Groups[6].Value);
                //current.CalculateAvrColors();
                mozaics.Add(current);
            }
            return new Catalog(catalogName, mozaicRealSize, mozaics);

        }

        private MozaicPanel GetPanno(Catalog catalog, string header)
        {
            string imagePath =
            Regex.Match(header, @"Исходное изображение:\s*(\S*)").Groups[1].Value;
            double realHeight = Convert.ToDouble(
                Regex.Match(header, @"Высота панно, см:\s*(\S*)").Groups[1].Value);
            double realWidth = Convert.ToDouble(
                Regex.Match(header, @"Ширина панно, см:\s*(\S*)").Groups[1].Value);
            int mozaicGap = Convert.ToInt32(
                Regex.Match(header, @"Зазор между ячейками реальный, мм:\s*(\S*)").Groups[1].Value);
            int computerMozaicGap =
                Convert.ToInt32(
                Regex.Match(header, @"Зазор между ячейками компьютерный, пикс:\s*(\S*)").Groups[1].Value);
            int computerMatrixGap =
               Convert.ToInt32(
               Regex.Match(header, @"Зазор между матрицами компьютерный, пикс:\s*(\S*)").Groups[1].Value);
            int matrixLines =
               Convert.ToInt32(
               Regex.Match(header, @"Высота матрицы, мозаик:\s*(\S*)").Groups[1].Value);
            int matrixColumns =
               Convert.ToInt32(
               Regex.Match(header, @"Ширина матрицы, мозаик:\s*(\S*)").Groups[1].Value);

            return new MozaicPanel(
                new OriginalImage(imagePath),
                catalog,
                realWidth,
                matrixLines,
                matrixColumns,
                mozaicGap,
                computerMozaicGap,
                computerMatrixGap);


        }

        private MozaicPanel GetMatrixes(MozaicPanel panno, string line)
        {
            /*
             var tmp = line.Split('[');

             panno.Matrixes = new Matrix[
                 Convert.ToInt32(Regex.Match(tmp.Last(), @"(\d*),(\d*)").Groups[2].Value)+1,
                 Convert.ToInt32(Regex.Match(tmp.Last(), @"(\d*),(\d*)").Groups[1].Value)+1
                 ];
             int matrixLines = Convert.ToInt32(Regex.Match(tmp[1], @"(\d*),(\d*)").Groups[2].Value);
             int matrixColumns = Convert.ToInt32(Regex.Match(tmp[1], @"(\d*),(\d*)").Groups[1].Value);

             for(int i=2;i<tmp.Count();i++)
             {
                 int lines = Convert.ToInt32(Regex.Match(tmp[i], @"(\d*),(\d*)").Groups[2].Value);

                 int columns = Convert.ToInt32(Regex.Match(tmp[i], @"(\d*),(\d*)").Groups[1].Value);

                 List<string> matrix = Regex.Matches(tmp[i], @"\d*").Cast<Match>()
                 .Select(m => m.Value)
                 .Where(n => n != "")
                 .ToList()
                 ;
                  matrix.RemoveRange(0, 2);

                 Matrix total = new Matrix(matrixColumns,matrixLines);

                 for (int k=0;k<total.mozaics.GetLength(1);k++)
                 {
                     for (int j = 0; j < total.mozaics.GetLength(0); j++)
                     {
                         if (matrix.ElementAtOrDefault(k * matrixColumns + j) != null)

                             total.mozaics[j, k] = Convert.ToInt32(matrix.ElementAt(k* matrixColumns + j));
                         else
                         {
                             total.mozaics[k, j] = 0;
                         }

                     }

                 }
                 panno.Matrixes[lines, columns] = total;

             }
             */
            return panno;
        }




    }
}
