using System.Runtime.CompilerServices;

namespace Harmonology
{
    public static class Frequency
    {
        public static void Validate(double value, [CallerMemberName] string? name = null)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(name, value, "Value must be positive");
        }
    }
}
