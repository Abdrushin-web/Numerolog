using System.Globalization;
using System.Text;

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

        #region Value

        /// <summary>
        /// Multidigit positive value
        /// </summary>
        public ushort Value { get; }
        public string ValueText => Value.ToString("000", CultureInfo.InvariantCulture);

        public const char UnknownDigit = '0';
        private static readonly string UnknownDigitText = UnknownDigit.ToString();

        public string GetValueText(
            Func<string?, string?>? formatKnown = null,
            Func<string?, string?>? formatUnknown = null)
        {
            string text;
            if (formatKnown is null &&
                formatUnknown is null) {
                text = ValueText;
            }
            else {
                var builder = new StringBuilder();
                foreach (var digit in ValueText) {
                    builder.Append(digit == UnknownDigit ?
                        TextNumber.Format(UnknownDigitText, formatUnknown) :
                        TextNumber.Format(digit.ToString(), formatKnown));
                }
                text = builder.ToString();
            }
            return text;
        }

        public static IEnumerable<byte> GetDigits(ushort value)
        {
            var valueText = value.ToString(CultureInfo.InvariantCulture);
            return valueText.Select(ParseDigit);
        }

        public static byte ParseDigit(char digit) => byte.Parse(new ReadOnlySpan<char>(digit));
        public static char DigitToString(byte digit) => digit.ToString()[0];

        #endregion

        #region SingleDigitSum

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

        #endregion

        public override string ToString() => $"{ValueText} = {SingleDigitSumText}";
    }
}