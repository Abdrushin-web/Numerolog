using NAudio.Utils;
using NAudio.Wave;

namespace Audiology
{
    public static class WaveStreams
    {
        public static async Task ToStream(this ISampleProvider sample, Stream stream)
        {
            if (sample is null)
                throw new ArgumentNullException(nameof(sample));
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            var wave = sample.ToWaveProvider();
            await wave.ToStream(stream);
        }

        public static async Task ToStream(this IWaveProvider wave, Stream stream)
        {
            if (wave is null)
                throw new ArgumentNullException(nameof(wave));
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            await Task.Yield();
            using var waveFileWriter = new WaveFileWriter(new IgnoreDisposeStream(stream), wave.WaveFormat);
            var array = new byte[wave.WaveFormat.AverageBytesPerSecond * 4];
            while (true) {
                int num = wave.Read(array, 0, array.Length);
                if (num == 0)
                    break;
                await waveFileWriter.WriteAsync(array, 0, num);
            }
            await stream.FlushAsync();
        }

        public static async Task ToFile(this ISampleProvider sample, string path)
        {
            if (sample is null)
                throw new ArgumentNullException(nameof(sample));
            using var stream = File.OpenWrite(path);
            try {
                await sample.ToStream(stream);
            }
            catch {
                File.Delete(path);
            }
        }

        public static async Task<byte[]> ToBytes(this ISampleProvider sample)
        {
            using var stream = new MemoryStream();
            await sample.ToStream(stream);
            return stream.ToArray();
        }
    }
}
