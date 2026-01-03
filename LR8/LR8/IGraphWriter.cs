using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR8
{
    public interface IGraphWriter
    {
        void SaveAdjacencyMatrix(string path, List<List<int>> adjacencyMatrix);
    }
}
