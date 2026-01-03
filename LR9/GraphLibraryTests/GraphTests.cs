using NUnit.Framework;
using System;
using System.Collections.Generic;
using GraphLibrary;

namespace GraphLibraryTests
{
    [TestFixture]
    public class GraphTests
    {
        // Связный граф из 3 узлов: 0—1—2
        [Test(Description = "BFS из узла 0 на связном графе 0—1—2 возвращает [0,1,2]")]
        public void BFS_FromNode0_ReturnsCorrectOrder()
        {
            var fakeReader = new FakeIncidenceReader();
            var fakeWriter = new FakeAdjacencyWriter();
            var graph = new Graph(fakeReader, fakeWriter);

            graph.LoadFromIncidenceMatrix("");
            var result = graph.BFS(0);

            Assert.That(result, Is.EqualTo(new List<int> { 0, 1, 2 }));
        }

        // Проверка вызова сохранения
        [Test(Description = "SaveToAdjacencyMatrix вызывает writer с матрицей 3x3")]
        public void SaveToAdjacencyMatrix_CallsWriterCorrectly()
        {
            var fakeReader = new FakeIncidenceReader();
            var spyWriter = new SpyAdjacencyWriter();
            var graph = new Graph(fakeReader, spyWriter);

            graph.LoadFromIncidenceMatrix("");
            graph.SaveToAdjacencyMatrix("output.txt");

            Assert.That(spyWriter.SaveWasCalled, Is.True);
            Assert.That(spyWriter.SavedMatrix.Count, Is.EqualTo(3));
            Assert.That(spyWriter.SavedMatrix[0][1], Is.EqualTo(1)); // 0—1
            Assert.That(spyWriter.SavedMatrix[1][2], Is.EqualTo(1)); // 1—2
        }

        // Пустой граф
        [Test(Description = "BFS на пустом графе выбрасывает ArgumentException")]
        public void BFS_OnEmptyGraph_ThrowsArgumentException()
        {
            var reader = new FakeEmptyGraphReader();
            var writer = new FakeAdjacencyWriter();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("");

            Assert.Throws<ArgumentException>(() => graph.BFS(0));
        }

        // Один изолированный узел (0)
        [Test(Description = "BFS из узла 0 на графе из одного узла возвращает [0]")]
        public void BFS_SingleIsolatedNode_ReturnsNodeItself()
        {
            var reader = new FakeSingleNodeReader();
            var writer = new FakeAdjacencyWriter();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("");
            var result = graph.BFS(0);

            Assert.That(result, Is.EqualTo(new List<int> { 0 }));
        }

        // Несвязный граф (0—1 и 2—3)
        [Test(Description = "BFS из узла 0 в несвязном графе возвращает только компоненту [0,1]")]
        public void BFS_DisconnectedGraph_ReturnsOnlyComponent()
        {
            var reader = new FakeDisconnectedGraphReader();
            var writer = new FakeAdjacencyWriter();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("");
            var result = graph.BFS(0);

            // Должен найти только 0 и 1
            Assert.That(result, Is.EqualTo(new List<int> { 0, 1 }));
        }

        // Изолированный узел в несвязном графе
        [Test(Description = "BFS из изолированного узла 2 возвращает [2]")]
        public void BFS_IsolatedNode_ReturnsOnlyItself()
        {
            var reader = new FakeDisconnectedGraphReader();
            var writer = new FakeAdjacencyWriter();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("");
            var result = graph.BFS(2);

            Assert.That(result, Is.EqualTo(new List<int> { 2 }));
        }

        // Попытка BFS из несуществующего узла
        [Test(Description = "BFS из несуществующего узла 999 выбрасывает ArgumentException")]
        public void BFS_NonExistentNode_ThrowsArgumentException()
        {
            var reader = new FakeIncidenceReader(); // граф 0-1-2
            var writer = new FakeAdjacencyWriter();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("");

            Assert.Throws<ArgumentException>(() => graph.BFS(999));
        }

        // Проверка сохранённой матрицы смежности
        [Test(Description = "Сохранённая МС соответствует ожидаемой для графа 0—1—2")]
        public void SaveToAdjacencyMatrix_SavesCorrectMatrix()
        {
            var reader = new FakeIncidenceReader();
            var spy = new SpyAdjacencyWriter();
            var graph = new Graph(reader, spy);

            graph.LoadFromIncidenceMatrix("");
            graph.SaveToAdjacencyMatrix("out");

            var expected = new List<List<int>>
            {
                new List<int> { 0, 1, 0 },
                new List<int> { 1, 0, 1 },
                new List<int> { 0, 1, 0 }
            };

            Assert.That(spy.SavedMatrix, Is.EqualTo(expected));
        }
    }

    // Fake-реализации

    public class FakeIncidenceReader : IGraphReader
    {
        // Граф: 0—1—2
        public List<List<int>> LoadIncidenceMatrix(string path) => new()
        {
            new List<int> { -1, 0 },
            new List<int> { 1, -1 },
            new List<int> { 0, 1 }
        };
    }

    public class FakeEmptyGraphReader : IGraphReader
    {
        // Пустой граф (0 узлов)
        public List<List<int>> LoadIncidenceMatrix(string path) => new();
    }

    public class FakeSingleNodeReader : IGraphReader
    {
        // Один узел, 0 рёбер → матрица 1×0
        public List<List<int>> LoadIncidenceMatrix(string path) => new()
        {
            new List<int>()
        };
    }

    public class FakeDisconnectedGraphReader : IGraphReader
    {
        // Граф: 0—1, и изолированный узел 2
        public List<List<int>> LoadIncidenceMatrix(string path) => new()
    {
        new List<int> { -1 }, // ребро e0: 0 → 1
        new List<int> { 1 },
        new List<int> { 0 }   // узел 2 не участвует ни в одном ребре
    };
    }

    public class FakeAdjacencyWriter : IGraphWriter
    {
        public void SaveAdjacencyMatrix(string path, List<List<int>> matrix) { }
    }

    public class SpyAdjacencyWriter : IGraphWriter
    {
        public bool SaveWasCalled { get; private set; }
        public List<List<int>> SavedMatrix { get; private set; }

        public void SaveAdjacencyMatrix(string path, List<List<int>> matrix)
        {
            SaveWasCalled = true;
            SavedMatrix = matrix;
        }
    }
}