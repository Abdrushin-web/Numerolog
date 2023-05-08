using System.Diagnostics.CodeAnalysis;

namespace Numerolog
{
    public static class Texts
    {
        public const string TrimmedEnd = "…";

        public static string? TrimToLength([NotNullIfNotNull(nameof(text))] this string? text, int length) => text?.Length > length ?
            text[..(length - 1)] + TrimmedEnd :
            text;
    }
}
