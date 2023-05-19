using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Numerolog.Components
{
    public abstract partial class AudioPlayer :
        IDisposable
    {
        [Parameter]
        public bool AutoPlay { get; set; } = false;
        [Parameter]
        public bool ShowControls { get; set; } = true;
        [Parameter]
        public bool Loop { get; set; } = false;

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

        async Task Load()
        {
            cancellation?.Cancel();
            cancellation?.Dispose();
            cancellation = new CancellationTokenSource();
            if (cancellation.IsCancellationRequested)
                return;
            var (bytes, type) = await GetAudio(cancellation.Token);
            if (bytes is null ||
                bytes.Length == 0 ||
                string.IsNullOrWhiteSpace(type) ||
                cancellation.IsCancellationRequested) {
                url = null;
            } else
                url = GetUrl(bytes, type);
        }

        protected abstract Task<(byte[]? bytes, string? type)> GetAudio(CancellationToken cancellation);

        [JSImport(nameof(GetUrl), nameof(AudioPlayer))]
        private static partial string GetUrl(byte[] bytes, string type);

        public void Dispose() => cancellation?.Dispose();

        string? url;
        CancellationTokenSource? cancellation;
        private bool initialized;
    }
}