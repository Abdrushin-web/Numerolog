using Colourful;
using Spectrology;

namespace Colorology
{
    public abstract class LCCircularSpectrum<T> :
        RGBCircularSpectrum,
        ILCCircularSpectrum
        where T : IColorSpace
    {
        protected LCCircularSpectrum(IColorConverter<T, RGBColor> converter)
            => this.converter = converter ?? throw new ArgumentNullException(nameof(converter));

        public double Lightness { get; set; } = 100;
        public double Chroma { get; set; } = 100;

        protected sealed override RGBColor DoGetValue(double degree)
        {
            var source = GetSource(degree);
            var target = converter.Convert(source);
            return target;
        }

        protected abstract T GetSource(double degree);

        private readonly IColorConverter<T, RGBColor> converter;
    }
}
