using Audiology.Audio;
using NAudio.Wave;

namespace Audiology.Wave
{
    public class WaveToneProvider :
        ToneProvider
    {
        protected override async Task<AudioSource?> GetAudioSource(IEnumerable<double>? frequencies, double frequency, TimeSpan duration, CancellationToken cancellation) => await WaveSource.Create(
            cancellation =>
            {
                var result = frequencies is null ?
                    frequency.Tone(duration, Gain) :
                    frequencies.
                        Tones(duration, false, Gain).
                        FollowedBy(frequencies.Tones(duration * 2, true, Gain));
                return Task.FromResult<ISampleProvider?>(result);
            },
            Loop,
            cancellation);
    }
}
