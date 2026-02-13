using System;

namespace Mazes;

public static class DiagonalMazeTask
{
    const int ShiftNumber = 3;
    const int StepToNext = 1;

    public static void MoveRobot(Robot robot, int stepCount, Direction direction)
    {
        for (int i = 0; i < stepCount; i++)
            robot.MoveTo(direction);
    }

    public static Direction GetDirectionToMove(int width, int height)
    {
        if (width > height)
            return Direction.Right;
        return Direction.Down;
    }

    public static int GetCountOfStepsForSector(int width, int height)
    {
        var maxSide = (double)Math.Max(width, height);
        var minSide = (double)Math.Min(width, height);
        return (int)Math.Round(maxSide / minSide);
    }

    public static int GetCountOfSectors(int width, int height)
    {
        var maxSide = Math.Max(width, height);
        var cellsForSector = GetCountOfStepsForSector(width, height);
        return (maxSide - ShiftNumber) / cellsForSector;
    }

    public static void MoveOut(Robot robot, int width, int height)
    {
        var countOfSteps = GetCountOfStepsForSector(width, height);
        var countOfSectors = GetCountOfSectors(width, height);
        var firstDirecion = GetDirectionToMove(width, height);
        var secondDirection = GetDirectionToMove(height, width);

        for (int i = 0; i < countOfSectors - 1; i++)
        {
            MoveRobot(robot, countOfSteps, firstDirecion);
            MoveRobot(robot, StepToNext, secondDirection);
        }

        MoveRobot(robot, countOfSteps, firstDirecion);
    }
}