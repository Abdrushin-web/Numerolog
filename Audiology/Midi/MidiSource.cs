using Audiology.Audio;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace Audiology.Midi
{
    public class MidiSource :
        AudioSource
    {
        public new const string Type = "audio/x-midi";
        public const string FileExtension = "midi";

        public MidiSource(byte[] bytes, bool loop = false) :
            base(bytes, Type, loop)
        { }

        public static async Task<MidiSource?> Create(
            Func<CancellationToken, Task<Pattern?>> getPattern,
            TempoMap? tempoMap = null,
            MidiFileFormat format = MidiFileFormat.SingleTrack,
            WritingSettings? settings = null,
            bool loop = false,
            CancellationToken cancellation = default)
        {
            if (getPattern is null)
                throw new ArgumentNullException(nameof(getPattern));
            var pattern = await getPattern(cancellation);
            if (pattern is null ||
                cancellation.IsCancellationRequested) {
                return default;
            }
            tempoMap ??= TempoMap.Default;
            var midi = pattern.ToFile(tempoMap);
            if (cancellation.IsCancellationRequested)
                return default;
            var bytes = await midi.ToBytes(format, settings);
            return new(bytes, loop);
        }
    }
}
