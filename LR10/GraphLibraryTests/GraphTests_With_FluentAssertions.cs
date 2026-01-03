using FluentAssertions;
using GraphLibrary;
using NUnit.Framework;
using System.Collections.Generic;

namespace GraphLibraryTests
{
    [TestFixture]
    public class GraphTests_With_FluentAssertions
    {
        [Test]
        public void BFS_From_Node_0_Should_Return_0_1_2()
        {
            // Arrange
            var graph = new Graph();
            var incidenceMatrix = new List<List<int>>
            {
                new List<int> { -1, 0 },
                new List<int> { 1, -1 },
                new List<int> { 0, 1 }
            };

            // Act
            graph.LoadFromIncidenceMatrix(incidenceMatrix);
            var result = graph.BFS(0);

            // Assert (только Fluent Assertions!)
            result.Should().BeEquivalentTo(new[] { 0, 1, 2 });
        }

        [Test]
        public void SaveToAdjacencyMatrix_Should_Produce_Correct_Matrix()
        {
            var graph = new Graph();
            var incidenceMatrix = new List<List<int>>
            {
                new List<int> { -1, 0 },
                new List<int> { 1, -1 },
                new List<int> { 0, 1 }
            };

            graph.LoadFromIncidenceMatrix(incidenceMatrix);
            var matrix = graph.GetAdjacencyMatrix();

            var expected = new List<List<int>>
            {
                new List<int> { 0, 1, 0 },
                new List<int> { 1, 0, 1 },
                new List<int> { 0, 1, 0 }
            };

            matrix.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BFS_From_NonExistent_Node_Should_Throw_ArgumentException()
        {
            var graph = new Graph();
            graph.LoadFromIncidenceMatrix(new List<List<int>>()); // пустой граф

            var act = () => graph.BFS(0);
            act.Should().Throw<ArgumentException>()
                .WithMessage("Node 0 does not exist in the graph.");
        }

        // Тест: изолированный узел корректно добавляется в adjacencyList
        [Test]
        public void LoadGraphWithIsolatedNode_ShouldIncludeItInAdjacencyList()
        {
            var graph = new Graph();
            var matrix = new List<List<int>>
    {
        new List<int> { -1 }, // ребро: 0 → 1
        new List<int> { 1 },
        new List<int> { 0 }   // узел 2 — изолированный
    };

            graph.LoadFromIncidenceMatrix(matrix);

            graph.IsEmpty.Should().BeFalse();
            graph.GetNodes().Should().BeEquivalentTo(new[] { 0, 1, 2 });
            graph.GetNeighbors(2).Should().BeEmpty();
        }

        // Тест: BFS не выходит за пределы компоненты связности
        [Test]
        public void BFS_OnDisconnectedGraph_ShouldNotLeaveComponent()
        {
            var graph = new Graph();
            var matrix = new List<List<int>>
    {
        new List<int> { -1, 0 }, // 0—1
        new List<int> { 1, 0 },
        new List<int> { 0, -1 }, // 2—3
        new List<int> { 0, 1 }
    };

            graph.LoadFromIncidenceMatrix(matrix);

            graph.BFS(0).Should().BeEquivalentTo(new[] { 0, 1 });
            graph.BFS(2).Should().BeEquivalentTo(new[] { 2, 3 });
        }

        // Тест: корректная индексация узлов при построении МС
        [Test]
        public void BuildAdjacencyMatrix_ShouldHandleNonSequentialNodeIds()
        {
            var graph = new Graph();
            // МИ: рёбра только между узлами 0 и 2 → узел 1 изолирован
            var matrix = new List<List<int>>
    {
        new List<int> { -1 },
        new List<int> { 0 }, // узел 1 — изолированный
        new List<int> { 1 }
    };

            graph.LoadFromIncidenceMatrix(matrix);
            var ms = graph.GetAdjacencyMatrix();

            // Ожидаем матрицу 3×3:
            // [0][2] = 1, [2][0] = 1, остальное = 0
            ms[0][2].Should().Be(1);
            ms[2][0].Should().Be(1);
            ms[1].Should().AllBeEquivalentTo(0); // строка изолированного узла
        }

        // Тест: сохранение пустой матрицы смежности
        [Test]
        public void SaveEmptyGraph_ShouldProduceEmptyAdjacencyMatrix()
        {
            var graph = new Graph();
            graph.LoadFromIncidenceMatrix(new List<List<int>>());
            var ms = graph.GetAdjacencyMatrix();

            ms.Should().BeEmpty();
        }

        // Тест: один узел без рёбер → BFS возвращает его
        [Test]
        public void BFS_SingleNodeNoEdges_ShouldReturnItself()
        {
            var graph = new Graph();
            graph.LoadFromIncidenceMatrix(new List<List<int>> { new List<int>() });
            var result = graph.BFS(0);

            result.Should().BeEquivalentTo(new[] { 0 });
        }
    }
}