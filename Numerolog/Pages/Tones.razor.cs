using Audiology.Audio;

namespace Numerolog.Pages
{
    public partial class Tones
    {
        ToneProvider FrequencyProvider
        {
            get
            {
                var provider = CreateToneProvider();
                provider.Frequency = frequency;
                return provider;
            }
        }
        ToneProvider ChordProvider
        {
            get
            {
                var provider = CreateToneProvider();
                provider.Frequencies = new[] { frequency1, frequency2, frequency3 };
                return provider;
            }
        }

        double
            frequency = ToneProvider.DefaultFrequency, // A
            // Pytagorean chord C dur
            frequency1 = 1000, // C
            frequency2 = 1250, // E
            frequency3 = 1500; // G
    }
}