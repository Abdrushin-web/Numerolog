using Audiology.Audio;
using Audiology.Midi;
using Melanchall.DryWetMidi.Standards;

namespace Numerolog.Pages
{
    public partial class Tones
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await MidiSynthesizer.SetDefaultSynthesizerFromHttp(Http);
        }

        ToneProvider FrequencyProvider => new MidiToneProvider
        {
            Gain = gain,
            Frequency = frequency,
            Instrument = instrument,
            Synthesizer = MidiSynthesizer.DefaultSynthesizer
        };
        ToneProvider ChordProvider => new MidiToneProvider
        {
            Gain = gain,
            Frequencies = new[] { frequency1, frequency2, frequency3 },
            Instrument = instrument,
            Synthesizer = MidiSynthesizer.DefaultSynthesizer
        };

        const double MinFrequency = 20;
        const double MaxFrequency = 20000;

        double
            gain = 1,
            frequency = ToneProvider.DefaultFrequency, // A
            // Pytagorean chord C dur
            frequency1 = 1000, // C
            frequency2 = 1250, // E
            frequency3 = 1500; // G

        readonly IEnumerable<GeneralMidi2Program> Instruments = Enum.GetValues<GeneralMidi2Program>().
            OrderBy(i => i.ToString());

        GeneralMidi2Program instrument = GeneralMidi2Program.AcousticGrandPiano;
    }
}