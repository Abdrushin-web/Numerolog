using Audiology.Audio;
using Audiology.Midi;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Numerolog.Components
{
    public partial class AudioPlayer :
        IDisposable
    {
        [Parameter]
        public bool AutoPlay { get; set; } = false;
        [Parameter]
        public bool ShowControls { get; set; } = true;
        [Parameter]
        public IAudioSourceProvider? Provider { get; set; }

        bool Loop => source?.Loop ?? false;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (initialized)
                await Load();
        }

        [SupportedOSPlatform("browser")]
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await JSHost.ImportAsync(nameof(AudioPlayer), $"/{nameof(AudioPlayer)}.js");
            initialized = true;
        }

        bool Midi => source?.Type == MidiSource.Type;
        readonly Guid MidiId = Guid.NewGuid();

        async Task Load()
        {
            cancellation?.Cancel();
            cancellation?.Dispose();
            cancellation = new CancellationTokenSource();
            source = null;
            url = null;
            if (Provider is null ||
                cancellation.IsCancellationRequested) {
                return;
            }
            try {
                source = await Provider.GetAudioSource(cancellation.Token);
                if (source is not null &&
                    !cancellation.IsCancellationRequested) {
                    url = GetUrl(source.Bytes, source.Type);
                }
            }
            catch (OperationCanceledException) {
                // ok
            }
        }

        [JSImport(nameof(GetUrl), nameof(AudioPlayer))]
        private static partial string GetUrl(byte[] bytes, string type);

        public void Dispose() => cancellation?.Dispose();

        string? url;
        CancellationTokenSource? cancellation;
        bool initialized;
        AudioSource? source;
    }
}