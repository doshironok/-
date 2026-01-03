using BlackBoxTesting;
using Variant10;
using FluentAssertions;
using NUnit.Framework;

namespace BlackBoxTesting.Tests
{
    [TestFixture]
    public class AreaTests
    {
        private IArea area;

        [SetUp]
        public void Setup()
        {
            area = new Area(5.0, 4.0); // R1=5, R2=4
        }

        // Тест 1: точка внутри левой области (x<0, y>0)
        [Test]
        public void Point_In_Left_Semicircle_Should_Return_True()
        {
            var result = area.IsPointInArea(-3.0, 4.0);
            result.Should().BeTrue();
        }

        // Тест 2: точка на границе левой области (x=0, y=R1) — ОШИБКА!
        [Test]
        public void Point_On_Left_Boundary_X0_YR1_Should_Return_True()
        {
            var result = area.IsPointInArea(0.0, 5.0);
            result.Should().BeTrue(); // упадёт, потому что x<0
        }

        // Тест 3: точка на оси X в левой области (x=-5, y=0) — ОШИБКА!
        [Test]
        public void Point_On_Left_Boundary_XNegative_Y0_Should_Return_True()
        {
            var result = area.IsPointInArea(-5.0, 0.0);
            result.Should().BeTrue(); // упадёт, потому что x<0
        }

        // Тест 4: точка внутри правого сектора (x>0, y<0)
        [Test]
        public void Point_In_Right_Sector_Should_Return_True()
        {
            var result = area.IsPointInArea(3.0, -2.0);
            result.Should().BeTrue();
        }

        // Тест 5: точка на границе правого сектора (x>0, y=0) — ОШИБКА!
        [Test]
        public void Point_On_Right_Boundary_XPositive_Y0_Should_Return_True()
        {
            var result = area.IsPointInArea(4.0, 0.0);
            result.Should().BeTrue(); // упадёт, потому что y<0
        }

        // Тест 6: точка в начале координат — ОШИБКА!
        [Test]
        public void Point_At_Origin_Should_Return_True()
        {
            var result = area.IsPointInArea(0.0, 0.0);
            result.Should().BeTrue(); // упадёт, потому что не попадает ни в одну область
        }

        // Тест 7: точка вне всех областей
        [Test]
        public void Point_Outside_All_Areas_Should_Return_False()
        {
            var result = area.IsPointInArea(6.0, 0.0);
            result.Should().BeFalse();
        }
    }
}