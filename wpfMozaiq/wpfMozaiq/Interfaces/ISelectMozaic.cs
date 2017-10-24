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
        void GenerateForGrid();
        Mozaic GenerateForOne(Catalog catalog, PixelsBlock block);
    }
}
