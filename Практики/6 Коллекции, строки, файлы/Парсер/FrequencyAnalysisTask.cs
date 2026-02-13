namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var countOfNGrams = new Dictionary<string, Dictionary<string, int>>();

        foreach (var sentence in text)
        {
            if (sentence.Count < 2) continue;

            for (var i = 0; i < sentence.Count - 1; i++)
                AddGram(countOfNGrams, sentence[i], sentence[i + 1]);

            for (var i = 0; i < sentence.Count - 2; i++)
                AddGram(countOfNGrams, (sentence[i] + ' ' + sentence[i + 1]), sentence[i + 2]);
        }

        ChooseNextWord(countOfNGrams, result);

        return result;
    }

    public static void AddGram(Dictionary<string, Dictionary<string, int>> dictionary, string firstWord, string secondWord)
    {
        if (dictionary.ContainsKey(firstWord) is false)
            dictionary[firstWord] = new Dictionary<string, int>();

        if (dictionary[firstWord].ContainsKey(secondWord) is false)
            dictionary[firstWord][secondWord] = 0;

        dictionary[firstWord][secondWord] += 1;
    }

    public static void ChooseNextWord(Dictionary<string, Dictionary<string, int>> countOfNGrams, Dictionary<string, string> result)
    {
        foreach (var entry in countOfNGrams)
        {
            var maxCount = 0;
            var nextWord = "";

            foreach (var item in entry.Value)
            {
                var word = item.Key;
                var count = item.Value;

                if (count > maxCount)
                {
                    maxCount = count;
                    nextWord = word;
                }
                else if (count == maxCount && String.CompareOrdinal(word, nextWord) < 0)
                    nextWord = word;
            }

            result[entry.Key] = nextWord;
        }
    }
}