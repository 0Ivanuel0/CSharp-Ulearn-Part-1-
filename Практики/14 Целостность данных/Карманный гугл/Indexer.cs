using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace PocketGoogle;

public class Indexer : IIndexer
{
    private readonly Dictionary<string, Dictionary<int, List<int>>> Index = 
        new Dictionary<string, Dictionary<int, List<int>>>();

    private readonly Dictionary<int, List<string>> DocumentWords = 
        new Dictionary<int, List<string>>();

    public void Add(int id, string documentText)
	{
		var separators = new char[] { ' ', '.', ',', '!', '?', ':', '-', '-', '\r', '\n' };
        DocumentWords[id] = new List<string>();

        var i = 0;
        while (i < documentText.Length)
        {
            while (i < documentText.Length && separators.Contains(documentText[i])) i++;
            if (i >= documentText.Length) break;

            var wordStart = i;
            while (i < documentText.Length && (separators.Contains(documentText[i]) is false)) i++;
            var currentWord = documentText.Substring(wordStart, i - wordStart);

            if (Index.ContainsKey(currentWord) is false)
                Index[currentWord] = new Dictionary<int, List<int>>();

            if (Index[currentWord].ContainsKey(id) is false)
                Index[currentWord][id] = new List<int>();

            Index[currentWord][id].Add(wordStart);
            DocumentWords[id].Add(currentWord);
        }
	}

	public List<int> GetIds(string word)
	{
        if (Index.ContainsKey(word) is false)
            return new List<int>();
        return Index[word].Keys.ToList();
	}

	public List<int> GetPositions(int id, string word)
	{
		if (Index.ContainsKey(word) is false)
            return new List<int>();

        if (Index[word].ContainsKey(id) is false)
            return new List<int>();

        return Index[word][id].ToList();
	}

	public void Remove(int id)
	{
        if (DocumentWords.ContainsKey(id) is false) return;

        var words = DocumentWords[id];
        foreach (var word in words)
        {
            if (Index.ContainsKey(word) is false) continue;

            Index[word].Remove(id);
            if (Index[word].Count == 0) Index.Remove(word);
        }

        DocumentWords.Remove(id);
	}
}