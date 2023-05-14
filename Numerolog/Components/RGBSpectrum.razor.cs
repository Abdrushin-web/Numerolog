using Colorology;
using Colorology.Spectra;
using Microsoft.AspNetCore.Components;
using Spectrology;
using System.Diagnostics.CodeAnalysis;

namespace Numerolog.Components
{
    public partial class RGBSpectrum
    {
        [Parameter]
        public IRGBCircularSpectrum? Value { get; set; }
        [Parameter]
        public int Count { get; set; } = 24;
        [Parameter]
        public int Start { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            SetColors();
        }

        private void SetColors() => colors = Value?.
            GetValues(Circle.Degree.Divide((uint)Count, Start)).
            Select(HtmlColors.ToHtml).
            ToArray();

        IReadOnlyList<string>? colors;
    }
}