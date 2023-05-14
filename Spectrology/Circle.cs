namespace Spectrology
{
    public class Circle
    {
        public Circle(double full)
        {
            if (full <= 0)
                throw new ArgumentOutOfRangeException(nameof(full), full, "Value must be positive");
            Full = full;
        }

        public static readonly Circle Degree = new Circle(360);

        public double Full { get; }

        public double GetSegmentWidth(uint count) => count == 0 ?
            0 :
            Full / count;

        public IEnumerable<double> Divide(uint count, double start = 0, bool normalize = true)
        {
            if (count == 0)
                yield break;
            if (count == 1) {
                if (normalize)
                    start = Normalize(start);
                yield return start;
                yield break;
            }
            var step = GetSegmentWidth(count);
            var degree = start;
            for (uint i = 0; i < count; i++) {
                if (normalize)
                    degree = Normalize(degree);
                yield return degree;
                degree += step;
            }
        }

        public (double from, double center, double to) GetSegment(uint count, double center, bool normalize = true) => GetSegment(GetSegmentWidth(count) / 2, center, normalize);

        public (double from, double center, double to) GetSegment(double halfWidth, double center, bool normalize = true)
        {
            var segment = (from: center - halfWidth, center, to: center + halfWidth);
            if (normalize)
                segment = (Normalize(segment.from), Normalize(segment.center), Normalize(segment.to));
            return segment;
        }

        public double GetSegmentIndexRatio(uint count, double angle, double startCenter = 0) => GetSegmentIndexRatio(GetSegmentWidth(count), angle, startCenter);

        public double GetSegmentIndexRatio(double width, double angle, double startCenter = 0) => Math.Round(Normalize(angle - startCenter) / width, 12);

        public double Normalize(double value)
        {
            value %= Full;
            if (value < 0)
                value += Full;
            return value;
        }
    }
}
