using System.Text.RegularExpressions;
/// <summary>
/// Provides inline formatted console output using ANSI escape codes.
/// </summary>
/// <remarks>
/// This helper allows console text styling using simple markup tags embedded in strings.
/// It works in modern terminals such as Windows Terminal, VS Code terminal, Linux, and macOS.
///
/// Basic Example:
/// <code>
/// ColorConsole.WriteLine("The {blue}sky{/} is {yellow}bright{/}");
/// </code>
///
/// Multiple styles:
/// <code>
/// ColorConsole.WriteLine("{yellow bold}Warning:{/} Low disk space");
/// ColorConsole.WriteLine("{red strike}Deprecated API{/}");
/// ColorConsole.WriteLine("{cyan underline}Documentation{/}");
/// </code>
///
/// Background colors:
/// <code>
/// ColorConsole.WriteLine("{bg-blue white}Highlighted text{/}");
/// </code>
///
/// Custom RGB colors:
/// <code>
/// ColorConsole.WriteLine("{rgb(255,120,0)}Custom orange{/}");
/// </code>
///
/// Log-style output:
/// <code>
/// ColorConsole.WriteLine("{green}[OK]{/} Server started on port {cyan}3000{/}");
/// ColorConsole.WriteLine("{yellow}[WARN]{/} Cache miss detected");
/// ColorConsole.WriteLine("{red}[ERROR]{/} Database unavailable");
/// </code>
///
/// Supported formatting tags:
///
/// Text colors:
/// {black} {red} {green} {yellow} {blue} {magenta} {cyan} {white}
///
/// Background colors:
/// {bg-black} {bg-red} {bg-green} {bg-yellow} {bg-blue} {bg-magenta} {bg-cyan} {bg-white}
///
/// Styles:
/// {bold}
/// {underline}
/// {strike}
///
/// Reset formatting:
/// {/}
///
/// RGB color:
/// {rgb(R,G,B)}
/// Example: {rgb(255,120,0)}
/// 
/// #### String interpolation
/// C# string interpolation uses `{}` to embed expressions, so the braces that belong to
/// `ColorConsole` tags must be escaped. In an interpolated string, `{{` produces a
/// single `{` and `}}` produces a single `}`.
///
/// Example:
/// <code>
/// int value = 42;
/// ColorConsole.WriteLine($"The {{blue}}sky{{/}} is {{yellow}}bright{{/}} – value = {value}");
/// </code>
/// 
/// Notes:
/// - Tags can be combined: {yellow bold underline}
/// - Always close formatting with {/} to reset the style.
/// - ANSI styling requires a modern terminal.
/// </remarks>
public static class ColorConsole
{
    static readonly Regex TagRegex = new(@"\{(.*?)\}", RegexOptions.Compiled);

    static readonly Dictionary<string, string> Codes = new()
    {
        ["reset"] = "\u001b[0m",
        ["/"] = "\u001b[0m",

        ["bold"] = "\u001b[1m",
        ["underline"] = "\u001b[4m",
        ["strike"] = "\u001b[9m",

        ["black"] = "\u001b[30m",
        ["red"] = "\u001b[31m",
        ["green"] = "\u001b[32m",
        ["yellow"] = "\u001b[33m",
        ["blue"] = "\u001b[34m",
        ["magenta"] = "\u001b[35m",
        ["cyan"] = "\u001b[36m",
        ["white"] = "\u001b[37m",

        ["bg-black"] = "\u001b[40m",
        ["bg-red"] = "\u001b[41m",
        ["bg-green"] = "\u001b[42m",
        ["bg-yellow"] = "\u001b[43m",
        ["bg-blue"] = "\u001b[44m",
        ["bg-magenta"] = "\u001b[45m",
        ["bg-cyan"] = "\u001b[46m",
        ["bg-white"] = "\u001b[47m"
    };

    /// <summary>
    /// Call this method like this ColorConsole.WriteLine("This is {red}red{/} text.")
    /// </summary>
    /// <param name="input">This is the full string including the formatting</param> 
    public static void WriteLine(string input)
    {
        Write(input);
        Console.WriteLine();
    }

    /// <summary>
    /// Call this method like this ColorConsole.Write("This is {red}red{/} text.")
    /// </summary>
    /// <param name="input">This is the full string including the formatting</param> 
    public static void Write(string input)
    {
        int lastIndex = 0;

        foreach (Match match in TagRegex.Matches(input))
        {
            Console.Write(input.Substring(lastIndex, match.Index - lastIndex));

            var tagContent = match.Groups[1].Value.ToLower().Trim();
            var tags = tagContent.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var tag in tags)
            {
                if (tag.StartsWith("rgb("))
                {
                    var rgb = tag.Substring(4, tag.Length - 5).Split(',');
                    var r = int.Parse(rgb[0]);
                    var g = int.Parse(rgb[1]);
                    var b = int.Parse(rgb[2]);

                    Console.Write($"\u001b[38;2;{r};{g};{b}m");
                }
                else if (Codes.TryGetValue(tag, out var ansi))
                {
                    Console.Write(ansi);
                }
            }

            lastIndex = match.Index + match.Length;
        }

        if (lastIndex < input.Length)
            Console.Write(input.Substring(lastIndex));

        Console.Write("\u001b[0m"); // safety reset
    }
}