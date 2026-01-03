using System;
using System.Diagnostics;

namespace Lab2_1
{
    class Program
    {
        static double f(double x)
        {
            return Math.Exp(1 - x) / (2 + Math.Sin(1 + x * x));
        }

        static double CalculateIntegral(double a, double b, int n)
        {
            if (n % 2 != 0) n++; // n - четное 

            double h = (b - a) / n;
            double sum = f(a) + f(b); // первое и последнее значение

            for (int i = 1; i < n; i++)
            {
                double xi = a + i * h;
                Debug.Assert(xi >= a && xi <= b, "Выход за границы диапазона интегрирования!");

                if (i % 2 == 0)
                    sum += 2 * f(xi); // четные узлы
                else
                    sum += 4 * f(xi); // нечетные узлы
            }
            return sum * h / 3; // формула Симпсона
        }

        // оценка погрешности по правилу Рунге
        static double RungeError(double I_n, double I_2n)
        {
            return Math.Abs(I_2n - I_n) / 15.0; // для Симпсона 2^4 - 1 = 15
        }

        static void Main(string[] args)
        {
            // прослушиватель трассировки
            Trace.Listeners.Add(new ConsoleTraceListener());
            Trace.AutoFlush = true;

            double a = 0.4;
            double b = 1.0;
            double tolerance = 1e-14;

            int FN = 5; // Д
            int LN = 9; // З

            // начальные значения
            int n = 2;


            double delta;
            int step = 0;

            Trace.TraceInformation("НАЧАЛО ВЫЧИСЛЕНИЙ МЕТОДОМ СИМПСОНА");
            Trace.WriteLine($"Пределы интегрирования: [{a}, {b}]");
            Trace.WriteLine($"Точность: {tolerance}");
            Trace.WriteLine($"Функция: f(x) = e^(1-x) / (2 + sin(1+x^2))");

            do
            {
                step++;
                double In = CalculateIntegral(a, b, n);
                double I2n = CalculateIntegral(a, b, 2 * n);
                delta = RungeError(In, I2n);
                double h = (b - a) / n;

                Trace.WriteLine($"Шаг {step}: n = {n}, h = {h:F8}");
                Trace.Indent();
                Trace.WriteLine($"integral_n   = {In:F8}");
                Trace.WriteLine($"integral_2n  = {I2n:F8}");
                Trace.WriteLine($"delta        = {delta:F10}");
                Trace.Unindent();
                Trace.WriteLine("---");

                // вывод на FN-ом шаге в отладчик
                if (step == FN)
                {
                    Debug.WriteLine($"FN шаг {FN}: Интеграл = {In:F8}");
                    Console.WriteLine($"*** ОТЛАДКА: FN шаг {FN}: Интеграл = {In:F8} ***");
                }

                // вывод на LN-ом шаге в трассировщик
                if (step == LN)
                {
                    Trace.TraceInformation($"LN шаг {LN}: Интеграл = {In:F8}");
                }

                n *= 2;

            } while (delta > tolerance);


            Trace.TraceInformation("ВЫЧИСЛЕНИЯ ЗАВЕРШЕНЫ");
            Trace.WriteLine($"РЕЗУЛЬТАТ: I = {CalculateIntegral(a, b, n):F8}");
            Trace.WriteLine($"Достигнутая погрешность: {delta:F10}");
            Trace.WriteLine($"Количество разбиений: n = {n}");
            Trace.WriteLine($"Количество шагов уточнения: {step}");
        }
    }
}