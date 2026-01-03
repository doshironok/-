using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLibrary
{
    public class Graph
    {
        private Dictionary<int, List<int>> adjacencyList;
        public Graph()
        {
            adjacencyList = new Dictionary<int, List<int>>();
        }

        public Graph(IGraphReader reader, IGraphWriter writer)
        {
            adjacencyList = new Dictionary<int, List<int>>();
            Reader = reader;
            Writer = writer;
        }

        public IGraphReader Reader { get; set; }
        public IGraphWriter Writer { get; set; }

        public void LoadFromIncidenceMatrix(string path)
        {
            var incidenceMatrix = Reader.LoadIncidenceMatrix(path);
            BuildAdjacencyListFromIncidence(incidenceMatrix);
        }

        private void BuildAdjacencyListFromIncidence(List<List<int>> matrix)
        {
            adjacencyList.Clear();
            if (matrix == null || matrix.Count == 0)
            {
                return; // вообще нет узлов
            }

            int nodeCount = matrix.Count;
            // Даже если рёбер нет — узлы всё равно существуют!
            for (int i = 0; i < nodeCount; i++)
            {
                adjacencyList[i] = new List<int>();
            }

            // Если нет рёбер — выходим
            if (matrix[0].Count == 0)
            {
                return;
            }

            int edgeCount = matrix[0].Count;

            for (int e = 0; e < edgeCount; e++)
            {
                int from = -1, to = -1;
                for (int n = 0; n < nodeCount; n++)
                {
                    if (matrix[n][e] == -1) from = n;
                    else if (matrix[n][e] == 1) to = n;
                }
                if (from != -1 && to != -1)
                {
                    adjacencyList[from].Add(to);
                    adjacencyList[to].Add(from); // неориентированный граф
                }
            }
        }

        public List<int> BFS(int startNode)
        {
            if (!adjacencyList.ContainsKey(startNode))
                throw new ArgumentException($"Node {startNode} does not exist in the graph.");

            var visited = new HashSet<int>();
            var queue = new Queue<int>();
            var result = new List<int>();

            queue.Enqueue(startNode);
            visited.Add(startNode);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();
                result.Add(current);

                foreach (int neighbor in adjacencyList[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        public void SaveToAdjacencyMatrix(string path)
        {
            var matrix = BuildAdjacencyMatrix();
            Writer.SaveAdjacencyMatrix(path, matrix);
        }

        private List<List<int>> BuildAdjacencyMatrix()
        {
            int nodeCount = adjacencyList.Count;
            if (nodeCount == 0) return new List<List<int>>();

            var nodes = adjacencyList.Keys.OrderBy(k => k).ToList();
            var nodeIndex = new Dictionary<int, int>();
            for (int i = 0; i < nodes.Count; i++)
            {
                nodeIndex[nodes[i]] = i;
            }

            var matrix = new List<List<int>>();
            for (int i = 0; i < nodeCount; i++)
            {
                var row = new List<int>(new int[nodeCount]);
                matrix.Add(row);
            }

            foreach (var kvp in adjacencyList)
            {
                int i = nodeIndex[kvp.Key];
                foreach (int neighbor in kvp.Value)
                {
                    int j = nodeIndex[neighbor];
                    matrix[i][j] = 1;
                }
            }

            return matrix;
        }

        // Для тестов: загрузка напрямую из матрицы инцидентности
        public void LoadFromIncidenceMatrix(List<List<int>> matrix)
        {
            BuildAdjacencyListFromIncidence(matrix);
        }

        // для тестов: получение матрицы смежности без записи в файл
        public List<List<int>> GetAdjacencyMatrix()
        {
            return BuildAdjacencyMatrix();
        }

        // для тестов: получение всех узлов
        public List<int> GetNodes() => adjacencyList.Keys.ToList();

        // для тестов: получение соседей узла
        public List<int> GetNeighbors(int node) => adjacencyList.TryGetValue(node, out var n) ? n : new List<int>();

        public bool IsEmpty => adjacencyList.Count == 0;
    }
}