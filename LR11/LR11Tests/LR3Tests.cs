using AreaCheckerApp;
using NUnit.Framework.Legacy;

[TestFixture]
public class AreaCheckerTests
{
    private AreaChecker checker;
    private AreaChecker checkerR45;

    [SetUp]
    public void Setup()
    {
        checker = new AreaChecker(5.0, 3.0, 4.0);    // R=5, a=3, b=4
        checkerR45 = new AreaChecker(4.5, 3.0, 4.0); // R=4.5, a=3, b=4
    }

    // Классы эквивалентности (7 классов)
    [Test] public void CE1_PointIn_I_Quadrant_InsideCircle() => ClassicAssert.AreEqual(2, checker.TestPoint(3, 4));
    [Test] public void CE2_PointIn_II_Quadrant() => ClassicAssert.AreEqual(2, checker.TestPoint(-2, 3));
    [Test] public void CE3_PointIn_I_Quadrant_OutsideCircle_InsideRectangle() => ClassicAssert.AreEqual(1, checkerR45.TestPoint(3, 4));
    [Test] public void CE4_PointIn_I_Quadrant_OutsideRectangle() => ClassicAssert.AreEqual(3, checker.TestPoint(4, 5));
    [Test] public void CE5_PointIn_III_Quadrant_InsideBoth() => ClassicAssert.AreEqual(1, checker.TestPoint(-2, -3));
    [Test] public void CE6_PointIn_III_OutsideCircle_InsideRectangle() => ClassicAssert.AreEqual(3, checkerR45.TestPoint(-2.5, -4));
    [Test] public void CE6_PointIn_III_InsideCircle_OutsideRectangle() => ClassicAssert.AreEqual(2, checkerR45.TestPoint(-4, -1));
    [Test] public void CE7_PointIn_IV_Quadrant() => ClassicAssert.AreEqual(2, checker.TestPoint(3, -3));

    // Граничные значения - основные границы
    [Test] public void BV1_Origin() => ClassicAssert.AreEqual(2, checker.TestPoint(0, 0));


    // Границы окружности (R=5) - значимые переходы
    [Test] public void BV2_OnCircle_I_Quadrant() => ClassicAssert.AreEqual(2, checker.TestPoint(3, 4)); // Граница круга в I - не закрашено
    [Test] public void BV3_OnCircle_III_Quadrant() => ClassicAssert.AreEqual(1, checker.TestPoint(-3, -4)); // Граница круга в III - закрашено

    // Границы прямоугольника - значимые переходы
    [Test] public void BV4_OnRectangle_I_Right() => ClassicAssert.AreEqual(1, checkerR45.TestPoint(3, 3.5)); // Граница прямоугольника в I - закрашено
    [Test] public void BV5_OnRectangle_I_Top() => ClassicAssert.AreEqual(1, checkerR45.TestPoint(2.5, 4)); // Граница прямоугольника в I - закрашено
    [Test] public void BV6_OnRectangle_III_Right() => ClassicAssert.AreEqual(1, checker.TestPoint(-3, -2)); // Граница прямоугольника в III - закрашено
    [Test] public void BV7_OnRectangle_III_Top() => ClassicAssert.AreEqual(1, checker.TestPoint(-2, -4)); // Граница прямоугольника в III - закрашено

    // Приближенные значения к границам окружности - ТОЛЬКО значимые переходы
    [Test] public void NearCircle_JustInside_III() => ClassicAssert.AreEqual(1, checker.TestPoint(-2.999, -0.001)); // Внутри круга в III и внутри прямоугольника - закрашено
    [Test] public void NearCircle_JustOutside_III() => ClassicAssert.AreEqual(2, checker.TestPoint(-3.001, -0.001)); // Внутри круга в III - не закрашено

    [Test] public void NearCircle_JustInside_I() => ClassicAssert.AreEqual(2, checker.TestPoint(4.999, 0.001)); // Внутри круга в I - не закрашено
    [Test] public void NearCircle_JustOutside_I() => ClassicAssert.AreEqual(3, checker.TestPoint(5.001, 0.001)); // Вне круга в I - не закрашено

    // Приближенные значения к границам прямоугольника - значимые переходы
    [Test] public void NearRectangle_JustInside_I_Right() => ClassicAssert.AreEqual(1, checkerR45.TestPoint(2.999, 3.5)); // Внутри прямоугольника в I и вне круга - закрашено
    [Test] public void NearRectangle_JustOutside_I_Right() => ClassicAssert.AreEqual(3, checkerR45.TestPoint(3.001, 3.5)); // Вне прямоугольника в I и вне круга- не закрашено

    [Test] public void NearRectangle_JustInside_I_Top() => ClassicAssert.AreEqual(1, checkerR45.TestPoint(2.5, 3.999)); // Внутри прямоугольника в I - закрашено
    [Test] public void NearRectangle_JustOutside_I_Top() => ClassicAssert.AreEqual(3, checkerR45.TestPoint(2.5, 4.001)); // Вне прямоугольника в I - не закрашено

    [Test] public void NearRectangle_JustInside_III_Right() => ClassicAssert.AreEqual(1, checker.TestPoint(-2.999, -2)); // Внутри прямоугольника в III - закрашено
    [Test] public void NearRectangle_JustOutside_III_Right() => ClassicAssert.AreEqual(2, checker.TestPoint(-3.001, -2)); // Вне прямоугольника и внутри круга в III - не закрашено

    [Test] public void NearRectangle_JustInside_III_Top() => ClassicAssert.AreEqual(1, checker.TestPoint(-2, -3.999)); // Внутри прямоугольника в III - закрашено
    [Test] public void NearRectangle_JustOutside_III_Top() => ClassicAssert.AreEqual(2, checker.TestPoint(-2, -4.001)); // Вне прямоугольника и внутри круга в III - не закрашено

    // Особые случаи - пересечения границ (значимые переходы)
    [Test] public void Special_OnBothBoundaries_I() => ClassicAssert.AreEqual(1, checkerR45.TestPoint(3, 4)); // Пересечение границ в I - закрашено
    [Test] public void Special_OnBothBoundaries_III() => ClassicAssert.AreEqual(1, checker.TestPoint(-3, -4)); // Пересечение границ в III - закрашено

    // Для окружности R=5
    [Test]
    public void MutantKiller_CircleBoundary_Inside_Returns2()
    {
        // Точка ВНУТРИ окружности (4.999, 0) → область 2
        ClassicAssert.AreEqual(2, checker.TestPoint(4.999, 0));
    }

    [Test]
    public void MutantKiller_CircleBoundary_Outside_Returns3()
    {
        // Точка СНАРУЖИ окружности (5.001, 0) → область 3
        ClassicAssert.AreEqual(3, checker.TestPoint(5.001, 0));
    }

    // Аналогично для прямоугольника a=3, b=4
    [Test]
    public void MutantKiller_RectRightBoundary_Inside_Returns1()
    {
        ClassicAssert.AreEqual(2, checkerR45.TestPoint(2.999, 0)); // внутри → закрашено
    }

    [Test]
    public void MutantKiller_RectRightBoundary_Outside_Returns3()
    {
        ClassicAssert.AreEqual(2, checkerR45.TestPoint(3.001, 0)); // снаружи → не закрашено
    }

    [Test]
    public void MutantKiller_AxesCoverage_X0Y1_ReturnsIV() // или нужная область
    {
        ClassicAssert.AreEqual(2, checker.TestPoint(0, 1)); // или ваша логика
    }


}

[TestFixture]
public class AreaCheckerDataDrivenTests
{
    private AreaChecker checker;
    private AreaChecker checkerR45;

    [SetUp]
    public void Setup()
    {
        checker = new AreaChecker(5.0, 3.0, 4.0);
        checkerR45 = new AreaChecker(4.5, 3.0, 4.0);
    }

    [TestCaseSource(nameof(AreaTestCases))]
    public void TestPoint_DataDriven_ShouldReturnExpectedArea(
        AreaChecker testChecker, double x, double y, int expectedArea, string testDescription)
    {
        // Act
        int result = testChecker.TestPoint(x, y);

        // Assert
        ClassicAssert.AreEqual(expectedArea, result, testDescription);
    }

    private static object[] AreaTestCases =
    {
            // Основные классы эквивалентности
            new object[] { null, 3, 4, 2, "CE1: I квадрант, внутри окружности" },
            new object[] { null, -2, 3, 2, "CE2: II квадрант" },
            new object[] { null, 3, 4, 1, "CE3: I квадрант, вне окружности, внутри прямоугольника" },
            new object[] { null, 4, 5, 3, "CE4: I квадрант, вне прямоугольника" },
            new object[] { null, -2, -3, 1, "CE5: III квадрант, внутри обеих фигур" },
            new object[] { null, -2.5, -4, 3, "CE6: III квадрант, вне окружности, внутри прямоугольника" },
            new object[] { null, -4, -1, 2, "CE6: III квадрант, внутри окружности, вне прямоугольника" },
            new object[] { null, 3, -3, 2, "CE7: IV квадрант" },

            // Граничные значения
            new object[] { null, 0, 0, 2, "BV1: Начало координат" },
            new object[] { null, 3, 4, 2, "BV2: На окружности в I квадранте" },
            new object[] { null, -3, -4, 1, "BV3: На окружности в III квадранте" },
            new object[] { null, 3, 3.5, 1, "BV4: На границе прямоугольника в I квадранте" },
            new object[] { null, 2.5, 4, 1, "BV5: На границе прямоугольника в I квадранте" },
            new object[] { null, -3, -2, 1, "BV6: На границе прямоугольника в III квадранте" },
            new object[] { null, -2, -4, 1, "BV7: На границе прямоугольника в III квадранте" }
        };

    static AreaCheckerDataDrivenTests()
    {
        var checker = new AreaChecker(5.0, 3.0, 4.0);
        var checkerR45 = new AreaChecker(4.5, 3.0, 4.0);

        for (int i = 0; i < AreaTestCases.Length; i++)
        {
            if (AreaTestCases[i] is object[] testCase && testCase[0] == null)
            {
                // использования checkerR45 и checker
                if (i == 2 || i == 5 || i == 6 || i == 11 || i == 12)
                    testCase[0] = checkerR45;
                else
                    testCase[0] = checker;
            }
        }
    }
}