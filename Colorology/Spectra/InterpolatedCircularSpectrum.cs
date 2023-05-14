using Colorology.Interpolation;
using Colourful;
using Spectrology;
using System.Diagnostics.CodeAnalysis;

namespace Colorology.Spectra
{
    public class InterpolatedCircularSpectrum :
        RGBCircularSpectrum
    {
        public InterpolatedCircularSpectrum(string name, [NotNull] IRGBInterpolation interpolation, params RGBColor[] colors)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            if (colors is null)
                throw new ArgumentNullException(nameof(colors));
            if (colors.Length == 0)
                throw new ArgumentException("At least one color is required", nameof(colors));
            this.name = name;
            Interpolation = interpolation;
            Colors = colors;
        }

        public static readonly InterpolatedCircularSpectrum Colors7 = new(
            "7 barev",
            new LChLinearInterpolation(),
            MacbethColorChecker.Red,
            MacbethColorChecker.Orange,
            MacbethColorChecker.Yellow,
            MacbethColorChecker.Green,
            MacbethColorChecker.Blue,
            MacbethColorChecker.Purple,
            MacbethColorChecker.Magenta);

        public static readonly InterpolatedCircularSpectrum Colors12 = new(
            "12 barev",
            new LChLinearInterpolation(),
            MacbethColorChecker.Red,
            MacbethColorChecker.Orange,
            MacbethColorChecker.OrangeYellow,
            MacbethColorChecker.Yellow,
            MacbethColorChecker.YellowGreen,
            MacbethColorChecker.Green,
            MacbethColorChecker.BluishGreen,
            MacbethColorChecker.BlueSky,
            MacbethColorChecker.Blue,
            MacbethColorChecker.PurplishBlue,
            MacbethColorChecker.Purple,
            MacbethColorChecker.Magenta);

        public override string Name => name;

        [MemberNotNull(nameof(interpolation))]
        public IRGBInterpolation Interpolation
        {
            get => interpolation;
            set => interpolation = value ?? throw new ArgumentNullException(nameof(Interpolation));
        }
        public IReadOnlyList<RGBColor> Colors { get; }

        protected override RGBColor DoGetValue(double degree)
        {
            var count = Colors.Count;
            if (count == 1)
                return Colors[0];
            var indexRatio = Circle.Degree.GetSegmentIndexRatio((uint)count, degree);
            var index = (int)indexRatio;
            var ratio = indexRatio - index;
            var index2 = index + 1;
            if (index2 == count)
                index2 = 0;
            var result = Interpolation.GetColor(Colors[index], Colors[index2], ratio);
            return result;
        }

        string name;
        IRGBInterpolation interpolation;
    }
}
