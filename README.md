ColorConsole
A tiny, zero‑dependency C# library that lets you write colored and styled text to the console using inline ANSI tags.

Quick tip – If you want to combine tags with C# string interpolation, just escape the tag braces:

Csharp

Apply
ColorConsole.WriteLine(
 Installation
Bash
Run
dotnet add package ColorConsole
Windows note – Windows 10+ terminals (e.g. Windows Terminal, ConEmu) already understand ANSI.
For older Windows console apps, call Console.TreatControlCAsInput = true; or enable Virtual Terminal processing with the kernel32.dll call.

 Quick Start
Csharp

Apply
using ColorConsole;

ColorConsole.WriteLine("The {blue}sky{/} is {yellow}bright{/}.");
ColorConsole.WriteLine("{bold}Bold text{/} and {underline}underlined{/}.");
ColorConsole.WriteLine("{bg-green white}Green background{/}.");
ColorConsole.WriteLine("{rgb(255,120,0)}Custom orange{/}.");

Apply
The sky is bright.
Bold text and underlined.
Green background.
Custom orange.
📚 Supported Tags
Tag	Meaning	Example
{red} {green} {yellow} {blue} {magenta} {cyan} {white}	Foreground color	{red}Error{/}
{bg-red} {bg-green} {bg-yellow} {bg-blue} {bg-magenta} {bg-cyan} {bg-white}	Background color	{bg-blue white}Text{/}
{bold} {underline} {strike}	Text style	{bold}Important{/}
{rgb(R,G,B)}	24‑bit foreground color	{rgb(255,120,0)}Orange{/}
{bg-rgb(R,G,B)}	24‑bit background color	{bg-rgb(0,120,255)}Blue{/}
{/} or {reset}	Reset all styles	{red}X{/}
Multiple tags in one set	Combine styles	{yellow bold underline}Notice{/}
All tags are case‑insensitive and can be combined in any order.

 Interpolating Values
Csharp

Apply
int users = 12;
ColorConsole.WriteLine($"There are {{green}}{users}{{/}} users online.");
Escape the tag braces ({{ and }}) so the compiler doesn’t think they’re interpolation placeholders.

 API
Method	Description
ColorConsole.WriteLine(string input)	Writes the formatted string and ends with \n.
ColorConsole.Write(string input)	Writes the formatted string without a newline.
Both methods process the tags on the fly and automatically emit a reset sequence at the end of each line to keep styles from leaking.

 FAQ
Question	Answer
Does this work on Windows cmd?	Only on Windows 10+ terminals that support ANSI (e.g. Windows Terminal, ConEmu, PowerShell).
Can I use it in a library?	Yes. The library is pure C# and has no external dependencies.
What about .NET Core vs .NET Framework?	It targets the LTS .NET 8.0, so it works on newer projects. If your project is older than .NET 8 and you want support, send me an email and maybe I'll update it to support older projects.  clintonjavery@gmail.com

 License
MIT – feel free to use it in any project, open source or commercial.

Happy colorful coding!