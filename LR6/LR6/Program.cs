using System;

namespace NumberConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberConverter converter = new NumberConverter();

            Console.WriteLine("Программа для преобразования чисел из десятичной в восьмеричную систему.");
            Console.WriteLine("Введите 'exit' для выхода.");

            while (true)
            {
                Console.Write("Введите десятичное число: ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "exit")
                {
                    Console.WriteLine("Выход из программы.");
                    break;
                }

                try
                {
                    string octalResult = converter.DecimalToOctal(input);
                    Console.WriteLine($"Восьмеричное представление: {octalResult}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла непредвиденная ошибка: {ex.Message}");
                }

                Console.WriteLine(); // Пустая строка для удобства
            }
        }
    }
}