using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class ManipulatorTask
{
    public static double[] MoveManipulatorTo(double x, double y, double alpha)
    {
        var wristX = x - Math.Cos(alpha) * Palm;
        var wristY = y + Math.Sin(alpha) * Palm;
        var shoulderToWrist = GetDistance(wristX, wristY, 0, 0);

        if (shoulderToWrist > UpperArm + Forearm ||
            shoulderToWrist < Math.Abs(UpperArm - Forearm))
            return new[] { double.NaN, double.NaN, double.NaN };

        var oXAngle = Math.Atan2(wristY, wristX);
        var elbowShoulderWristAngle = TriangleTask.GetABAngle(UpperArm, shoulderToWrist, Forearm);

        var elbowAngle = -(Math.PI - TriangleTask.GetABAngle(UpperArm, Forearm, shoulderToWrist));
        var shoulderAngle = oXAngle + (elbowShoulderWristAngle * (elbowAngle > 0 ? 1 : -1));
        var wristAngle = -1 * (alpha + elbowAngle + shoulderAngle);

        if (double.IsNaN(wristAngle))
            return new[] { double.NaN, double.NaN, double.NaN };

        return new[] { shoulderAngle, elbowAngle, wristAngle };
    }

    public static double GetDistance(double x1, double y1, double x2, double y2)
	{
		return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    }
}

[TestFixture]
public class ManipulatorTask_Tests
{
    [Test]
    public void TestMoveManipulatorTo()
    {
        var random = new Random();
        int testCount = 1000;

        for (int i = 0; i < testCount; i++)
        {
            var x = random.NextDouble() * (UpperArm + Forearm + Palm) * 2 - (UpperArm + Forearm + Palm);
            var y = random.NextDouble() * (UpperArm + Forearm + Palm) * 2 - (UpperArm + Forearm + Palm);
            var alpha = random.NextDouble() * Math.PI * 2 - Math.PI;

            var angles = ManipulatorTask.MoveManipulatorTo(x, y, alpha);
            var isReachable = IsPointReachable(x, y, alpha);

            if (isReachable)
            {
                ClassicAssert.IsFalse(double.IsNaN(angles[0]),
                    $"Point ({x}, {y}) with alpha={alpha} should be reachable but got NaN");
                ClassicAssert.IsFalse(double.IsNaN(angles[1]),
                    $"Point ({x}, {y}) with alpha={alpha} should be reachable but got NaN");
                ClassicAssert.IsFalse(double.IsNaN(angles[2]),
                    $"Point ({x}, {y}) with alpha={alpha} should be reachable but got NaN");

                var actual = AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2]);
                var palmEndPos = actual[2];
                var palmAngle = angles[0] + angles[1] + angles[2];

                const double epsilon = 1e-6;
                ClassicAssert.AreEqual(x, palmEndPos.X, epsilon,
                    $"X coordinate mismatch for point ({x}, {y}) with alpha={alpha}");
                ClassicAssert.AreEqual(y, palmEndPos.Y, epsilon,
                    $"Y coordinate mismatch for point ({x}, {y}) with alpha={alpha}");
                ClassicAssert.AreEqual(-alpha, palmAngle, epsilon,
                    $"Alpha mismatch for point ({x}, {y}) with alpha={alpha}");
            }
            else
            {
                ClassicAssert.IsTrue(double.IsNaN(angles[0]) && double.IsNaN(angles[1]) && double.IsNaN(angles[2]),
                    $"Point ({x}, {y}) with alpha={alpha} should not be reachable but got angles: " +
                    $"[{angles[0]}, {angles[1]}, {angles[2]}]");
            }
        }
    }

    private static bool IsPointReachable(double x, double y, double alpha)
    {
        var wristX = x - Math.Cos(alpha) * Palm;
        var wristY = y + Math.Sin(alpha) * Palm;
        var shoulderToWrist = Math.Sqrt(wristX * wristX + wristY * wristY);

        var minDistance = Math.Abs(UpperArm - Forearm);
        var maxDistance = UpperArm + Forearm;

        return shoulderToWrist >= minDistance && shoulderToWrist <= maxDistance;
    }
}