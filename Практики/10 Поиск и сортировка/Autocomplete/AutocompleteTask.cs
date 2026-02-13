using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
	/// <returns>
	/// Возвращает первую фразу словаря, начинающуюся с prefix.
	/// </returns>
	/// <remarks>
	/// Эта функция уже реализована, она заработает, 
	/// как только вы выполните задачу в файле LeftBorderTask
	/// </remarks>
	public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
		if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
			return phrases[index];
            
		return null;
	}

	/// <returns>
	/// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
	/// элементов словаря, начинающихся с prefix.
	/// </returns>
	/// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
	public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
	{
		var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count()) + 1;
		var result = new List<string>();

		for (int index = left; index < phrases.Count() && result.Count() < count; index++)
		{
			if (phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
				result.Add(phrases[index]);
			else break;
		}

		return result.ToArray();
	}

	/// <returns>
	/// Возвращает количество фраз, начинающихся с заданного префикса
	/// </returns>
	public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
	{
		var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count()) + 1;
		var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count());

		return right - left;
    }
}

[TestFixture]
public class AutocompleteTests
{
	[Test]
	public void TopByPrefix_IsEmpty_WhenNoPhrases()
	{
		// ...
		//CollectionAssert.IsEmpty(actualTopWords);
	}

	// ...

	[Test]
	public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
	{
		// ...
		//Assert.AreEqual(expectedCount, actualCount);
	}

	// ...
}
