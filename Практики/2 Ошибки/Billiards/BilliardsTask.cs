using System;

namespace Billiards;

public static class BilliardsTask
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="directionRadians">Угол направления движения шара</param>
    /// <param name="wallInclinationRadians">Угол наклона стены</param>
    /// <returns></returns>
    public static double BounceWall(double directionRadians, double wallInclinationRadians)
    {
        //TODO
        return (wallInclinationRadians - directionRadians) + wallInclinationRadians;
    }

    static void Main()
    {
        double directionRadians = double.Parse(Console.ReadLine());
        double wallInclinationRadians = double.Parse(Console.ReadLine());
        double angle = BounceWall(directionRadians, wallInclinationRadians);

        Console.WriteLine(angle);
    }
}