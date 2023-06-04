namespace Spectrology
{
    public abstract class CircularSpectrum<T> :
        ICircularSpectrum<T>
    {
        public abstract string Name {get; }

        public T GetValue(double degree)
        {
            degree = Circle.Degree.Normalize(degree);
            return DoGetValue(degree);
        }

        protected abstract T DoGetValue(double degree);
    }


    public static class CircularSpectrum
    {
        public static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }
    }
}
