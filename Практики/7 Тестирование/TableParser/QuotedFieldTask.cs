using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Linq;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
    [TestCase("''", 0, "", 2)]
    [TestCase("'a'", 0, "a", 3)]
    [TestCase("'b\"a'\"", 2, "a'", 4)]
    public void Test(string line, int startIndex, string expectedValue, int expectedLength)
    {
        var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
        ClassicAssert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
    }

    // Добавьте свои тесты
}

class QuotedFieldTask
{

    public static Token ReadQuotedField(string line, int startIndex)
    {
        for (var i = startIndex + 1; i < line.Length - 1; i++)
            if (line[i + 1] == line[startIndex])
                return new Token(line[(startIndex + 1)..(i + 1)], startIndex, i + 2 - line[(startIndex + 1)..(i + 1)].Count(c => c == '\\'));

        return new Token(line, startIndex, line.Length - startIndex);
    }
}