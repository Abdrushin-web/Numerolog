using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Interaction;

namespace Audiology.Midi
{
    /// <summary>
    /// MIDI helper
    /// </summary>
    public static class Midi
    {
        public static readonly double MaxVelocity = SevenBitNumber.MaxValue;

        public static SevenBitNumber GainToVelocity(this double gain) => new SevenBitNumber(Convert.ToByte(gain * MaxVelocity));

        public static MetricTimeSpan ToMetric(this TimeSpan duration) => new(duration);
    }
}
