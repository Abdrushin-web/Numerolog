using Colourful;

namespace Colorology
{
    public class RGBLinearInterpolation :
        IRGBInterpolation
    {
        public string Name => "RGB lineární";

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
            var c = new LinearRGBColor(
                c1.R * ratio1 + c2.R * ratio2,
                c1.G * ratio1 + c2.G * ratio2,
                c1.B * ratio1 + c2.B * ratio2);
            var color = To.Convert(c);
            return color;
        }

        static readonly IColorConverter<RGBColor, LinearRGBColor> From = new ConverterBuilder().FromRGB().ToLinearRGB().Build();
        static readonly IColorConverter<LinearRGBColor, RGBColor> To = new ConverterBuilder().FromLinearRGB().ToRGB().Build();
    }
}
