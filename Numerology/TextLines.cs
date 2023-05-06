using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Numerology
{
    public static class TextLines
    {
        public static readonly string Separator = Environment.NewLine;
        public static readonly string[] Separators = new[]
        {
            Separator,
            "\n"
        };

        public static IEnumerable<string>? SplitLines([NotNullIfNotNull(nameof(text))] this string? text, StringSplitOptions options = StringSplitOptions.None, bool withSeparators = false)
        {
            var lines = text?.
                Split(Separators, options).
                AsEnumerable();
            if (withSeparators && lines != null) {
                lines =
                    lines.SkipLast(1).Select(line => line + Separator).Concat(
                    lines.TakeLast(1));
            }
            return lines;
        }

        public static string JoinLines(this IEnumerable<string> lines, string? separator = null) => string.Join(separator ?? Separator, lines);

        public static bool HasMultipleLines(this string text) => string.IsNullOrEmpty(text) ?
            false :
            Separators.Any(text.Contains);

        public static bool HasSingleLineWithSeparatorEnd(this string text) =>
            text.HasMultipleLines() &&
            text.EndsWith(Separator) &&
            !text.Substring(0, text.Length - Separator.Length).HasMultipleLines();

        public static IEnumerable<IEnumerable<string>>? SplitLinesAndWords([NotNullIfNotNull(nameof(text))] this string? text, StringSplitOptions lineOptions = StringSplitOptions.None, bool withSeparators = false)
        {
            var lines = text.SplitLines(lineOptions, false);
            if (lines is null)
                return null;
            var lastLineIndex = lines.Count() - 1;
            if (lastLineIndex < 0)
                return Enumerable.Empty<IEnumerable<string>>();
            var linesWords = lines.Select((line, index) =>
            {
                var words = line.SplitWords(withSeparators)!;
                if (withSeparators &&
                    index != lastLineIndex) {
                    words =
                        words.SkipLast(1).Concat(
                        words.TakeLast(1).Select(word => word + Separator));
                }
                return words;
            });
            return linesWords;
        }
    }
}
