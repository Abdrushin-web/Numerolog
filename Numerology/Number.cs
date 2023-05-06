using System.Globalization;

namespace Numerology
{
    /// <summary>
    /// Positive number
    /// </summary>
    public class Number
    {
        public Number(ushort value)
        {
            if (value == 0)
                throw new ArgumentOutOfRangeException(nameof(value), value, "Value must be positive");
            Value = value;
            SingleDigitSum = GetSingleDigitSum(value);
        }

        public static implicit operator Number(ushort value) => new Number(value);

        /// <summary>
        /// Multidigit positive value
        /// </summary>
        public ushort Value { get; }
        public string ValueText => Value.ToString("000", CultureInfo.InvariantCulture);
        /// <summary>
        /// Single digit sum of <see cref="Value"/> digits
        /// </summary>
        public byte SingleDigitSum { get; }
        public string SingleDigitSumText => SingleDigitSum.ToString(CultureInfo.InvariantCulture);

        public static byte GetSingleDigitSum(ushort value)
        {
            while (value > 9)
                value = (ushort)GetDigits(value).Sum(i => i);
            return (byte)value;
        }

        public static IEnumerable<byte> GetDigits(ushort value)
        {
            var valueText = value.ToString(CultureInfo.InvariantCulture);
            return valueText.Select(ParseDigit);
        }

        public static byte ParseDigit(char digit) => byte.Parse(new ReadOnlySpan<char>(digit));
        public static char FormatDigit(byte digit) => digit.ToString()[0];

        public override string ToString() => $"{ValueText} = {SingleDigitSumText}";
    }
}