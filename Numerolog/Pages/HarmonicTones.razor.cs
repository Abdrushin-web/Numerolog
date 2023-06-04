using Audiology.Audio;
using Harmonology;
using Melanchall.DryWetMidi.MusicTheory;
using Rationals;

namespace Numerolog.Pages
{
    public partial class HarmonicTones
    {
        public override ToneProvider CreateToneProvider()
        {
            var provider = base.CreateToneProvider();
            var octave = new HarmonicOctave(baseFrequency, (uint)count);
            frequencyFactors = octave.FrequencyFactors.ToArray();
            provider.Frequencies = frequencyFactors.Select(i => i.frequency);
            return provider;
        }

        int count = 9;
        double baseFrequency = Tone.ConcertA.Frequency;
        (double frequency, Rational factor)[]? frequencyFactors;
    }
}