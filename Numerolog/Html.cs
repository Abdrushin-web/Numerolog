using Numerology;

namespace Numerolog
{
    public static class Html
    {
        public static readonly string LineSeparator = "<br/>";

        public static string? WithHtmlLines(this string? text) => text.SplitLines()?.JoinHtmlLines();
        public static string JoinHtmlLines(this IEnumerable<string> lines) => lines.JoinLines(LineSeparator);
    }
}
