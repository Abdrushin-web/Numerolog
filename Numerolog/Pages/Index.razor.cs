using Microsoft.AspNetCore.Components;
using Numerology;

namespace Numerolog.Pages
{
    public partial class Index
    {
        private void OnTextChanged(ChangeEventArgs a) => OnTextChanged((string)a.Value!);
        private void OnTextChanged(string value)
        {
            text = value;
            result = Alphabet.ComputeLinesAndWords(value);
        }

        private void ToggleShowDetails() => showDetails = !showDetails;

        private string text;
        private TextNumber? result;
        private bool showDetails;
    }
}