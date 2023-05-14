using Colourful;
using Spectrology;

namespace Colorology
{
    public interface ILCCircularSpectrum :
        IRGBCircularSpectrum
    {
        double Lightness { get; set; }
        double Chroma { get; set; }
    }
}