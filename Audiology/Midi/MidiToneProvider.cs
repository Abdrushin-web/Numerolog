using Audiology.Audio;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Standards;
using MeltySynth;

namespace Audiology.Midi
{
    public class MidiToneProvider :
        ToneProvider
    {
        public MidiToneProvider(Synthesizer? synthesizer = null)
            => Synthesizer = synthesizer;

        public GeneralMidi2Program Instrument { get; set; } = GeneralMidi2Program.AcousticGrandPiano;
        public Synthesizer? Synthesizer { get; set; }

        protected override async Task<AudioSource?> GetAudioSource(IEnumerable<double>? frequencies, double frequency, TimeSpan duration, CancellationToken cancellation)
        {
            var midi = await MidiSource.Create(
                cancellation =>
                {
                    var builder = new PatternBuilder().
                        ProgramChange(Instrument);
                    if (frequencies is null)
                        builder.Tone(frequency, duration, Gain);
                    else {
                        builder.
                            Tones(frequencies, duration, false, Gain).
                            Tones(frequencies, duration * 2, true, Gain);
                    }
                    var result = builder.Build();
                    return Task.FromResult<Pattern?>(result);
                },
                loop: Loop,
                cancellation: cancellation);
            cancellation.ThrowIfCancellationRequested();
            if (Synthesizer != null &&
                midi != null) {
                var wave = await midi.ToWave(Synthesizer, cancellation);
                return wave;
            }
            return midi;
        }
    }
}
