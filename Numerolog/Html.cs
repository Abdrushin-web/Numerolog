using Numerology;
using System.Diagnostics.CodeAnalysis;

namespace Numerolog
{
    public static class Html
    {
        public static readonly string LineSeparator = "<br/>";

        public static string? WithHtmlLines([NotNullIfNotNull(nameof(text))] this string? text) => text.SplitLines()?.JoinHtmlLines();
        public static string JoinHtmlLines(this IEnumerable<string> lines) => lines.JoinLines(LineSeparator);
    }
}
