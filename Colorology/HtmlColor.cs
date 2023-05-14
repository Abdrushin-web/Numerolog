using Colourful;
using System.Globalization;

namespace Colorology
{
    public static class HtmlColors
    {
        public static string ToHtml(this RGBColor color)
        {
            color.ToRGB8Bit(out var r, out var g, out var b);
            return $"#{r:x2}{g:x2}{b:x2}";
        }

        public static RGBColor? HtmlToRGBColor(this string? color)
        {
            if (string.IsNullOrWhiteSpace(color))
                return null;
            if (color.Length > 0 &&
                color[0] == '#') {
                color = color.Substring(1);
            }
            if (color.Length != 6)
                throw new ArgumentException($"'{nameof(color)}' must have 6 characters", nameof(color));
            var r = ParseHexColorChannel(color.Substring(0, 2));
            var g = ParseHexColorChannel(color.Substring(2, 2));
            var b = ParseHexColorChannel(color.Substring(4, 2));
            var result = RGBColor.FromRGB8Bit(r, g, b);
            return result;
        }

        public static byte ParseHexColorChannel(string value) => byte.Parse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
    }
}
