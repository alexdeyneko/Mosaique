using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class Generator
    {
        public Generator(Catalog cat, Matrix mat)
        {
            catalog = cat;
            matrix = mat;
        }
        private Matrix matrix;
        private Catalog catalog;
        public Mozaic[][] total;
        public void Generate()
        {
            int hight = matrix.colorMatrix[0].Count();
            int width = matrix.colorMatrix.Count();

            total = new Mozaic[width][];
            for (int i = 0; i < width; i++)
            {
                total[i] = new Mozaic[hight];
                for (int j = 0; j < hight; j++)
                {
                  
                    double maxDelta = 255;
                   
                    foreach(var item in catalog.mozaiclist)
                    {
                        double delta = Math.Sqrt(Math.Pow(matrix.colorMatrix[i][j].g - item.color.g,2)+
                            Math.Pow(matrix.colorMatrix[i][j].r - item.color.r, 2)+
                            Math.Pow(matrix.colorMatrix[i][j].b - item.color.b, 2));
                        
                        if (delta < maxDelta)
                        {
                            total[i][j] = item;
                            
                            maxDelta = delta;
                        }
                    }
                    
                }
            }

            Bitmap newMap = new Bitmap(40* total.Count(), 40* total[0].Count());
            Graphics g = Graphics.FromImage(newMap);
            Bitmap bm1;
            for (int i=0;i<total.Count();i++)
            {
                for(int j = 0; j < total[0].Count(); j++)
                {
                    bm1 = new Bitmap(total[i][j].file);//ваша маленькая картинка
                    g.DrawImage(bm1, i * 40, j*40, 40, 40);//туту нужно изменить под ваши задачи размещения картинок, координаты, области и тд
                }
              

            }

               g.Dispose();
            
            newMap.Save("C:\\Users\\Алексей\\Desktop\\test4.jpg");
       
    }
        

    }
}
