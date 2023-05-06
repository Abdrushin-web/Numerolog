using System.Diagnostics.CodeAnalysis;

namespace Numerology
{
    public static class TextWords
    {
        public const string Separator = " ";

        public static IEnumerable<string>? SplitWords([NotNullIfNotNull(nameof(text))] this string? text, bool withSeparators = false)
        {
            var words = text?.
                Split().
                AsEnumerable();
            if (withSeparators && words != null) {
                words =
                    words.SkipLast(1).Select(line => line + Separator).Concat(
                    words.TakeLast(1));
            }
            return words;
        }
    }
}
