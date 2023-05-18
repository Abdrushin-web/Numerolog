using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Audiology
{
    public static class SoundGenerator
    {
        public static ISampleProvider Tone(this double frequency, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            var generator = new SignalGenerator() { Gain = gain };
            generator.Frequency = frequency;
            generator.Type = type;
            return generator;
        }

        public static ISampleProvider Tone(this double frequency, TimeSpan duration, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin) => Tone(frequency, gain, type).Take(duration);

        public static ISampleProvider Tones(this IEnumerable<double> frequencies, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            if (frequencies is null)
                throw new ArgumentNullException(nameof(frequencies));
            var count = frequencies.Count();
            if (count > 0)
                gain /= count;
            return frequencies.
                Select(frequency => Tone(frequency, gain, type)).
                Mix();
        }

        public static ISampleProvider Tones(this IEnumerable<double> frequencies, TimeSpan duration, bool simultaneousOtherwiseSequence = true, double gain = 1, TimeSpan silenceBetweenDuration = default, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            if (simultaneousOtherwiseSequence)
                return Tones(frequencies, gain, type).Take(duration);
            if (frequencies is null)
                throw new ArgumentNullException(nameof(frequencies));
            return frequencies.
                Select(frequency => Tone(frequency, duration, gain, type)).
                Sequence(silenceBetweenDuration);
        }

        public static ISampleProvider Tones(this IEnumerable<(double frequency, TimeSpan duration)> tones, TimeSpan silenceBetweenDuration = default, double gain = 1, SignalGeneratorType type = SignalGeneratorType.Sin)
        {
            if (tones is null)
                throw new ArgumentNullException(nameof(tones));
            return tones.
                Select(tone => Tone(tone.frequency, tone.duration, gain, type)).
                Sequence(silenceBetweenDuration);
        }
    }
}
