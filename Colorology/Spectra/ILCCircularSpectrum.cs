using Colourful;
using Spectrology;

namespace Colorology.Spectra
{
    public interface ILCCircularSpectrum :
        IRGBCircularSpectrum
    {
        double Lightness { get; set; }
        double Chroma { get; set; }
    }
}