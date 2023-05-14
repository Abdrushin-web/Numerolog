using Colorology;
using Microsoft.AspNetCore.Components;

namespace Numerolog.Components
{
    public partial class RGBSpectra
    {
        [Parameter]
        public IEnumerable<IRGBCircularSpectrum>? List { get; set; }
        [Parameter]
        public int Count { get; set; } = 24;
        [Parameter]
        public int Start { get; set; }
    }
}