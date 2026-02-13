using System;
using Avalonia.Input;
using Avalonia.Media;
using Digger.Architecture;

namespace Digger;

public class Terrain : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand();
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return true;
    }

    public int GetDrawingPriority()
    {
        return 0;
    }

    public string GetImageFileName()
    {
        return "Terrain.png";
    }
}

public class Player : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var command = new CreatureCommand();

        switch (Game.KeyPressed)
        {
            case Key.Left:
                if ((Game.Map[Math.Max(x - 1, 0), y] is Sack) is false)
                    command.DeltaX = x > 0 ? -1 : 0;
                break;

            case Key.Right:
                if ((Game.Map[Math.Min(x + 1, Game.MapWidth - 1), y] is Sack) is false)
                    command.DeltaX = x + 1 < Game.MapWidth ? 1 : 0;
                break;

            case Key.Up:
                if ((Game.Map[x, Math.Max(y - 1, 0)] is Sack) is false)
                    command.DeltaY = y > 0 ? -1 : 0;
                break;

            case Key.Down:
                if ((Game.Map[x, Math.Min(y + 1, Game.MapHeight - 1)] is Sack) is false)
                    command.DeltaY = y + 1 < Game.MapHeight ? 1 : 0;
                break;
        }

        return command;
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        if (conflictedObject is Gold) Game.Scores += 10;
        else if (conflictedObject is Sack || conflictedObject is Monster) return true;
        return false;
    }

    public int GetDrawingPriority()
    {
        return 1;
    }

    public string GetImageFileName()
    {
        return "Digger.png";
    }
}


public class Sack : ICreature
{
    public bool WasFalling { get; private set; }
    public int FallDistance { get; private set; }

    public CreatureCommand Act(int x, int y)
    {
        var command = new CreatureCommand();

        if (y + 1 < Game.MapHeight)
        {
            var belowSack = Game.Map[x, y + 1];

            if (belowSack == null)
                MoveDown(command);
            else if (WasFalling && (belowSack is Player || belowSack is Monster))
                MoveDown(command);
            else TryTransformAfterFalling(command);
        }
        else TryTransformAfterFalling(command);

        return command;
    }

    private void MoveDown(CreatureCommand command)
    {
        command.DeltaY = 1;
        FallDistance += 1;
        WasFalling = true;
    }

    private void TryTransformAfterFalling(CreatureCommand command)
    {
        if (WasFalling && FallDistance > 1)
            command.TransformTo = new Gold();
        WasFalling = false;
        FallDistance = 0;
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Gold;
    }

    public int GetDrawingPriority()
    {
        return 2;
    }

    public string GetImageFileName()
    {
        return "Sack.png";
    }
}

public class Gold : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        return new CreatureCommand();
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return true;
    }

    public int GetDrawingPriority()
    {
        return 3;
    }

    public string GetImageFileName()
    {
        return "Gold.png";
    }
}

public class Monster : ICreature
{
    public CreatureCommand Act(int x, int y)
    {
        var command = new CreatureCommand();
        var playerPosition = GetPlayerPosition(Game.MapWidth, Game.MapHeight);

        if (playerPosition is null)
            return new CreatureCommand();

        var playerX = playerPosition[0];
        var playerY = playerPosition[1];

        if (playerX < x && CanToMove(x, y, -1, 0)) command.DeltaX = -1;
        else if (playerX > x && CanToMove(x, y, 1, 0)) command.DeltaX = 1;
        else if (playerY < y && CanToMove(x, y, 0, -1)) command.DeltaY = -1;
        else if (playerY > y && CanToMove(x, y, 0, 1)) command.DeltaY = 1;
        else return new CreatureCommand();

        return command;
    }

    private int[] GetPlayerPosition(int width, int height)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (Game.Map[x, y] is Player)
                    return new int[] { x, y };
        return null;
    }

    private bool CanToMove(int x, int y, int dx, int dy)
    {
        return
            Game.Map[x + dx, y + dy] is not Terrain &&
            Game.Map[x + dx, y + dy] is not Monster &&
            Game.Map[x + dx, y + dy] is not Sack;
    }

    public bool DeadInConflict(ICreature conflictedObject)
    {
        return conflictedObject is Monster || conflictedObject is Sack;
    }

    public int GetDrawingPriority()
    {
        return 1;
    }

    public string GetImageFileName()
    {
        return "Monster.png";
    }
}
