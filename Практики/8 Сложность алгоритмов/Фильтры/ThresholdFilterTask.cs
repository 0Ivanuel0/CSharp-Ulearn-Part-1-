using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        var rows = original.GetLength(0);
        var columns = original.GetLength(1);
        var requiredWhite = (int)(rows * columns * whitePixelsFraction);

        var threshold = GetThreshold(original, requiredWhite);
        var result = new double[rows, columns];

        for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
            {
                if (original[i, j] >= threshold)
                    result[i, j] = 1.0;
                else result[i, j] = 0.0;
            }

        return result;
    }

    public static double GetThreshold(double[,] original, int requiredWhite)
    {
        var rows = original.GetLength(0);
        var columns = original.GetLength(1);

        if (requiredWhite <= 0)
            return 256;

        if (requiredWhite >= columns * rows)
            return -1;

        var listOfPixels = new List<double>(rows * columns);

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                listOfPixels.Add(original[i, j]);

        listOfPixels.Sort();
        listOfPixels.Reverse();

        return listOfPixels[requiredWhite - 1];
    }
}
