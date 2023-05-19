using Audiology.Audio;
using NAudio.Wave;

namespace Audiology.Wave
{
    public class WaveSource :
        AudioSource
    {
        public new const string Type = "audio/x-wav";
        public const string FileExtension = "wav";

        public WaveSource(byte[] bytes, bool loop = false) :
            base(bytes, Type, loop)
        { }

        public static async Task<WaveSource?> Create(
            Func<CancellationToken, Task<ISampleProvider?>> getSample,
            bool loop = false,
            CancellationToken cancellation = default)
        {
            if (getSample is null)
                throw new ArgumentNullException(nameof(getSample));
            var sample = await getSample(cancellation);
            if (sample is null ||
                cancellation.IsCancellationRequested) {
                return default;
            }
            var bytes = await sample.ToBytes(cancellation);
            return new(bytes, loop);
        }
    }
}
