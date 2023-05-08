using Microsoft.AspNetCore.Components;
using Numerology;

namespace Numerolog.Pages
{
    public partial class Index
    {
        #region Text

        [Parameter]
        [SupplyParameterFromQuery]
        public string? Text { get; set; }

        private Task OnTextChanged(ChangeEventArgs a) => OnTextChanged((string)a.Value!);
        private async Task OnTextChanged(string value)
        {
            text = value;
            await SetResult();
            UpdateUri();
        }

        private string? text;

        #endregion

        #region ShowDetails

        [Parameter]
        [SupplyParameterFromQuery]
        public bool ShowDetails { get; set; }

        private void ToggleShowDetails()
        {
            showDetails = !showDetails;
            UpdateUri();
        }

        private bool showDetails;

        #endregion

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            text = Text;
            showDetails = ShowDetails;
            if (uriUpdated)
                uriUpdated = false;
            else
                await SetResult();
        }

        private string Title => string.IsNullOrWhiteSpace(text) ?
            Application.Name :
            $"{text.TrimToLength(100)} | {Application.Name}";

        #region Uri

        private string Uri => Navigation.GetUriWithQueryParameters(new Dictionary<string, object?>
        {
            [nameof(Text)] = string.IsNullOrWhiteSpace(text) ? null : text,
            [nameof(ShowDetails)] = showDetails ? true : null
        });

        private void UpdateUri()
        {
            Navigation.NavigateTo(Uri);
            uriUpdated = true;
        }

        private bool uriUpdated;

        #endregion

        #region Result

        private bool Computing => cancellation != null;

        private async Task SetResult()
        {
            this.cancellation?.Cancel();
            using var cancellation = this.cancellation = new CancellationTokenSource();
            try {
                await Task.Run(
                    () => result = Alphabet.ComputeLinesAndWords(text),
                    cancellation.Token);
            }
            finally {
                this.cancellation = null;
            }
        }

        private TextNumber? result;
        private CancellationTokenSource? cancellation;

        #endregion
    }
}