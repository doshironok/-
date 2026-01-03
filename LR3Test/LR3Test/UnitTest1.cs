using AreaCheckerApp;
using NUnit.Framework;
using System;

[TestFixture]
public class AreaCheckerTests
{
    private AreaChecker checker;
    private AreaChecker checkerR45;

    [SetUp]
    public void Setup()
    {
        checker = new AreaChecker(5.0, 3.0, 4.0);
        checkerR45 = new AreaChecker(4.5, 3.0, 4.0);
    }

    // ───────────────────────────────────────
    // → Классы эквивалентности (7 шт)
    // ───────────────────────────────────────

    [Test] public void CE1_PointIn_I_Quadrant() => Assert.AreEqual(2, checker.TestPoint(3, 4)); // внутри круга → Область 2
    [Test] public void CE2_PointIn_II_Quadrant_InsideCircle() => Assert.AreEqual(2, checker.TestPoint(-2, 3)); // внутри круга → Область 2
    [Test] public void CE3_PointIn_I_Quadrant_OutsideCircle() => Assert.AreEqual(1, checkerR45.TestPoint(3, 4)); // R=4.5 → вне круга, внутри прямоугольника → Область 1
    [Test] public void CE4_PointIn_III_Quadrant() => Assert.AreEqual(1, checker.TestPoint(-3, -4)); // внутри круга → Область 1
    [Test] public void CE5_PointIn_IV_Quadrant_InsideCircle() => Assert.AreEqual(2, checker.TestPoint(3, -4)); // внутри круга → Область 2
    [Test] public void CE6_PointIn_I_Quadrant_OutsideCircle_InsideStrip() => Assert.AreEqual(1, checkerR45.TestPoint(3, 4)); // уже есть
    [Test] public void CE7_PointIn_IV_Quadrant_OutsideStrip() => Assert.AreEqual(3, checker.TestPoint(3, -5)); // вне полоски → Область 3

    // ───────────────────────────────────────
    // → Граничные значения (20 шт)
    // ───────────────────────────────────────

    [Test] public void BV1_Origin() => Assert.AreEqual(2, checker.TestPoint(0, 0));
    [Test] public void BV2_Between_I_II_Inside() => Assert.AreEqual(2, checker.TestPoint(-0.1, 4.9));

    [Test]
    public void BV3_Between_I_II_OnCircle()
    {
        double x = -0.1;
        double y = Math.Sqrt(25 - x * x); // ≈ 4.999
        Assert.AreEqual(2, checker.TestPoint(x, y));
    }

    [Test] public void BV4_Between_I_II_Outside() => Assert.AreEqual(3, checker.TestPoint(-0.1, 5.1));
    [Test] public void BV5_I_OnCircle() => Assert.AreEqual(2, checker.TestPoint(3, 4)); // на окружности → не закрашена
    [Test] public void BV6_Between_II_III_Inside() => Assert.AreEqual(2, checker.TestPoint(-4.9, -0.1));

    [Test]
    public void BV7_Between_II_III_OnCircle()
    {
        double y = -0.1;
        double x = -Math.Sqrt(25 - y * y); // ≈ -4.999
        Assert.AreEqual(2, checker.TestPoint(x, y));
    }

    [Test] public void BV8_Between_II_III_Outside() => Assert.AreEqual(3, checker.TestPoint(-5.1, -0.1));
    [Test] public void BV9_Between_III_IV_Inside() => Assert.AreEqual(2, checker.TestPoint(0.1, -4.9));
    [Test] public void BV10_Between_III_IV_OnCircleAndStrip() => Assert.AreEqual(2, checker.TestPoint(3, -4)); // не закрашена
    [Test] public void BV11_Between_III_IV_InsideStrip() => Assert.AreEqual(2, checker.TestPoint(2, -3)); // не закрашена
    [Test] public void BV12_Between_III_IV_OnTopOfStrip() => Assert.AreEqual(2, checker.TestPoint(2, -4)); // не закрашена
    [Test] public void BV13_Between_III_IV_OutsideStrip() => Assert.AreEqual(3, checker.TestPoint(2, -4.1));
    [Test] public void BV14_III_OnBottomOfStrip() => Assert.AreEqual(1, checker.TestPoint(-2, -4)); // внутри круга → Область 1
    [Test] public void BV15_III_OnTopOfStrip() => Assert.AreEqual(1, checker.TestPoint(-2, -0.1)); // внутри круга → Область 1
    [Test] public void BV16_Between_IV_I_Inside() => Assert.AreEqual(2, checker.TestPoint(4.9, 0.1));
    [Test] public void BV17_Between_IV_I_OnCircleAndStrip() => Assert.AreEqual(2, checker.TestPoint(5, 0));
    [Test] public void BV18_Between_IV_I_InsideStrip() => Assert.AreEqual(2, checker.TestPoint(4, 0.1));
    [Test] public void BV19_Between_IV_I_OnTopOfStrip() => Assert.AreEqual(2, checker.TestPoint(4, 0));
    [Test] public void BV20_Between_IV_I_OutsideStrip() => Assert.AreEqual(3, checker.TestPoint(6, 0.1));

    // ───────────────────────────────────────
    // → Дополнительные тесты: близкие к границам (8 шт)
    // ───────────────────────────────────────

    [Test] public void NearYAxis_II_Side() => Assert.AreEqual(2, checker.TestPoint(-0.001, 4.999));
    [Test] public void NearYAxis_I_Side() => Assert.AreEqual(2, checker.TestPoint(0.001, 4.999));
    [Test] public void NearXAxis_II_Side() => Assert.AreEqual(2, checker.TestPoint(-4.999, 0.001));
    [Test] public void NearXAxis_III_Side() => Assert.AreEqual(1, checker.TestPoint(-4.999, -0.001)); // III квадрант, внутри круга → Область 1
    [Test] public void NearYAxis_III_Side() => Assert.AreEqual(1, checker.TestPoint(-0.001, -4.999)); // III квадрант, внутри круга → Область 1
    [Test] public void NearYAxis_IV_Side() => Assert.AreEqual(2, checker.TestPoint(0.001, -4.999)); // IV квадрант → Область 2
    [Test] public void NearXAxis_IV_Side() => Assert.AreEqual(2, checker.TestPoint(4.999, -0.001)); // IV квадрант → Область 2
    [Test] public void NearXAxis_I_Side() => Assert.AreEqual(2, checker.TestPoint(4.999, 0.001)); // I квадрант, внутри круга → Область 2
}