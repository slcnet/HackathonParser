namespace ParserTests;

public class ParserTests
{
    private readonly Parser.HackathonParser _p = new();

    [TestCase("", "")]
    [TestCase("this does not contain a match", "")]
    [TestCase("\"two\"", "two")]
    [TestCase("'two'", "two")]
    [TestCase("one \"two\"", "two")]
    [TestCase("one 'two'", "two")]
    [TestCase("'one' 'two'", "onetwo")]
    [TestCase("'\\''", "'")]
    [TestCase("\"\\\"\"", "\"")]
    // rest of these are the unit tests for the implementation
    [TestCase("This line does not match anything.", "")]
    [TestCase("This line matches \"something using double quotes\"", "something using double quotes")]
    [TestCase("This line matches 'something using single quotes'", "something using single quotes")]
    [TestCase("This line matches \"something that escapes a double quote (i.e., \\\")\"",
        "something that escapes a double quote (i.e., \")")]
    [TestCase("This line matches 'something that escapes a single quote (i.e., \\')'",
        "something that escapes a single quote (i.e., ')")]
    [TestCase("\"This line matches everything with double quotes\"", "This line matches everything with double quotes")]
    [TestCase("'This line matches everything with single quotes'", "This line matches everything with single quotes")]
    [TestCase("'This line starts with a partial match' using single quotes", "This line starts with a partial match")]
    [TestCase("\"This line starts with a partial match\" using double quotes", "This line starts with a partial match")]
    [TestCase(
        "This line contains a single quoted string ' but does not have a terminating single quote and should be empty",
        "")]
    [TestCase(
        "This line contains a double quoted string \" but does not have a terminating double quote and should be empty",
        "")]
    [TestCase("This line ends with a single quote '", "")]
    [TestCase("This line ends with a double quote \"", "")]
    [TestCase("This \"line\" contains \"multiple quoted\" strings that should \"each\" show up in the 'output' ",
        "linemultiple quotedeachoutput")]
    [TestCase("This 'line' contains 'multiple quoted' strings that should \"each\" show up in the 'output' ",
        "linemultiple quotedeachoutput")]
    [TestCase("This line contains a single quote enclosed in double quotes \"'\"!", "'")]
    [TestCase("This line contains a double quote enclosed in single quotes '\"'!", "\"")]
    [TestCase("The double quote match on this line should output \"\" nothing", "")]
    [TestCase("The single quote match on this line should output '' nothing", "")]
    [TestCase("The double quote match on this line should output \" \" a single space", " ")]
    [TestCase("The single quote match on this line should output ' ' a single space", " ")]
    [TestCase("Double quoted strings\" should be able to start and end with spaces \"",
        " should be able to start and end with spaces ")]
    [TestCase("Single quoted strings' should be able to start and end with spaces '",
        " should be able to start and end with spaces ")]
    [TestCase("This will have multiple double quoted escapes \"\\\"\\\"\\\"\\\"\\\"\\\"\". Six of them, specifically.",
        "\"\"\"\"\"\"")]
    [TestCase("This will have multiple single quoted escapes '\\'\\'\\'\\'\\''. Five of them, specifically.", "'''''")]
    [TestCase(
        "This single quoted line contains a quoted backslash 'that is not \\ escaping' and should be seen in the output",
        "that is not \\ escaping")]
    [TestCase(
        "This double quoted line contains a quoted backslash \"that is not \\ escaping\" and should be seen in the output",
        "that is not \\ escaping")]
    [TestCase("\"random\" test 'one' should \" be easy to pass \" if 'all the above succeed'. Don't you think?",
        "randomone be easy to pass all the above succeed")]
    [TestCase("\"this shouldn\\\"t\" be hard to 'parse or parse\\'d' or \"one \\\"\\\"\\\"\" foo",
        "this shouldn\"tparse or parse'done \"\"\"")]
    [TestCase("Jutaxposed quotes work well 'one'\"two\"'three'\" \"", "onetwothree ")]
    [TestCase("What if I start a quote \"but then end on an escaped quote?\\\"", "")]
    [TestCase("What if I start a quote 'but then end on an escaped quote?\\' and maybe some follow on text?", "")]
    [TestCase("What if I quote something within quotes? He responded, \"I don't think your, 'yes' response is \\\"appropriate!\\\"\"", "I don't think your, 'yes' response is \"appropriate!\"")]
    [TestCase("What if I quote something within quotes? He responded, 'I dont think your, \"yes\" response is appropriate!'", "I dont think your, \"yes\" response is appropriate!")]
    [TestCase("Unterminated with nested quotes. He responded, \"I don't think your, 'yes' response is \\\"appropriate!\\\"", "")]
    [TestCase("Unterminated with nested quotes. He responded, 'I dont think your, \"yes\" response is appropriate!", "")]
    public void Parse_returns_expected_result(string text, string expected)
    {
        var result = _p.Parse(text);
        Assert.That(result, Is.EqualTo(expected));
    }
}