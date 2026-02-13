using System.Xml.Linq;

namespace Names;

internal static class HeatmapTask
{
    public static string[] MakeNumerateArray(int startNum, int endNum)
    {
        var numerateArray = new string[endNum];

        for (int i = startNum; i < endNum + startNum; i++)
            numerateArray[i - startNum] = i.ToString();

        return numerateArray;
    }

    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        var daysArray = MakeNumerateArray(2, 30);
        var monthsArray = MakeNumerateArray(1, 12);
        var birthRate = new double[30, 12];


        //for (int i = SecondDay; i < dayNum.Length + SecondDay; i++)
        //    dayNum[i - SecondDay] = i.ToString();

        //for (int i = 1; i < monthNum.Length + 1; i++)
        //    monthNum[i - 1] = i.ToString();

        foreach (var e in names)
        {
            if (e.BirthDate.Day != 1)
                 birthRate[e.BirthDate.Day - 2, e.BirthDate.Month - 1]++;
        }

        return new HeatmapData(
            "Пример карты интенсивностей",
            birthRate,
            daysArray,
            monthsArray);
    }
}