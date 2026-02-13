using System.Globalization;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase( Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            var startWords = phraseBeginning.Split(' ').ToList();

            for (int i = 0; i < wordsCount; i++)
            {
                var nextWord = GetNextWord(startWords, nextWords);

                if (nextWord == null)
                    break;

                startWords.Add(nextWord);
            }

            return string.Join(" ", startWords);
        }

        private static string GetNextWord(List<string> startWords, Dictionary<string, string> nextWords)
        {
            var oneWordKey = startWords[startWords.Count - 1];

            if (startWords.Count >= 2)
            {
                var twoWordKey = startWords[startWords.Count - 2] + " " + startWords[startWords.Count - 1];

                if (nextWords.ContainsKey(twoWordKey))
                    return nextWords[twoWordKey];
            }

            if (nextWords.ContainsKey(oneWordKey))
                return nextWords[oneWordKey];

            return null;
        }
    }
}