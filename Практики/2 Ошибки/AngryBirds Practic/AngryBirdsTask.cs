using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
    const double G = 9.8;
    public static double FindSightAngle(double v, double distance)
    {
        return 0.5 * Math.Asin((distance * G) / (v * v));
    }

    static void Main()
    {
        double v = double.Parse(Console.ReadLine());
        double distance = double.Parse(Console.ReadLine());
        double angle = FindSightAngle(v, distance);

        Console.WriteLine(angle);
    }
}