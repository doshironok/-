using NUnit.Framework;
using NumberConverterApp;
using NUnit.Framework.Legacy;

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

            ClassicAssert.AreEqual(expected, result);
        }

        // Тестирование нуля
        [Test]
        public void DecimalToOctal_Zero_ReturnsZero()
        {
            string input = "0";
            string expected = "0";

            string result = converter.DecimalToOctal(input);

            ClassicAssert.AreEqual(expected, result);
        }

        // Тестирование отрицательного числа
        [Test]
        public void DecimalToOctal_NegativeNumber_ReturnsCorrectOctal()
        {
            string input = "-64";
            string expected = "-100";

            string result = converter.DecimalToOctal(input);

            ClassicAssert.AreEqual(expected, result);
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
            ClassicAssert.AreEqual(expected, result);
        }

        // Тестирование граничного значения (минимальное отрицательное)
        [Test]
        public void DecimalToOctal_MinValue_ReturnsCorrectOctal()
        {
            string input = long.MinValue.ToString();
            string expected = "-1000000000000000000000";

            string result = converter.DecimalToOctal(input);

            ClassicAssert.AreEqual(expected, result);
        }

        // Тестирование некорректного ввода (не число)
        [Test]
        public void DecimalToOctal_InvalidInput_ThrowsArgumentException()
        {
            string input = "abc";

            ClassicAssert.Throws<ArgumentException>(() => converter.DecimalToOctal(input));
        }

        // Тестирование с лишними пробелами
        [Test]
        public void DecimalToOctal_WithSpaces_ThrowsArgumentException()
        {
            string input = " 123 ";
            ClassicAssert.Throws<ArgumentException>(() => converter.DecimalToOctal(input));
        }

        [Test]
        public void DecimalToOctal_PositiveNumber_CorrectDigits()
        {
            // Убивает мутант: % 8 на % 9
            var result = converter.DecimalToOctal("10");
            ClassicAssert.AreEqual("12", result); // 10 = 1*8 + 2
        }

        [Test]
        public void DecimalToOctal_NegativeNumber_HasMinusSign()
        {
            // Убивает мутант: "-" + result на result
            var result = converter.DecimalToOctal("-10");
            ClassicAssert.IsTrue(result.StartsWith("-"));
            ClassicAssert.AreEqual("-12", result);
        }
    }
}