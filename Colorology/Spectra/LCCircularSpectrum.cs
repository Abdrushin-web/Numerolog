using Colourful;

namespace Colorology.Spectra
{
    public abstract class LCCircularSpectrum :
        RGBCircularSpectrum,
        ILCCircularSpectrum
    {
        public const double NormalLightness = 100;
        public const double NormalChroma = 100;

        public double Lightness { get; set; } = NormalLightness;
        public double Chroma { get; set; } = NormalChroma;
    }

    public abstract class LCCircularSpectrum<T> :
        LCCircularSpectrum
        where T : IColorSpace
    {
        protected LCCircularSpectrum(IColorConverter<T, RGBColor> converter)
            => this.converter = converter ?? throw new ArgumentNullException(nameof(converter));

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
