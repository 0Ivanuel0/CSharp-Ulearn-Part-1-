namespace Passwords;

public class CaseAlternatorTask
{
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var uniqueResults = new HashSet<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, uniqueResults);
        return uniqueResults.ToList();
    }

    static void AlternateCharCases(char[] word, int startIndex, HashSet<string> result)
    {
        if (startIndex == word.Length)
        {
            result.Add(new string(word));
            return;
        }

        if (char.IsLetter(word[startIndex]) is false)
        {
            AlternateCharCases(word, (int)(startIndex + 1), result);
            return;
        }
         
        word[startIndex] = char.ToLower(word[startIndex]);
        AlternateCharCases(word, (int)(startIndex + 1), result);

        word[startIndex] = char.ToUpper(word[startIndex]);
        AlternateCharCases(word, (int)(startIndex + 1), result);
    }
}