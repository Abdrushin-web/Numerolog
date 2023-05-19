using MeltySynth;
using NAudio.Wave;

namespace Audiology.Midi
{
    public class MidiSynthesizer :
        ISampleProvider
    {
        public MidiSynthesizer(Stream stream, Synthesizer? synthesizer = null)
        {
            if (stream is null)
                throw new ArgumentNullException(nameof(stream));
            synthesizer ??= DefaultSynthesizer;
            if (synthesizer is null)
                throw new ArgumentNullException(nameof(DefaultSynthesizer));
            Synthesizer = synthesizer;
            WaveFormat = WaveFormat.CreateIeeeFloatWaveFormat(synthesizer.SampleRate, 2);
            channels = new(() =>
            {
                var midi = new MidiFile(stream);
                var sequencer = new MidiFileSequencer(synthesizer);
                sequencer.Play(midi, false);
                var sampleCount = (int)(synthesizer.SampleRate * midi.Length.TotalSeconds);
                var left = new float[sampleCount];
                var right = new float[sampleCount];
                sequencer.Render(left, right);
                return (left, right);
            });
        }

        public const int DefaultSampleRate = 44100;
        public const string DefaultSoundFontPath = "SoundFonts/TimGM6mb.sf2";
        public static Synthesizer? DefaultSynthesizer;

        public static async Task SetDefaultSynthesizer(
            Func<Task<Stream>>? getSoundFontStream = null,
            SynthesizerSettings? settings = null,
            bool skipIfNotNull = true)
        {
            if (skipIfNotNull &&
                DefaultSynthesizer != null) {
                return;
            }
            Stream soundFontStream;
            if (getSoundFontStream is null)
                soundFontStream = File.OpenRead(DefaultSoundFontPath);
            else {
                soundFontStream = await getSoundFontStream();
                if (soundFontStream is null)
                    throw new ArgumentNullException(nameof(soundFontStream));
            }
            settings ??= new(DefaultSampleRate);
            DefaultSynthesizer = await Task.Run(() => new Synthesizer(new SoundFont(soundFontStream), settings));
        }

        public static async Task SetDefaultSynthesizerFromHttp(
            HttpClient? client = null,
            string? soundFontPath = null,
            SynthesizerSettings? settings = null,
            bool skipIfNotNull = true)
        {
            soundFontPath ??= "/" + DefaultSoundFontPath;
            client ??= new HttpClient();
            await SetDefaultSynthesizer(() => client.GetStreamAsync(soundFontPath), settings, skipIfNotNull);
        }

        public Synthesizer Synthesizer { get; }
        public WaveFormat WaveFormat { get; }

        public int Read(float[] buffer, int offset, int count)
        {
            (var left, var right) = channels.Value;
            count /= 2;
            count = Math.Min(count, left.Length - currentIndex);
            int index = offset;
            for (int i = 0; i < count; i++) {
                buffer[index++] = left[currentIndex];
                buffer[index++] = right[currentIndex];
                currentIndex++;
            }
            return count * 2;
        }

        private readonly Lazy<(float[] left, float[] right)> channels;
        private int currentIndex;
    }
}
