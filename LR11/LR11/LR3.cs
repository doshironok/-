using System;

namespace AreaCheckerApp
{
    public class AreaChecker
    {

        public double R { get; set; } // Радиус окружности
        public double a { get; set; } // Полуширина прямоугольника по X
        public double b { get; set; } // Полувысота прямоугольника по Y

        public AreaChecker()
        {
            R = 1.0;
            a = 1.0;
            b = 1.0;
        }

        // Конструктор с параметрами
        public AreaChecker(double radius, double semiWidth, double semiHeight)
        {
            R = radius;
            a = semiWidth;
            b = semiHeight;
        }

        public int TestPoint(double x, double y)
        {
            double distanceSquared = x * x + y * y;
            bool insideCircle = distanceSquared <= R * R;
            bool insideRectangle = Math.Abs(x) <= a && Math.Abs(y) <= b;

            // Проверяем, находится ли точка в закрашенной области (Область 1)
            bool inShadedRegion = false;

            if (x > 0 && y > 0) // I квадрант
            {
                if (!insideCircle && insideRectangle)
                    inShadedRegion = true;
            }
            else if (x < 0 && y < 0) // III квадрант
            {
                if (insideCircle && insideRectangle)
                    inShadedRegion = true;
            }

            if (inShadedRegion)
            {
                return 1;
            }

            if (insideCircle)
            {
                return 2; // Внутри окружности, но не в закрашенной области
            }

            return 3; // Вне окружности и не в закрашенной области
        }
    }

    public class LR3
    {
        public static void Main(string[] args)
        {
        }
    }
}