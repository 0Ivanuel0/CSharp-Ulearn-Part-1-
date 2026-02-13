using System;
using System.Collections.Generic;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];

        var sy = GetTransposedMatrix(sx);
        var localRadius = sx.GetLength(0) / 2;

        for (int x = localRadius; x < width - localRadius; x++)
            for (int y = localRadius; y < height - localRadius; y++)
            {
                var localMatrix = GetLocalMatrix(g, sx, x, y);

                var sxMultLocal = GetMatrixMult(sx, localMatrix);
                var syMultLocal = GetMatrixMult(sy, localMatrix);

                var gx = SumElements(sxMultLocal);
                var gy = SumElements(syMultLocal);

                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }

        return result;
    }

    private static double[,] GetTransposedMatrix(double[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);
        var transposedMatrix = new double[columns, rows];

        for (var i = 0; i < rows; i++)
            for (var j = 0; j < columns; j++)
                transposedMatrix[j, i] = matrix[i, j];

        return transposedMatrix;
    }

    private static double[,] GetMatrixMult(double[,] firstMatrix, double[,] secondMatrix)
    {
        var rows = firstMatrix.GetLength(0);
        var columns = firstMatrix.GetLength(1);
        var matrixMult = new double[rows, columns];

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                matrixMult[i, j] = firstMatrix[i, j] * secondMatrix[i, j];

        return matrixMult;
    }

    private static double[,] GetLocalMatrix(double[,] matrix, double[,] sx, int x, int y)
    {
        var localRadius = sx.GetLength(0) / 2;
        var rows = sx.GetLength(0);
        var columns = sx.GetLength(1);
        var localMatrix = new double[columns, rows];

        for (int i = -localRadius; i <= localRadius; i++)
            for (int j = -localRadius; j <= localRadius; j++)
                localMatrix[i + localRadius, j + localRadius] = matrix[x + i, y + j];

        return localMatrix;
    }

    private static double SumElements(double[,] matrix)
    {
        var sum = (double)0;
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
            for (int j = 0; j < columns; j++)
                sum += matrix[i, j];

        return sum;
    }
}
