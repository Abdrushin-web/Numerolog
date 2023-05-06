using System.Globalization;

namespace Numerology
{
    /// <summary>
    /// <see cref="Number"/> helper
    /// </summary>
    public static class Numbers
    {
        public static Number? SumByDigits(this IEnumerable<Number?> numbers)
        {
            if (numbers is null)
                throw new ArgumentNullException(nameof(numbers));
            var result = numbers.Aggregate(SumByDigits);
            return result;
        }

        public static Number? SumByDigits(this Number? number1, Number? number2) =>
            number1 is null ?
                number2 :
                number2 is null ?
                    number1 :
                    new Number(SumByDigits(number1.Value, number2.Value));

        public static ushort SumByDigits(this ushort value1, ushort value2)
        {
            var digits1 = Number.GetDigits(value1).Reverse();
            var digits2 = Number.GetDigits(value2).Reverse();
            using var digits1Enumerator = digits1.GetEnumerator();
            using var digits2Enumerator = digits2.GetEnumerator();
            bool hasNext1, hasNext2;
            string? resultText = null;
            while (true) {
                hasNext1 = digits1Enumerator.MoveNext();
                hasNext2 = digits2Enumerator.MoveNext();
                if (!hasNext1 && !hasNext2)
                    break;
                var digit1 = hasNext1 ? digits1Enumerator.Current : 0;
                var digit2 = hasNext2 ? digits2Enumerator.Current : 0;
                var digit = Number.GetSingleDigitSum((ushort)(digit1 + digit2));
                resultText = Number.FormatDigit(digit) + resultText;
            }
            var result = resultText is null ?
                ushort.MinValue :
                ushort.Parse(resultText, CultureInfo.InvariantCulture);
            return result;
        }
    }
}
