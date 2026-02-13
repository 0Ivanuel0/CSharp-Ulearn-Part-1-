using System.Drawing;

namespace RoutePlanning;

public static class PathFinderTask
{
    private static int[] bestOrder;
    private static double bestLength = 100000;
    
	public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
	{
		bestOrder = MakeTrivialPermutation(checkpoints.Length);
		bestLength = checkpoints.GetPathLength();
		return bestOrder;
	}

	private static int[] MakeTrivialPermutation(int size)
	{
		var bestOrder = new int[size];
		for (var i = 0; i < bestOrder.Length; i++)
			bestOrder[i] = i;
		return bestOrder;
	}
}