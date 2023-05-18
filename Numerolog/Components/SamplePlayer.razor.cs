using Audiology;
using NAudio.Wave;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Numerolog.Components
{
    public abstract partial class SamplePlayer
    {
        protected virtual bool Loop => false;

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            if (initialized)
                await LoadSample();
        }

        [SupportedOSPlatform("browser")]
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await JSHost.ImportAsync(nameof(SamplePlayer), $"/{nameof(SamplePlayer)}.js");
            initialized = true;
        }

        async Task LoadSample()
        {
            cancellation?.Cancel();
            cancellation?.Dispose();
            cancellation = new CancellationTokenSource();
            if (cancellation.IsCancellationRequested)
                return;
            var sample = await GetSample(cancellation.Token);
            if (sample == null ||
                cancellation.IsCancellationRequested) {
                url = null;
            } else {
                var bytes = await sample.ToBytes();
                url = cancellation.IsCancellationRequested ?
                    null :
                    GetUrl(bytes);
            }
        }

        protected abstract Task<ISampleProvider?> GetSample(CancellationToken cancellation);

        [JSImport(nameof(GetUrl), nameof(SamplePlayer))]
        private static partial string GetUrl(byte[] bytes);

        string? url;
        CancellationTokenSource? cancellation;
        private bool initialized;
    }
}