using Melanchall.DryWetMidi.MusicTheory;
using Microsoft.AspNetCore.Components;

namespace Numerolog.Components
{
    public partial class FrequencyInput
    {
        public FrequencyInput()
            => Value = Tone.ConcertA.Frequency;

        [Parameter]
        public string? Name { get; set; } = "Frekvence";
        [Parameter]
        public uint NameNumber { get; set; }
        public string NameNumberText => NameNumber == 0 ? string.Empty : NameNumber.ToString();
        [Parameter]
        public double Step { get; set; } = 1;
        [Parameter]
        public double Min { get; set; } = Tone.MinFrequency;
        [Parameter]
        public double Max { get; set; } = Tone.MaxFrequency;
    }
}