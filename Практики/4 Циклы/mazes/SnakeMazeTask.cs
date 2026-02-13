namespace Mazes;

public static class SnakeMazeTask
{
    const int ShiftNum = 3;
    const int StepThroughOne = 2;

    public static void MoveRobot(Robot robot, int stepCount, Direction direction)
    {
        for (int i = 0; i < stepCount; i++)
            robot.MoveTo(direction);
    }

    public static void MoveOneSector(Robot robot, int width)
    {
        MoveRobot(robot, width - ShiftNum, Direction.Right);
        MoveRobot(robot, StepThroughOne, Direction.Down);
        MoveRobot(robot, width - ShiftNum, Direction.Left);
    }

    public static void MoveOut(Robot robot, int width, int height)
    {
        var countOfSectors = height / 4;

        for (int i = 0; i < countOfSectors - 1; i++)
        {
            MoveOneSector(robot, width);
            MoveRobot(robot, StepThroughOne, Direction.Down);
        }

        MoveOneSector(robot, width);
    }
}