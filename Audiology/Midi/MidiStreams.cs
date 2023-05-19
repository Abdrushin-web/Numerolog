using Melanchall.DryWetMidi.Core;

namespace Audiology.Midi
{
    public static class MidiStreams
    {
        public static async Task ToStream(this MidiFile midi, Stream stream, MidiFileFormat format = MidiFileFormat.SingleTrack, WritingSettings? settings = null)
        {
            if (midi is null)
                throw new ArgumentNullException(nameof(midi));
            await Task.Run(() => midi.Write(stream, format, settings));
        }

        public static async Task<byte[]> ToBytes(this MidiFile midi, MidiFileFormat format = MidiFileFormat.SingleTrack, WritingSettings? settings = null)
        {
            using var stream = new MemoryStream();
            await midi.ToStream(stream, format, settings);
            return stream.ToArray();
        }
    }
}
