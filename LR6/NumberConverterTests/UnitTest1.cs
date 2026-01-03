using NUnit.Framework;
using NumberConverterApp;

namespace NumberConverterApp.Tests
{
    [TestFixture]
    public class NumberConverterTests
    {
        private NumberConverter converter;

        [SetUp]
        public void Setup()
        {
            converter = new NumberConverter();
        }

        // Тестирование корректных положительных чисел
        [Test]
        public void DecimalToOctal_PositiveNumber_ReturnsCorrectOctal()
        {
            string input = "64";
            string expected = "100";

            string result = converter.DecimalToOctal(input);

            Assert.AreEqual(expected, result);
        }

        // Тестирование нуля
        [Test]
        public void DecimalToOctal_Zero_ReturnsZero()
        {
            string input = "0";
            string expected = "0";

            string result = converter.DecimalToOctal(input);

            Assert.AreEqual(expected, result);
        }

        // Тестирование отрицательного числа
        [Test]
        public void DecimalToOctal_NegativeNumber_ReturnsCorrectOctal()
        {
            string input = "-64";
            string expected = "-100";

            string result = converter.DecimalToOctal(input);

            Assert.AreEqual(expected, result);
        }

        // Тестирование граничного значения (максимальное положительное)
        [Test]
        public void DecimalToOctal_MaxValue_ReturnsCorrectOctal()
        {
            // Arrange
            string input = long.MaxValue.ToString();
            string expected = Convert.ToString(long.MaxValue, 8);

            // Act
            string result = converter.DecimalToOctal(input);

            // Assert
            Assert.AreEqual(expected, result);
        }

        // Тестирование граничного значения (минимальное отрицательное)
        [Test]
        public void DecimalToOctal_MinValue_ReturnsCorrectOctal()
        {
            string input = long.MinValue.ToString();
            string expected = "-1000000000000000000000";

            string result = converter.DecimalToOctal(input);

            Assert.AreEqual(expected, result);
        }

        // Тестирование некорректного ввода (не число)
        [Test]
        public void DecimalToOctal_InvalidInput_ThrowsArgumentException()
        {
            string input = "abc";

            Assert.Throws<ArgumentException>(() => converter.DecimalToOctal(input));
        }

        // Тестирование с лишними пробелами
        [Test]
        public void DecimalToOctal_WithSpaces_ThrowsArgumentException()
        {
            string input = " 123 ";
            Assert.Throws<ArgumentException>(() => converter.DecimalToOctal(input));
        }
    }
}