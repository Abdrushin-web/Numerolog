using Audiology;
using NAudio.Wave;

namespace Numerolog.Components
{
    public abstract partial class SamplePlayer :
        AudioPlayer
    {
        protected sealed override async Task<(byte[]? bytes, string? type)> GetAudio(CancellationToken cancellation)
        {
            var sample = await GetSample(cancellation);
            if (sample is null ||
                cancellation.IsCancellationRequested) {
                return default;
            } else {
                var bytes = await sample.ToBytes();
                return (bytes, "audio/x-wav");
            }
        }

        protected abstract Task<ISampleProvider?> GetSample(CancellationToken cancellation);
  }
}