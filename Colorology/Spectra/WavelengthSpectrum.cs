using Colourful;

namespace Colorology.Spectra
{
    public static class WavelengthSpectrum
    {
        public const double MinWavelength = 350;
        public const double MaxWavelength = 780;

        public static double Gamma { get; set; } = 1.00;

        /// <summary>
        /// Gets color from <paramref name="wavelength"/>
        /// </summary>
        /// <remarks>Code adjusted from https://robot.lk/viewtopic.php?t=3072</remarks>
        /// <param name="wavelength">Wavelength [nm]</param>
        /// <returns>Color</returns>
        public static RGBColor GetColorFromWavelength(double wavelength)
        {
            double blue;
            double green;
            double red;
            double factor;

            if (wavelength >= 350 && wavelength <= 439) {
                red = -(wavelength - 440d) / (440d - 350d);
                green = 0.0;
                blue = 1.0;
            }
            else if (wavelength >= 440 && wavelength <= 489) {
                red = 0.0;
                green = (wavelength - 440d) / (490d - 440d);
                blue = 1.0;
            }
            else if (wavelength >= 490 && wavelength <= 509) {
                red = 0.0;
                green = 1.0;
                blue = -(wavelength - 510d) / (510d - 490d);

            }
            else if (wavelength >= 510 && wavelength <= 579) {
                red = (wavelength - 510d) / (580d - 510d);
                green = 1.0;
                blue = 0.0;
            }
            else if (wavelength >= 580 && wavelength <= 644) {
                red = 1.0;
                green = -(wavelength - 645d) / (645d - 580d);
                blue = 0.0;
            }
            else if (wavelength >= 645 && wavelength <= 780) {
                red = 1.0;
                green = 0.0;
                blue = 0.0;
            }
            else {
                red = 0.0;
                green = 0.0;
                blue = 0.0;
            }
            factor = wavelength switch
            {
                >= 350 and <= 419 => 0.3 + 0.7 * (wavelength - 350d) / (420d - 350d),
                >= 420 and <= 700 => 1.0,
                >= 701 and <= 780 => 0.3 + 0.7 * (780d - wavelength) / (780d - 700d),
                _ => 0.0
            };
            var r = AdjustChannel(red, factor);
            var g = AdjustChannel(green, factor);
            var b = AdjustChannel(blue, factor);
            return RGBColor.FromRGB8Bit(r, g, b);
        }

        static byte AdjustChannel(double value, double factor) => value == 0.0 ?
            (byte)0 :
            (byte)Math.Round(byte.MaxValue * Math.Pow(value * factor, Gamma));
    }
}
