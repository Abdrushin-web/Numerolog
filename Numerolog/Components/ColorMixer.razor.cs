using Colorology;
using Microsoft.AspNetCore.Components;

namespace Numerolog.Components
{
    public partial class ColorMixer
    {
        [Parameter]
        public IRGBInterpolation? Mixer { get; set; }
        [Parameter]
        public string? Color1 { get; set; }

        [Parameter]
        public string? Color2 { get; set; }
        [Parameter]
        public double Ratio { get; set; } = 0.5;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            var c1 = Color1?.HtmlToRGBColor();
            var c2 = Color2?.HtmlToRGBColor();
            color = c1.HasValue && c2.HasValue ?
                Mixer?.GetColor(c1.Value, c2.Value, Ratio).ToHtml() :
                null;
        }

        string? color;
    }
}