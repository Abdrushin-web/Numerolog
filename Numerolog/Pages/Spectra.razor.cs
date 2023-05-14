using Colorology.Spectra;

namespace Numerolog.Pages
{
    public partial class Spectra
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Lightness = 100;
            Chroma = 100;
        }

        private int Lightness
        {
            get => lightness;
            set
            {
                lightness = value;
                SetSpectrum(spectrum => spectrum.Lightness = value);
            }
        }

        private int Chroma
        {
            get => chroma;
            set
            {
                chroma = value;
                SetSpectrum(spectrum => spectrum.Chroma = value);
            }
        }

        private void SetSpectrum(Action<ILCCircularSpectrum> set)
        {
            if (List is null)
                return;
            foreach (var spectrum in List.OfType<ILCCircularSpectrum>())
                set(spectrum);
        }

        int count = 24, start, lightness, chroma;
    }
}