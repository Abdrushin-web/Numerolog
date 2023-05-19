using Audiology.Wave;
using MeltySynth;
using NAudio.Wave;

namespace Audiology.Midi
{
    public static class MidiSynthesizers
    {
        public static ISampleProvider MidiToSampleProvider(this Stream stream, Synthesizer? synthesizer = null)
            => new MidiSynthesizer(stream, synthesizer);

        public static async Task<WaveSource> ToWave(this MidiSource source, Synthesizer? synthesizer = null, CancellationToken cancellation = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            var bytes = await source.
                ToStream().
                MidiToSampleProvider(synthesizer).
                ToBytes(cancellation);
            return new(bytes, source.Loop);
        }
    }
}
