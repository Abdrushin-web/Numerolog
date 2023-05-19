namespace Audiology.Audio
{
    public abstract class ToneProvider :
        IAudioSourceProvider
    {
        public double Gain { get; set; } = 1;

        public double Frequency { get; set; } = DefaultFrequency;
        public IEnumerable<double>? Frequencies { get; set; }
        public const double DefaultFrequency = 440;

        public TimeSpan Duration { get; set; }
        public static readonly TimeSpan DefaultDuration = TimeSpan.FromSeconds(1);
        public bool HasDefaultDuration => Duration <= TimeSpan.Zero;

        public bool Loop => HasDefaultDuration;

        public async Task<AudioSource?> GetAudioSource(CancellationToken cancellation = default)
        {
            var duration = Duration;
            if (HasDefaultDuration)
                duration = DefaultDuration;
            var frequencies = Frequencies;
            double frequency;
            if (frequencies?.Any() == true)
                frequency = 0;
            else {
                frequencies = null;
                frequency = Frequency;
                if (frequency <= 0)
                    frequency = DefaultFrequency;
            }
            var result = await GetAudioSource(frequencies, frequency, duration, cancellation);
            return result;
        }

        protected abstract Task<AudioSource?> GetAudioSource(IEnumerable<double>? frequencies, double frequency, TimeSpan duration, CancellationToken cancellation);
    }
}
