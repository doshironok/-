using System;
using System.Diagnostics;

namespace Lab2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            Trace.AutoFlush = true;

            Trace.TraceInformation("НАЧАЛО ВЫЧИСЛЕНИЯ");

            int p = 1;
            int q = 1;
            int x0 = 0; // x_{n-2}
            int x1 = 1; 

            // количество элементов для суммирования
            int count = 50;
            int sum = 0; 
            int current;

            Trace.Indent();

            for (int n = 0; n < count; n++)
            {
                if (n == 0)
                    current = x0;
                else if (n == 1)
                    current = x1;
                else
                {
                    
                    // проверка переполнения при вычислении с помощью checked и Assert
                    try
                    {
                        current = checked(p * x1 + q * x0);
                    }
                    catch (OverflowException)
                    {
                        Debug.Fail($"Арифметическое переполнение при вычислении элемента n = {n}.");
                        Trace.TraceError($"ПЕРЕПОЛНЕНИЕ! Невозможно вычислить элемент для n = {n}. Сумма рассчитана до n = {n - 1}.");
                        break;
                    }

                    // сдвигаем значения для следующей итерации
                    x0 = x1;
                    x1 = current;
                }

                int newSum = sum + current;
                Debug.Assert(newSum >= sum, "Обнаружено арифметическое переполнение при суммировании.");
                if (newSum < sum)
                {
                    Trace.TraceError($"ПЕРЕПОЛНЕНИЕ СУММЫ! На шаге n={n}. Текущая сумма: {sum}, новый элемент: {current}.");
                    break;
                }
                // явная проверка для трассировки
                sum = newSum;

                Trace.WriteLine($"n={n,2}: x_n={current,10}, Сумма={sum,10}");
            }

            Trace.Unindent();
            Trace.TraceInformation($"ВЫЧИСЛЕНИЯ ЗАВЕРШЕНЫ. Итоговая сумма: {sum}");

            Console.WriteLine($"Сумма первых {count} элементов последовательности: {sum}");
            Console.ReadKey();
        }
    }
}