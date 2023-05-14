using Colourful;
using Spectrology;

namespace Colorology
{
    public static class HSL
    {
        /// <summary>
        /// Gets RGB color from HSL
        /// </summary>
        /// <remarks>Code adjusted from https://www.programmingalgorithms.com/algorithm/hsl-to-rgb/</remarks>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="lightness"></param>
        /// <returns></returns>
        public static RGBColor ToRGB(double hue, double saturation, double lightness)
        {
            double r, g, b;

            if (saturation == 0) {
                r = g = b = (byte)(lightness);
            }
            else {
                hue /= Circle.Degree.Full;

                var v2 = (lightness < 0.5) ? (lightness * (1 + saturation)) : ((lightness + saturation) - (lightness * saturation));
                var v1 = 2 * lightness - v2;

                r = HueToRGB(v1, v2, hue + (1.0f / 3));
                g = HueToRGB(v1, v2, hue);
                b = HueToRGB(v1, v2, hue - (1.0f / 3));
            }

            return new(r, g, b);
        }

        static double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
                vH += 1;

            if (vH > 1)
                vH -= 1;

            if ((6 * vH) < 1)
                return (v1 + (v2 - v1) * 6 * vH);

            if ((2 * vH) < 1)
                return v2;

            if ((3 * vH) < 2)
                return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

            return v1;
        }
    }
}
