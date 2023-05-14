using Colourful;

namespace Colorology
{
    public class LChuvCircularSpectrum :
        LCCircularSpectrum<LChuvColor>
    {
        public LChuvCircularSpectrum() :
            base(new ConverterBuilder().
                FromLChuv().
                ToRGB().
                Build())
        { }

        public override string Name => "CIE LCh uv";

        protected override LChuvColor GetSource(double degree) => new (Lightness, Chroma, degree);
    }
}
