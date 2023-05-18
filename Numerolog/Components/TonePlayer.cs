using Audiology;
using Microsoft.AspNetCore.Components;
using NAudio.Wave;

namespace Numerolog.Components
{
    public class TonePlayer :
        SamplePlayer
    {
        [Parameter]
        public double Gain { get; set; } = 1;

        [Parameter]
        public double Frequency { get; set; } = DefaultFrequency;

        [Parameter]
        public IEnumerable<double>? Frequencies { get; set; }

        public const double DefaultFrequency = 440;

        [Parameter]
        public TimeSpan Duration { get; set; }

        public static readonly TimeSpan DefaultDuration = TimeSpan.FromSeconds(5);

        protected override bool Loop => Duration <= TimeSpan.Zero;

        protected override Task<ISampleProvider?> GetSample(CancellationToken cancellation)
        {
            var duration = Duration;
            if (Loop)
                duration = DefaultDuration;
            ISampleProvider? result;
            if (Frequencies?.Any() == true)
                result = Frequencies.Tones(duration, gain: Gain);
            else {
                var frequency = Frequency;
                if (frequency <= 0)
                    frequency = DefaultFrequency;
                result = frequency.Tone(duration, Gain);
            }
            return Task.FromResult<ISampleProvider?>(result);
        }
    }
}