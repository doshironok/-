using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBoxTesting
{
    public interface IArea
    {
        bool IsPointInArea(double x, double y);
    }
}