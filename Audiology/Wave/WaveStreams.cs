using NAudio.Utils;
using NAudio.Wave;

namespace Audiology.Wave
{
    public static class WaveStreams
    {
        public static async Task ToStream(this ISampleProvider sample, Stream stream, CancellationToken cancellation = default)
        {
            if (sample is null)
                throw new ArgumentNullException(nameof(sample));
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            var wave = sample.ToWaveProvider();
            cancellation.ThrowIfCancellationRequested();
            await wave.ToStream(stream, cancellation);
        }

        public static async Task ToStream(this IWaveProvider wave, Stream stream, CancellationToken cancellation = default)
        {
            if (wave is null)
                throw new ArgumentNullException(nameof(wave));
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            await Task.Yield();
            using var waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(stream), wave.WaveFormat);
            var array = new byte[wave.WaveFormat.AverageBytesPerSecond * 4];
            while (true)
            {
                cancellation.ThrowIfCancellationRequested();
                int num = wave.Read(array, 0, array.Length);
                if (num == 0)
                    break;
                await waveFileWriter.WriteAsync(array, 0, num, cancellation);
            }
            await stream.FlushAsync();
        }

        public static async Task<byte[]> ToBytes(this ISampleProvider sample, CancellationToken cancellation = default)
        {
            using var stream = new MemoryStream();
            await sample.ToStream(stream, cancellation);
            return stream.ToArray();
        }
    }
}
