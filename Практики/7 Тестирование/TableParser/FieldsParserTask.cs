using Avalonia.Controls.Shapes;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
    public static void Test(string input, string[] expectedResult)
    {
        var actualResult = FieldsParserTask.ParseLine(input);
        ClassicAssert.AreEqual(expectedResult.Length, actualResult.Count);
        for (int i = 0; i < expectedResult.Length; ++i)
        {
            ClassicAssert.AreEqual(expectedResult[i], actualResult[i].Value);
        }
    }

    [TestCase("text", new[] { "text" })]
    [TestCase("hello world", new[] { "hello", "world" })]
    [TestCase("'hello' world", new[] { "hello", "world" })]
    [TestCase("  hello  world  ", new[] { "hello", "world" })]
    [TestCase("'hello\"' world", new[] { "hello\"", "world" })]
    [TestCase("'hello\\''", new[] { "hello'" })]
    [TestCase("\"hello", new[] { "hello" })]
    [TestCase("", new string[0])]
    [TestCase("   ", new string[0])]
    [TestCase("''", new[] { "" })]
    [TestCase("\"hello\"", new[] { "hello" })]
    [TestCase("\"hello\\\"world\"", new[] { "hello\"world" })]
    [TestCase("\"'hello'\"", new[] { "'hello'" })]
    [TestCase("\"a\"b\"c\"", new[] { "a", "b", "c" })]
    [TestCase("'multiple words' here", new[] { "multiple words", "here" })]
    [TestCase("\"hello\\\\world\"", new[] { "hello\\world" })]
    [TestCase("\"text ", new[] { "text " })]
    [TestCase("\"a\\\\\"", new[] { "a\\" })]

    public static void RunTests(string input, string[] expectedOutput)
    {
        Test(input, expectedOutput);
    }
}

public class FieldsParserTask
{
    public static List<Token> ParseLine(string line)
    {
        var listOfTokens = new List<Token>();
        var currentPosition = 0;

        while (currentPosition < line.Length)
        {
            if (line[currentPosition] == ' ')
            {
                currentPosition += 1;
                continue;
            }

            var token = GetTypeOfField(line, currentPosition);

            listOfTokens.Add(token);
            currentPosition += token.Length;
        }

        return listOfTokens;
    }

    public static Token GetTypeOfField(string line, int startIndex)
    {
        if (line[startIndex] == '\"' || line[startIndex] == '\'')
            return ReadQuotedField(line, startIndex);
        return ReadBasicField(line, startIndex);
    }

    public static Token ReadQuotedField(string line, int startIndex)
    {
        return QuotedFieldTask.ReadQuotedField(line, startIndex);
    }

    public static Token ReadBasicField(string line, int startIndex)
    {
        var endPosition = startIndex;

        while (
            endPosition < line.Length &&
            line[endPosition] != '\"' &&
            line[endPosition] != '\'' &&
            char.IsWhiteSpace(line[endPosition]) is false
            )
            endPosition += 1;

        var value = line.Substring(startIndex, endPosition - startIndex);

        return new Token(value, startIndex, endPosition - startIndex);
    }
}