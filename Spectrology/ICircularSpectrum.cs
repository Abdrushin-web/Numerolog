namespace Spectrology
{
    public interface ICircularSpectrum<T>
    {
        string Name { get; }

        T GetValue(double degree);
    }
}