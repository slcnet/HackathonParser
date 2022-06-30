using Parser;

if (args.Length != 1)
{
    Console.WriteLine("Usage: Parser <inputFilePath>");
    return;
}

var p = new HackathonParser();
var lines = File.ReadLines(args[0]);
foreach (var line in lines)
{
    var result = p.Parse(line);
    Console.WriteLine(result);
}