using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using LR8;


namespace GraphLibraryTests
{
    [TestFixture]
    public class GraphReaderTests
    {
        [Test]
        public void LoadIncidenceMatrix_WithValidInput_ReturnsCorrectMatrix()
        {
            // Arrange
            string input = "-1 0\n1 -1\n0 1";
            using var stringReader = new StringReader(input);
            var reader = new IncidenceMatrixReaderFromTextReader(); // см. ниже

            // Act
            var matrix = reader.LoadIncidenceMatrixFromTextReader(stringReader);

            // Assert
            Assert.That(matrix.Count, Is.EqualTo(3));
            Assert.That(matrix[0], Is.EqualTo(new List<int> { -1, 0 }));
            Assert.That(matrix[1], Is.EqualTo(new List<int> { 1, -1 }));
            Assert.That(matrix[2], Is.EqualTo(new List<int> { 0, 1 }));
        }
    }

    // Вспомогательный класс для тестирования с TextReader
    public class IncidenceMatrixReaderFromTextReader : IGraphReader
    {
        public List<List<int>> LoadIncidenceMatrix(string path)
        {
            throw new System.NotImplementedException("Use LoadIncidenceMatrixFromTextReader instead.");
        }

        public List<List<int>> LoadIncidenceMatrixFromTextReader(TextReader reader)
        {
            var matrix = new List<List<int>>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var row = System.Array.ConvertAll(line.Split(' '), int.Parse).ToList();
                matrix.Add(row);
            }
            return matrix;
        }
    }


}