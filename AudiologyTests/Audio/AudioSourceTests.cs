using Audiology.Midi;
using Audiology.Wave;
using Xunit;

namespace Audiology.Audio.Tests
{
    public class AudioSourceTests
    {
        [Fact]
        public async Task ToFile()
        {
            await MidiSynthesizer.SetDefaultSynthesizer();
            var frequencies = new[] { 1000d, 1250, 1500 };
            var provider = new MidiToneProvider { Frequencies = frequencies };
            await SaveFile(provider, "chord1");
            frequencies[0] += 33;
            await SaveFile(provider, "chord2");
            frequencies[0] += 33;
            await SaveFile(provider, "chord3");
        }

        private static async Task SaveFile(MidiToneProvider provider, string path)
        {
            // MIDI
            var source = await provider.GetAudioSource();
            Assert.NotNull(source);
            path = Path.ChangeExtension(path, MidiSource.FileExtension);
            await source.ToFile(path);
            // WAVE
            provider.Synthesizer = MidiSynthesizer.DefaultSynthesizer;
            source = await provider.GetAudioSource();
            Assert.NotNull(source);
            path = Path.ChangeExtension(path, WaveSource.FileExtension);
            await source.ToFile(path);
            provider.Synthesizer = null;
        }
    }
}