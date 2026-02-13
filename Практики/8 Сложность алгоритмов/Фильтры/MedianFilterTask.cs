using System;
using System.Collections.Generic;

namespace Recognizer;

internal static class MedianFilterTask
{
    public static double[,] MedianFilter(double[,] original)
    {
        var rows = original.GetLength(0);
        var columns = original.GetLength(1);
        var newOriginal = new double[rows, columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
            {
                var startX = Math.Max(0, i - 1);
                var endX = Math.Min(rows - 1, i + 1);
                var startY = Math.Max(0, j - 1);
                var endY = Math.Min(columns - 1, j + 1);

                newOriginal[i, j] = GetPixelMedian(original, startX, endX, startY, endY);
            }

        return newOriginal;
    }

    public static double GetPixelMedian(double[,] original, int startX, int endX, int startY, int endY)
    {
        var pixelValue = new List<double>();

        for (var i = startX; i <= endX; i++)
            for (var j = startY; j <= endY; j++)
                pixelValue.Add(original[i, j]);

        return CalculateMedian(pixelValue);
    }

    private static double CalculateMedian(List<double> pixelValue)
    {
        pixelValue.Sort();

        if (pixelValue.Count % 2 != 0)
            return pixelValue[pixelValue.Count / 2];

        return (pixelValue[(pixelValue.Count / 2) - 1] + pixelValue[pixelValue.Count / 2]) / 2;
    }
}
