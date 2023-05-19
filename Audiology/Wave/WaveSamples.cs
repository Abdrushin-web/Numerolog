using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Audiology.Wave
{
    public static class WaveSamples
    {
        public static ISampleProvider Mix(this IEnumerable<ISampleProvider> sources)
        {
            if (sources is null)
                throw new ArgumentNullException(nameof(sources));
            return new MixingSampleProvider(sources);
        }

        public static ISampleProvider Sequence(this IEnumerable<ISampleProvider> sources, TimeSpan silenceBetweenDuration = default)
        {
            if (sources is null)
                throw new ArgumentNullException(nameof(sources));
            return sources.Aggregate((i1, i2) => silenceBetweenDuration == default ?
                i1.FollowedBy(i2) :
                i1.FollowedBy(silenceBetweenDuration, i2));
        }
    }
}
