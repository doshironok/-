using System;
using System.Numerics;

namespace NumberConverterApp
{
    public class NumberConverter
    {
        public string DecimalToOctal(string decimalStr)
        {
            if (string.IsNullOrWhiteSpace(decimalStr))
            {
                throw new ArgumentException("Входная строка не является корректным десятичным числом.", nameof(decimalStr));
            }

            if (decimalStr.Trim() != decimalStr)
            {
                throw new ArgumentException("Входная строка содержит пробелы.", nameof(decimalStr));
            }

            if (!BigInteger.TryParse(decimalStr, out BigInteger number))
            {
                throw new ArgumentException("Входная строка не является корректным десятичным числом.", nameof(decimalStr));
            }

            if (number == 0)
                return "0";

            bool isNegative = number < 0;
            if (isNegative)
                number = -number;

            string result = "";
            while (number > 0)
            {
                result = (number % 8).ToString() + result;
                number /= 8;
            }

            return isNegative ? "-" + result : result;
        }
    }
}