using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var result = new List<List<string>>();
            var sentenceSeparators = new[] { '.', '!', '?', ';', ':', '(', ')' };

            var sentences = text.Split(sentenceSeparators, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sentence in sentences)
            {
                var words = new List<string>();
                var currentWord = new List<char>();

                foreach (var c in sentence)
                {
                    if (char.IsLetter(c) || c == '\'')
                        currentWord.Add(char.ToLower(c));
                    else if (currentWord.Count > 0)
                    {
                        words.Add(new string(currentWord.ToArray()));
                        currentWord.Clear();
                    }
                }

                if (currentWord.Count > 0)
                    words.Add(new string(currentWord.ToArray()));

                if (words.Count > 0)
                    result.Add(words);
            }

            return result;
        }
    }
}

//Изначально написал такой код, но никак не могу понять почему он не работает, если у тебя есть время, то я был бы очень признателен, если бы ты помог понять в чем проблема. На всех тестах кроме последнего он работает правильно
//var sentencesList = new List<List<string>>();
//var sentenceSepararos = new char[] { '.', '!', '?', ';', ':', '(', ')' };
//var wordSeparators = new char[] { ' ', ',', '^', '%', '*', '&', '#', '@', '$', '-', '+', '=', '_', '~', '<', '>', '1', '\t', '\r', '\n' };
//var sentences = text.Split(sentenceSepararos, StringSplitOptions.RemoveEmptyEntries).ToList();

//foreach (var e in sentences)
//{
//    var wordsInSentence = new List<string>();
//    var separtedSentence = e.Split(wordSeparators, StringSplitOptions.RemoveEmptyEntries).ToList();

//    foreach (var word in separtedSentence)
//        if (word.Length != 0 && (char.IsLetter(word[0]) || word[0] == '`' || word[0] == '\\'))
//            wordsInSentence.Add(word.ToLower());

//    sentencesList.Add(wordsInSentence);
//}
//return sentencesList


