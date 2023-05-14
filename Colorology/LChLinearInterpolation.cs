using Colourful;
using Spectrology;

namespace Colorology
{
    public class LChLinearInterpolation :
        IRGBInterpolation
    {
        public string Name => "LCh lineární";

        public RGBColor GetColor(RGBColor color1, RGBColor color2, double ratio)
        {
            if (ratio <= 0)
                return color1;
            else if (ratio >= 1)
                return color2;
            if (color1 == color2)
                return color1;
            var ratio1 = 1 - ratio;
            var ratio2 = ratio;
            var c1 = From.Convert(color1);
            var c2 = From.Convert(color2);
            if (Math.Abs(c1.h - c2.h) > Circle.Degree.Full / 2) {
                if (c1.h > c2.h)
                    c1 = new(c1.L, c1.C, c1.h - Circle.Degree.Full);
                else
                    c2 = new(c2.L, c2.C, c2.h - Circle.Degree.Full);
            }
            var c = new LChabColor(
                c1.L * ratio1 + c2.L * ratio2,
                c1.C * ratio1 + c2.C * ratio2,
                c1.h * ratio1 + c2.h * ratio2);
            var color = To.Convert(c);
            return color;
        }

        static readonly IColorConverter<RGBColor, LChabColor> From = new ConverterBuilder().FromRGB().ToLChab().Build();
        static readonly IColorConverter<LChabColor, RGBColor> To = new ConverterBuilder().FromLChab().ToRGB().Build();
    }
}
