using System.Text;

namespace Parser;

public class HackathonParser
{
    private const int SEARCHING = 0;
    private const int IN_DOUBLE_QUOTE = 1;
    private const int IN_SINGLE_QUOTE = 2;

    private const char DOUBLE_QUOTE = '"';
    private const char SINGLE_QUOTE = '\'';
    private const char ESCAPE_CHAR = '\\';

    public string Parse(string lineOfText)
    {
        var ranges = ParseRanges(lineOfText);
        return CreateStringFromRanges(lineOfText, ranges);
    }

    private static List<(int start, int length, bool completed)> ParseRanges(string lineOfText)
    {
        var state = SEARCHING; // looking for a match
        var i = 0;
        var matchStartIx = -1;
        // start is the starting index for a capture
        // length is the length of the capture
        // completed is whether I have seen the end quote and am allowed to capture the content up to that point
        var ranges = new List<(int start, int length, bool completed)>();
        while (i < lineOfText.Length)
        {
            switch (state)
            {
                case SEARCHING when lineOfText[i] == DOUBLE_QUOTE:
                    state = IN_DOUBLE_QUOTE;
                    matchStartIx = i + 1;
                    break;
                case IN_DOUBLE_QUOTE when lineOfText[i] == DOUBLE_QUOTE:
                case IN_SINGLE_QUOTE when lineOfText[i] == SINGLE_QUOTE:
                    ranges.Add((matchStartIx, i - matchStartIx, true));
                    state = SEARCHING;
                    break;
                case SEARCHING when lineOfText[i] == SINGLE_QUOTE:
                    state = IN_SINGLE_QUOTE;
                    matchStartIx = i + 1;
                    break;
                case IN_DOUBLE_QUOTE when lineOfText[i] == ESCAPE_CHAR && i + 1 < lineOfText.Length &&
                                          lineOfText[i + 1] == DOUBLE_QUOTE:
                case IN_SINGLE_QUOTE when lineOfText[i] == ESCAPE_CHAR && i + 1 < lineOfText.Length &&
                                          lineOfText[i + 1] == SINGLE_QUOTE:
                    // I'm in a double quote and I have an escaped double quote so I need to remove the escaping character
                    ranges.Add((matchStartIx, i - matchStartIx, false));
                    // and I need to add the quote back in
                    ranges.Add((i + 1, 1, false));
                    // and then advance the index so I'm pointing at the character immediately after the quote character
                    i++;
                    // and I need to advance the matchStartIx to start at the character immediately after the quote
                    matchStartIx = i + 1;
                    break;
            }

            i++;
        }

        return ranges;
    }
    
    
    private static string CreateStringFromRanges(string lineOfText, IReadOnlyList<(int start, int length, bool completed)> ranges)
    {
        var sb = new StringBuilder();
        var i = 0;
        var j = -1;
        while (i < ranges.Count)
        {
            var (start, end, completed) = ranges[i];
            if (completed)
            {
                if (j != -1)
                {
                    while (j < i)
                    {
                        sb.Append(lineOfText.AsSpan(ranges[j].start, ranges[j].length));
                        j++;
                    }
                }

                sb.Append(lineOfText.AsSpan(start, end));
                j = -1;
            }
            else if (j == -1)
            {
                j = i;
            }

            i++;
        }

        return sb.ToString();
    }

}
