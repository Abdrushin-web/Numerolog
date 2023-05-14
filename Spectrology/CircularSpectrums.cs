namespace Spectrology
{
    public static class CircularSpectrums
    {
        public static IEnumerable<T> GetValues<T>(this ICircularSpectrum<T> spectrum, IEnumerable<double> degrees)
        {
            if (spectrum is null)
                throw new ArgumentNullException(nameof(spectrum));
            if (degrees is null)
                throw new ArgumentNullException(nameof(degrees));
            return degrees.Select(spectrum.GetValue);
        }
    }
}
