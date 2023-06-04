using Audiology.Audio;
using Audiology.Midi;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Standards;
using MeltySynth;
using Microsoft.AspNetCore.Components;

namespace Numerolog.Components
{
    public partial class MidiInstrument
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await MidiSynthesizer.SetDefaultSynthesizerFromHttp(Http);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Synthesizer ??= MidiSynthesizer.DefaultSynthesizer;
        }

        [Parameter]
        public double Gain { get; set; } = 1;
        [Parameter]
        public Synthesizer? Synthesizer { get; set; }
        [Parameter]
        public GeneralMidi2Program Instrument { get; set; } = GeneralMidi2Program.AcousticGrandPiano;

        public IEnumerable<GeneralMidi2Program> Instruments => StaticInstruments;

        public static readonly GeneralMidi2Program[] StaticInstruments = Enum.GetValues<GeneralMidi2Program>().
            OrderBy(i => i.ToString()).
            ToArray();

        public virtual ToneProvider CreateToneProvider() => new MidiToneProvider
        {
            Gain = Gain,
            Instrument = Instrument,
            Synthesizer = Synthesizer
        };
    }
}