using System;
using NUnit.Framework;

namespace Manipulation;

public class TriangleTask
{
    public static double GetABAngle(double a, double b, double c)
    {
        if (((a + b > c) && (a + c > b) && (b + c > a) is false) || (a <= 0 || b <= 0 || c <= 0))
            return double.NaN;

        var cosAB = (a * a + b * b - c * c) / (2 * a * b);

        return Math.Acos(cosAB);
    }
}

[TestFixture]
public class TriangleTask_Tests
{
    [TestCase(3, 4, 5, Math.PI / 2)]
    [TestCase(1, 1, 1, Math.PI / 3)]
    [TestCase(2, 2, 2, Math.PI / 3)] // Равносторонний треугольник
    [TestCase(5, 6, 5, Math.PI / 3)] // Равнобедренный треугольник
    public void TestGetABAngle(double a, double b, double c, double expectedAngle)
    {
        Assert.Fail("Not implemented yet");
    }
}