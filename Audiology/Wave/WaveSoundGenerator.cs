using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Audiology.Wave
{
    public static class WaveSoundGenerator
    {
        public static ISampleProvider Tone(this double frequency, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            var generator = new SignalGenerator() { Gain = gain };
            generator.Frequency = frequency;
            generator.Type = type;
            return generator;
        }

        public static ISampleProvider Tone(this double frequency, TimeSpan duration, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin) => frequency.Tone(gain, type).Take(duration);

        public static ISampleProvider Tones(this IEnumerable<double> frequencies, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            if (frequencies is null)
                throw new ArgumentNullException(nameof(frequencies));
            var count = frequencies.Count();
            if (count > 0)
                gain /= count;
            return frequencies.
                Select(frequency => frequency.Tone(gain, type)).
                Mix();
        }

        public static ISampleProvider Tones(this IEnumerable<double> frequencies, TimeSpan duration, bool simultaneousOtherwiseSequence = true, double gain = 1, TimeSpan silenceBetweenDuration = default, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            if (simultaneousOtherwiseSequence)
                return frequencies.Tones(gain, type).Take(duration);
            if (frequencies is null)
                throw new ArgumentNullException(nameof(frequencies));
            return frequencies.
                Select(frequency => frequency.Tone(duration, gain, type)).
                Sequence(silenceBetweenDuration);
        }

        public static ISampleProvider Tones(this IEnumerable<(double frequency, TimeSpan duration)> tones, TimeSpan silenceBetweenDuration = default, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            if (tones is null)
                throw new ArgumentNullException(nameof(tones));
            return tones.
                Select(tone => tone.frequency.Tone(tone.duration, gain, type)).
                Sequence(silenceBetweenDuration);
        }
    }
}
