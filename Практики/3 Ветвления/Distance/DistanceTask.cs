using System;
using System.Data;

namespace DistanceTask;

public static class DistanceTask
{
    // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)

    public static double CalculateLength(double x1, double y1, double x2, double y2)
    {
        return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
    }

    public static double FindPerpendicular(double AB, double BC, double AC)
    {
        var semiArea = (AB + BC + AC) / 2;
        var square = Math.Sqrt(semiArea * (semiArea - AB) * (semiArea - BC) * (semiArea - AC));

        if (AB != 0)
            return 2 * square / AB;

        return 0;
    }

    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
        if (ax == bx && ay == by)
            return CalculateLength(ax, ay, x, y);

        var AC = CalculateLength(ax, ay, x, y);
        var BC = CalculateLength(bx, by, x, y);
        var AB = CalculateLength(ax, ay, bx, by);

        double cosA = (AC * AC + AB * AB - BC * BC) / (2 * AC * AB);
        double cosB = (BC * BC + AB * AB - AC * AC) / (2 * BC * AB);

        if (cosA >= 0 && cosB >= 0)
            return FindPerpendicular(AB, BC, AC);

        return Math.Min(AC, BC);
    }
}