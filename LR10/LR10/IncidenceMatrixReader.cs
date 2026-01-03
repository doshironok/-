using System;
using System.Collections.Generic;
using System.IO;

namespace GraphLibrary
{
    public class IncidenceMatrixReader : IGraphReader
    {
        public List<List<int>> LoadIncidenceMatrix(string path)
        {
            var matrix = new List<List<int>>();
            using (var reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var row = Array.ConvertAll(line.Split(' '), int.Parse).ToList();
                    matrix.Add(row);
                }
            }
            return matrix;
        }
    }
}