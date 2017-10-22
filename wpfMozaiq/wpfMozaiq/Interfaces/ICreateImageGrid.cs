using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Interfaces
{
    interface ICreateImageGrid
    {
        int CalculateOptimalWidth();
        int CalculateOptimalHeight();
        void CreateImageGrid();
    }
}
