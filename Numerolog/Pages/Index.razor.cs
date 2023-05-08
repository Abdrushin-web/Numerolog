using Microsoft.AspNetCore.Components;
using Numerology;
using System.Diagnostics.CodeAnalysis;

namespace Numerolog.Pages
{
    public partial class Index
    {
        #region Name

        [Parameter]
        [SupplyParameterFromQuery]
        public string? Name { get; set; }

        [MemberNotNullWhen(true, nameof(name))]
        private bool HasName => !string.IsNullOrWhiteSpace(name);

        private void OnNameChanged(ChangeEventArgs a) => OnNameChanged((string)a.Value!);
        private void OnNameChanged(string value)
        {
            name = value;
            UpdateUri();
        }

        private string? name;

        #endregion

        #region Text

        [Parameter]
        [SupplyParameterFromQuery]
        public string? Text { get; set; }

        [MemberNotNullWhen(true, nameof(text))]
        private bool HasText => !string.IsNullOrWhiteSpace(text);

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
            name = Name;
            text = Text;
            showDetails = ShowDetails;
            if (uriUpdated)
                uriUpdated = false;
            else
                await SetResult();
        }

        private string Title
        {
            get
            {
                const string separator = " | ";
                var value = HasName || HasText ?
                    (HasName ? name : text)!.
                        Replace(TextLines.Separator, separator).
                        TrimToLength(100) +
                        separator :
                        null;
                if (HasResult && result.HasNumber)
                    value += $"{result.Number.ValueText} = {result.Number.SingleDigitSumText}{separator}";
                value += Application.Name;
                return value;
            }
        }

        #region Uri

        private string Uri => Navigation.GetUriWithQueryParameters(new Dictionary<string, object?>
        {
            [nameof(Name)] = HasName ? name : null,
            [nameof(Text)] = HasText ? text : null,
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

        [MemberNotNullWhen(true, nameof(result))]
        private bool HasResult => result != null;

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