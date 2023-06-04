using Spectrology;

namespace Harmonology
{
    public class OctaveCircularSpectrum :
        CircularSpectrum<double>
    {
        public OctaveCircularSpectrum(string name, double baseFrequency)
        {
            CircularSpectrum.ValidateName(name);
            Frequency.Validate(baseFrequency, nameof(baseFrequency));
            this.name = name;
            BaseFrequency = baseFrequency;
        }

        public override string Name => name;
        public double BaseFrequency { get; }

        protected override double DoGetValue(double degree)
        {
            var factor = 1 + degree / Circle.Degree.Full;
            return BaseFrequency * factor;
        }

        private readonly string name;
    }
}
