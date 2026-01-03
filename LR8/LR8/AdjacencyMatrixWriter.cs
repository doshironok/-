using LR8;
using System.Collections.Generic;
using System.IO;

namespace GraphLibrary
{
    public class AdjacencyMatrixWriter : IGraphWriter
    {
        public void SaveAdjacencyMatrix(string path, List<List<int>> adjacencyMatrix)
        {
            using (var writer = new StreamWriter(path))
            {
                foreach (var row in adjacencyMatrix)
                {
                    writer.WriteLine(string.Join(" ", row));
                }
            }
        }
    }
}