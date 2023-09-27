if (args.Length != 2)
{
    Console.WriteLine("Usage: Scorer <expectedFile> <actualFile>");
    return;
}

var expectedLines = File.ReadAllLines(args[0]);
var actualLines = File.ReadAllLines(args[1]);
var score = 0;
var i = 0;
while (i < expectedLines.Length && i < actualLines.Length)
{
    // this is slightly generous compared to the requirements
    var expected = expectedLines[i];
    var actual = actualLines[i];
    score += expected.Equals(actual, StringComparison.InvariantCulture) ? 1 : 0;
    i++;
}

Console.WriteLine(score);
