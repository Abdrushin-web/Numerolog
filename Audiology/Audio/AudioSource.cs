namespace Audiology.Audio
{
    public class AudioSource
    {
        public AudioSource(byte[] bytes, string type, bool loop = false)
        {
            if (bytes is null)
                throw new ArgumentNullException(nameof(bytes));
            if (bytes.Length == 0)
                throw new ArgumentException("Cannot be empty", nameof(bytes));
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException($"'{nameof(type)}' cannot be null or whitespace.", nameof(type));
            Bytes = bytes;
            Type = type;
            Loop = loop;
        }

        public byte[] Bytes { get; }
        public string Type { get; }
        public bool Loop { get; }

        public Stream ToStream() => new MemoryStream(Bytes);

        public async Task ToFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));
            await File.WriteAllBytesAsync(path, Bytes);
        }
    }
}
