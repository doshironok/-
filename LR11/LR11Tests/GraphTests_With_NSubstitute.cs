using GraphLibrary;
using NSubstitute;

namespace GraphLibraryTests
{
    [TestFixture]
    public class GraphTests_With_NSubstitute
    {
        // Тест 1: BFS на связном графе
        [Test]
        public void BFS_FromNode0_ReturnsCorrectOrder()
        {
            // Arrange
            var reader = Substitute.For<IGraphReader>();
            var writer = Substitute.For<IGraphWriter>();

            // Задаём поведение mock'а: возвращаем фиксированную МИ
            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>
            {
                new List<int> { -1, 0 },
                new List<int> { 1, -1 },
                new List<int> { 0, 1 }
            });

            var graph = new Graph(reader, writer);

            // Act
            graph.LoadFromIncidenceMatrix("dummy");
            var result = graph.BFS(0);

            // Assert
            Assert.That(result, Is.EqualTo(new List<int> { 0, 1, 2 }));
        }

        // Тест 2: Проверка вызова Save с правильной МС
        [Test]
        public void SaveToAdjacencyMatrix_CallsWriterWithCorrectMatrix()
        {
            // Arrange
            var reader = Substitute.For<IGraphReader>();
            var writer = Substitute.For<IGraphWriter>();

            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>
            {
                new List<int> { -1, 0 },
                new List<int> { 1, -1 },
                new List<int> { 0, 1 }
            });


            var graph = new Graph(reader, writer);

            // Act
            graph.LoadFromIncidenceMatrix("dummy");
            graph.SaveToAdjacencyMatrix("out.txt");

            // Assert
            var expectedMatrix = new List<List<int>>
            {
                new List<int> { 0, 1, 0 },
                new List<int> { 1, 0, 1 },
                new List<int> { 0, 1, 0 }
            };

            writer.Received(1).SaveAdjacencyMatrix("out.txt", Arg.Is<List<List<int>>>(actual =>
                actual != null &&
                actual.Count == expectedMatrix.Count &&
                actual.Zip(expectedMatrix, (a, e) => a.SequenceEqual(e)).All(match => match)
            ));
        }

        // Тест 3: BFS из несуществующего узла
        [Test]
        public void BFS_NonExistentNode_ThrowsArgumentException()
        {
            var reader = Substitute.For<IGraphReader>();
            var writer = Substitute.For<IGraphWriter>();

            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>> { new List<int>() });

            var graph = new Graph(reader, writer);
            graph.LoadFromIncidenceMatrix("dummy");

            Assert.Throws<ArgumentException>(() => graph.BFS(999));
        }

        // Тест 4: Пустой граф
        [Test]
        public void LoadEmptyGraph_ResultsInEmptyAdjacencyList()
        {
            var reader = Substitute.For<IGraphReader>();
            var writer = Substitute.For<IGraphWriter>();

            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>());

            var graph = new Graph(reader, writer);
            graph.LoadFromIncidenceMatrix("dummy");

            Assert.That(graph.IsEmpty, Is.True);
        }

        // Тест 5: Изолированный узел в несвязном графе
        [Test]
        public void BFS_IsolatedNode_ReturnsOnlyItself()
        {
            // МИ: узел 0—1, узел 2 (изолированный)
            var reader = Substitute.For<IGraphReader>();
            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>
            {
                new List<int> { -1 },
                new List<int> { 1 },
                new List<int> { 0 } // изолированный узел
            });

            var writer = Substitute.For<IGraphWriter>();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("dummy");
            var result = graph.BFS(2);

            Assert.That(result, Is.EqualTo(new List<int> { 2 }));
        }

        // Тест 6: Несвязный граф — две компоненты связности
        [Test]
        public void BFS_DisconnectedGraph_ReturnsOnlyComponent()
        {
            // МИ: 0—1 и 2—3
            var reader = Substitute.For<IGraphReader>();
            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>
            {
                new List<int> { -1, 0 },
                new List<int> { 1, 0 },
                new List<int> { 0, -1 },
                new List<int> { 0, 1 }
            });

            var writer = Substitute.For<IGraphWriter>();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("dummy");

            Assert.That(graph.BFS(0), Is.EqualTo(new List<int> { 0, 1 }));
            Assert.That(graph.BFS(2), Is.EqualTo(new List<int> { 2, 3 }));
        }

        // Тест 7: Один узел без рёбер
        [Test]
        public void BFS_SingleNodeNoEdges_ReturnsItself()
        {
            var reader = Substitute.For<IGraphReader>();
            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>
            {
                new List<int>() // один узел, ноль рёбер
            });

            var writer = Substitute.For<IGraphWriter>();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("dummy");
            var result = graph.BFS(0);

            Assert.That(result, Is.EqualTo(new List<int> { 0 }));
        }

        // Тест 8: Пустой граф — попытка BFS
        [Test]
        public void BFS_EmptyGraph_ThrowsArgumentException()
        {
            var reader = Substitute.For<IGraphReader>();
            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>());

            var writer = Substitute.For<IGraphWriter>();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("dummy");

            Assert.Throws<ArgumentException>(() => graph.BFS(0));
        }

        // Тест 9: Сохранение пустого графа в МС
        [Test]
        public void SaveToAdjacencyMatrix_EmptyGraph_SavesEmptyMatrix()
        {
            var reader = Substitute.For<IGraphReader>();
            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>());

            var writer = Substitute.For<IGraphWriter>();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("dummy");
            graph.SaveToAdjacencyMatrix("out.txt");

            // Проверяем, что writer вызван с пустой матрицей
            writer.Received(1).SaveAdjacencyMatrix("out.txt", Arg.Is<List<List<int>>>(m => m.Count == 0));
        }

        // Тест 10: Сохранение графа с изолированным узлом
        [Test]
        public void SaveToAdjacencyMatrix_WithIsolatedNode_CreatesCorrectMatrix()
        {
            var reader = Substitute.For<IGraphReader>();
            reader.LoadIncidenceMatrix(Arg.Any<string>()).Returns(new List<List<int>>
            {
                new List<int> { -1 },
                new List<int> { 1 },
                new List<int> { 0 }
            });

            var writer = Substitute.For<IGraphWriter>();
            var graph = new Graph(reader, writer);

            graph.LoadFromIncidenceMatrix("dummy");
            graph.SaveToAdjacencyMatrix("out.txt");

            var expected = new List<List<int>>
            {
                new List<int> { 0, 1, 0 },
                new List<int> { 1, 0, 0 },
                new List<int> { 0, 0, 0 }
            };

            writer.Received(1).SaveAdjacencyMatrix("out.txt", Arg.Is<List<List<int>>>(actual =>
                actual.Count == 3 &&
                actual[0][1] == 1 &&
                actual[1][0] == 1 &&
                actual[2][2] == 0
            ));
        }
    }
}