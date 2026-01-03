using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR8
{
    public interface IGraphReader
    {
        List<List<int>> LoadIncidenceMatrix(string path);
    }
}
