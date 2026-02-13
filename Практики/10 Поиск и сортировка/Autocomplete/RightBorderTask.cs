using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Autocomplete;


public class RightBorderTask
{
    /// <returns>
    /// Возвращает индекс правой границы. 
    /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
    /// Если такого нет, то возвращает items.Length
    /// </returns>
    /// <remarks>
    /// Функция должна быть НЕ рекурсивной
    /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
    /// </remarks>
    public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        while (left + 1 < right)
        {
            var middle = left + (right - left) / 2;

            if (string.Compare(prefix, phrases[middle], StringComparison.InvariantCultureIgnoreCase) >= 0
                || phrases[middle].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                left = middle;

            else right = middle;
        }

        return right;
    }
}