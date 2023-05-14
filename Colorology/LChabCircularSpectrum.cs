using Colourful;

namespace Colorology
{
    public class LChabCircularSpectrum :
        LCCircularSpectrum<LChabColor>
    {
        public LChabCircularSpectrum() :
            base(new ConverterBuilder().
                FromLChab().
                ToRGB().
                Build())
        { }

        public override string Name => "CIE LCh ab";

        protected override LChabColor GetSource(double degree) => new (Lightness, Chroma, degree);
    }
}
