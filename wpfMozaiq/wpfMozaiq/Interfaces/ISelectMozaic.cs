using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpfMozaiq.Models;

namespace wpfMozaiq.Interfaces
{
    interface ISelectMozaic
    {
        Mozaic[,] GenerateForGrid(Catalog catalog, PixelsBlock[,] grid);
        Mozaic GenerateForOne(Catalog catalog, PixelsBlock block);
    }
}
