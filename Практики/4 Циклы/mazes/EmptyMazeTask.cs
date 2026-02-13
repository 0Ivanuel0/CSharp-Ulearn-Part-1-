namespace Mazes;

public static class EmptyMazeTask
{
    const int ShiftNum = 3;

    public static void MoveOut(Robot robot, int width, int height)
    {
        MoveRobot(robot, height - ShiftNum, Direction.Down);
        MoveRobot(robot, width - ShiftNum, Direction.Right);
    }

    public static void MoveRobot(Robot robot, int stepCount, Direction direction)
    {
        for (int i = 0; i < stepCount; i++)
            robot.MoveTo(direction);
    }
}