using System;

namespace Fractals;

internal static class DragonFractalTask
{
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        const int ShiftNumber = 1;

        var x = 1.0;
        var y = 0.0;
        var random = new Random(seed);

        for (int i = 0; i < iterationsCount; i++)
        {
            double newX, newY;

            if (random.Next(2) == 0)
            {
                newX = (x * Math.Cos(Math.PI / 4) - y * Math.Sin(Math.PI / 4)) / Math.Sqrt(2);
                newY = (x * Math.Sin(Math.PI / 4) + y * Math.Cos(Math.PI / 4)) / Math.Sqrt(2);
            }
            else
            {
                newX = (x * Math.Cos(3 * Math.PI / 4) - y * Math.Sin(3 * Math.PI / 4)) / Math.Sqrt(2) + ShiftNumber;
                newY = (x * Math.Sin(3 * Math.PI / 4) + y * Math.Cos(3 * Math.PI / 4)) / Math.Sqrt(2);
            }

            x = newX; 
            y = newY;

            pixels.SetPixel(x, y);
        }
    }
}