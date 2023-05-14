using Colourful;

namespace Colorology.Interpolation
{
    /// <summary>
    /// Interpolates between 2 colors
    /// </summary>
    public interface IRGBInterpolation
    {
        /// <summary>
        /// Interpolation name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets interpolated color
        /// </summary>
        /// <param name="color1">First color</param>
        /// <param name="color2">Second color</param>
        /// <param name="ratio">Ratio between <paramref name="color1"/> and <paramref name="color2"/>. 0 means <paramref name="color1"/>, 1 <paramref name="color2"/>, 1/2 between.</param>
        /// <returns>Result</returns>
        RGBColor GetColor(RGBColor color1, RGBColor color2, double ratio);
    }
}
