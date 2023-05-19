namespace Audiology.Audio
{
    public interface IAudioSourceProvider
    {
        Task<AudioSource?> GetAudioSource(CancellationToken cancellation = default);
    }
}
