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
}
