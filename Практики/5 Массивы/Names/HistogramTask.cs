namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        const int FirstDay = 1;

        var daysNum = new string[31];
        var birthRate = new double[31];

        for (int i = FirstDay; i < 31 + FirstDay; i++)
            daysNum[i - FirstDay] = i.ToString();

        foreach (var e in names)
            if (e.Name == name && e.BirthDate.Day != FirstDay)
                birthRate[e.BirthDate.Day - FirstDay] += 1;

        return new HistogramData(
            $"Рождаемость людей с именем '{name}'", 
            daysNum,
            birthRate);
    }
}