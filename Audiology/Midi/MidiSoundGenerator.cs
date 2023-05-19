using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.MusicTheory;

namespace Audiology.Midi
{
    public static class MidiSoundGenerator
    {
        public static PatternBuilder Tone(this PatternBuilder builder, double frequency, TimeSpan duration, double gain = 1)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            return builder.Note(new Tone(frequency).Note, duration.ToMetric(), gain.GainToVelocity());
        }

        public static PatternBuilder Tones(this PatternBuilder builder, IEnumerable<double> frequencies, TimeSpan duration, bool simultaneousOtherwiseSequence = true, double gain = 1, TimeSpan silenceBetweenDuration = default)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));
            if (frequencies is null)
                throw new ArgumentNullException(nameof(frequencies));
            if (simultaneousOtherwiseSequence) {
                builder.Chord(frequencies.Select(frequency => new Tone(frequency).Note), duration.ToMetric(), gain.GainToVelocity());
            } else {
                foreach (var frequency in frequencies) {
                    builder.Tone(frequency, duration, gain);
                    if (silenceBetweenDuration > TimeSpan.Zero)
                        builder.StepForward(silenceBetweenDuration.ToMetric());
                }
            }
            return builder;
        }
    }
}
