using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models.Services
{
    public class DrawService
    {
        public MozaicPanel Panno { get; set; }
        public DrawService(MozaicPanel panno)
        {
            Panno = panno;
        }
        public Bitmap DrawPanno()
        {

            int width = Panno.Matrixes.GetLength(0) * (Panno.Matrixes[0, 0].mozaics.GetLength(0))
                * (Panno.Catalog.Mozaics.First().GetSmall().Width + Panno.ComputerMatrixGap);

            int height = Panno.Matrixes.GetLength(1) * (Panno.Matrixes[0, 0].mozaics.GetLength(1))
            * (Panno.Catalog.Mozaics.First().GetSmall().Height + Panno.ComputerMatrixGap);

            Bitmap newMap = new Bitmap(width, height);

            Graphics g = Graphics.FromImage(newMap);

            for (int i = 0; i < Panno.Matrixes.GetLength(0); i++)
            {
                for (int j = 0; j < Panno.Matrixes.GetLength(1); j++)
                {
                    Bitmap current = DrawMatrix(Panno.Matrixes[i, j]);
                    g.DrawImage(current
                        , i * (current.Width + Panno.ComputerMatrixGap), j * (current.Height + Panno.ComputerMatrixGap),
                        current.Width, current.Height);

                }

            }
            g.Dispose();

            return newMap;
        }
        private Bitmap DrawMatrix(Matrix matrix)
        {
            Bitmap image = new Bitmap(

                matrix.mozaics.GetLength(0) *
                (Panno.Catalog.Mozaics.First().GetSmall().Width + Panno.ComputerMozaicGap),

                matrix.mozaics.GetLength(1) * (Panno.Catalog.Mozaics.First().GetSmall().Height + Panno.ComputerMozaicGap)

                );

            Graphics g = Graphics.FromImage(image);

            for (int i = 0; i < matrix.mozaics.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.mozaics.GetLength(1); j++)
                {
                    Bitmap current = GetMozaicForId(matrix.mozaics[i, j]);

                    g.DrawImage(current, i * (current.Width + Panno.ComputerMozaicGap),

                        j * (current.Height + Panno.ComputerMozaicGap), current.Width, current.Height);

                }

            }

            return image;
        }

        private Bitmap GetMozaicForId(int id)
        {
            if (id != 0)
                return Panno.Catalog.Mozaics.Where(n => n.RatingId == id).First().GetSmall();
            else
            {
                Bitmap bmp = new Bitmap(Panno.Catalog.Mozaics.First().GetSmall().Width,
                    Panno.Catalog.Mozaics.First().GetSmall().Height);
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(Color.White);
                return bmp;
            }


        }
    }
}
