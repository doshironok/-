using System;

namespace Variant10
{
    public class Area : BlackBoxTesting.IArea
    {
        private readonly double R1;
        private readonly double R2;

        public Area(double r1, double r2)
        {
            R1 = r1;
            R2 = r2;
        }

        public bool IsPointInArea(double x, double y)
        {
            // Ошибка 1: вместо x <= 0 используется x < 0 → точка (0, y) не попадает в левую область
            if (x < 0 && Math.Sqrt(x * x + y * y) <= R1)
                return true;

            // Ошибка 2: вместо y <= 0 используется y < 0 → точка (x, 0) не попадает в правый сектор
            if (x >= 0 && y < 0 && Math.Sqrt(x * x + y * y) <= R2)
                return true;

            // Ошибка 3: отсутствие проверки точки (0,0) — она должна быть в обеих областях
            // Но из-за ошибок выше — не попадает никуда

            return false;
        }
    }
}