using Rationals;

namespace Harmonology
{
    public class HarmonicOctave
    {
        public HarmonicOctave(double baseFrequency, uint valueCount)
        {
            Frequency.Validate(baseFrequency, nameof(baseFrequency));
            if (valueCount == 0)
                throw new ArgumentOutOfRangeException(nameof(valueCount), valueCount, "Value must be positive");
            BaseFrequency = baseFrequency;
            ValueCount = valueCount;
        }

        public double BaseFrequency { get; }
        public uint ValueCount { get; }

        public static readonly Rational Two = 2;

        public Rational GetFactor(uint index)
        {
            var value = Rational.One + (Rational)index / ValueCount;
            while (value.WholePart > Two)
                value /= Two;
            return value;
        }

        public double GetFrequency(uint index) => GetFrequency(GetFactor(index));
        public double GetFrequency(Rational factor) => BaseFrequency * (double)factor;

        public IEnumerable<double> Frequencies => Indices.Select(GetFrequency);

        public IEnumerable<(double frequency, Rational factor)> FrequencyFactors => Indices.
            Select(i =>
            {
                var factor = GetFactor(i);
                return (GetFrequency(factor), factor);
            });

        private IEnumerable<uint> Indices => Enumerable.Range(0, (int)ValueCount).
            Select(i => (uint)i);
    }
}
