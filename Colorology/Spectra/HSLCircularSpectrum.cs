using Colourful;

namespace Colorology.Spectra
{
    public class HSLCircularSpectrum :
        LCCircularSpectrum
    {
        public override string Name => "HSL";

        protected override RGBColor DoGetValue(double degree)
        {
            var saturation = Chroma / NormalChroma;
            var lightness = Lightness / NormalLightness / 2;
            return HSL.ToRGB(degree, saturation, lightness);
        }

        static readonly double WavelengthWidth = WavelengthSpectrum.MaxWavelength - WavelengthSpectrum.MinWavelength;
    }
}
