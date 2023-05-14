using Colourful;
using Spectrology;

namespace Colorology.Spectra
{
    public class WavelengthCircularSpectrum :
        RGBCircularSpectrum
    {
        public override string Name => "Vlnová délka";

        protected override RGBColor DoGetValue(double degree)
        {
            var ratio = degree / Circle.Degree.Full;
            var wavelength = WavelengthSpectrum.MaxWavelength - WavelengthWidth * ratio;
            return WavelengthSpectrum.GetColorFromWavelength(wavelength);
        }

        static readonly double WavelengthWidth = WavelengthSpectrum.MaxWavelength - WavelengthSpectrum.MinWavelength;
    }
}
