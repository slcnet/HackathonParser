# Introduction

Your goal is to create a custom parser that can extract text from a file and output to the console (i.e., standard output). Requirements:

1. The parser should process content from a text file one line at a time.
2. Lines in the input file will be delimited using a carriage return followed by a line feed (i.e., CR LF or \r\n).
3. The content of each match should be output to the console exactly as it was matched (e.g., without case being changed). Content is matched when:
   1. It is within a set of double quotes (i.e., within a set of ") OR it is within a set of single quotes (i.e., within a set of ').
   2. When within a match, the enclosing quote character (i.e., single or double quote) can be escaped using a backslash (e.g., ‘this is David\’s son’ starts with ‘this’ and ends with ‘son’).
   3. A backslash enclosed in single or double quotes has no special meaning if not followed by the enclosing quote character and would, therefore, be included in the match as a regular character (e.g., “The file is at c:\temp\foo.txt on your hard drive” will include both backslashes).
   4. A match can never exceed one line (i.e., will not include a carriage return or line feed).
   5. Matches cannot be nested (e.g., “one ‘two’ three” is a single match enclosed by double quotes).
4. All matched content except any escaping backslash characters must be output to the console. No additional text may be output. When multiple matches are present on a line, they should be output exactly as is one after another without the addition of any additional characters.
5. If no content is matched, a blank line must be inserted into the output file, implying that the output file will have as many lines as the input file.
6. The quote character that delimits matches must not be included in the console output. Quotes that are present within a match must be output.
7. The parser **MUST NOT** be implemented using a regular expression engine.
8. The only content that may be output is the matched content for a line followed by a carriage return and line feed (i.e., CR LF or \r\n).

## Optional Definition

If you’re familiar with regular expressions, the parser should be able to extract the content from the capturing groups in the following ECMAScript (JavaScript) regular expression when processed a single line at a time **excluding any escaped single or double quotes**:

```regex
(?:"((?:\\")|([^"]))+")|((?:'((?:\\')|([^\']))+'))
```

This regular expression and the sample input file below can be seen at [regex101.com](https://regex101.com/r/XL5O36/1).

As stated in the requirements above, the parser **MUST NOT** be implemented using a regular expression engine.

# Sample File Input

The content below represents a single file containing three different lines. The sections enclosed in `**` represent the matches that should be discovered during file parsing. 

```markdown
When I say "**hello**" to my friends the '**world**' rejoices!
This is a test to '**verify the output**' of the "**world's**" first HQY '**Hackathon!**'
This line is not matched but will have an equivalent blank line in the output.
Please remember that '**HQY**\**'s first joy is you!**'
```

*Note that the sample above is intentionally incomplete. Requirements are included above that are not represented in the provided sample content.*

# Sample Console Output

The highlights in the sample file input above identify the matched portion of the input and, excluding the backslash (see rule #4 above), the output that should be sent to the console:

```
Helloworld
Verify the outputworld'sHackathon!

HQY’s first joy is you!
```

# Scoring

A sample test file has been created that is effectively a unit test suite has been created to validate the parser requirements. Points will be awarded proportionally to the number of unit tests passed. If all unit tests are passed, the maximum number of points possible will be received.

You will be given the unit test suite file approximately 15 minutes before the end of the challenge. This will give you a little bit of time to account for any missed considerations and generate the output file for submission. You **MUST** turn in the console output from that prior to the end of the challenge to receive credit. Please name the file <YourTeamName>.txt when submitting it.

# Note

If you copy/paste from this document, note that the quotes are the smart or “curly” quotes and will not match the standard quote (i.e., ") because Microsoft Word uses distinct left and right quotes. 
