using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Numerology
{
    /// <summary>
    /// Numerical alphabet
    /// </summary>
    public class Alphabet
    {
        public Alphabet(CultureInfo culture, IEnumerable<(IEnumerable<string> texts, Number number)> letters, bool ignoreCase = true)
            : this(
                  culture,
                  letters?.
                    SelectMany(i => i.texts.Select(text => (text, i.number))).
                    ToDictionary(
                        letter => letter.text,
                        letter => letter.number,
                        StringComparer.Create(culture, ignoreCase))!)
        { }

        public Alphabet(CultureInfo culture, IEnumerable<TextNumber> letters, bool ignoreCase = true, string textSeparator = " ")
            : this(
                  culture,
                  letters?.Select(letter => (letter.Text.Split(textSeparator).AsEnumerable(), letter.Number))!,
                  ignoreCase)
        { }

        public Alphabet(CultureInfo culture, IReadOnlyDictionary<string, Number> letters)
        {
            Culture = culture ?? throw new ArgumentNullException(nameof(culture));
            Letters = letters ?? throw new ArgumentNullException(nameof(letters));
            LetterLengths = letters.
                Select(letter =>
                {
                    if (string.IsNullOrWhiteSpace(letter.Key))
                        throw new ArgumentException($"Letter with number {letter.Value} cannot be white space");
                    if (letter.Value is null)
                        throw new ArgumentNullException($"Letter \"{letter.Key}\" cannot have null number");
                    return letter.Key.Length;
                }).
                Distinct().
                Order().
                ToArray();
        }

        /// <summary>
        /// Non-null culture
        /// </summary>
        public CultureInfo Culture { get; }
        /// <summary>
        /// Known letters with their numbers
        /// </summary>
        public IReadOnlyDictionary<string, Number> Letters { get; }
        /// <summary>
        /// Min to max letter length
        /// </summary>
        public IReadOnlyList<int> LetterLengths { get; }

        /// <summary>
        /// Reads letters from <paramref name="text"/> and computes its number
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>Number if <paramref name="text"/> is not null or white space, otherwise null</returns>
        public TextNumber? ComputeLetters(string? text) => Read(text).Compute();

        public TextNumber? ComputeLinesAndWords(string? text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            var linesWords = text.SplitLinesAndWords(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries, true)!;
            var results = linesWords.Select(lineWords => lineWords!.Select(ComputeLetters).NonNull().Compute()).NonNull();
            var result = results.Compute();
            return result;
        }

        /// <summary>
        /// Reads letters
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="cancellation">Optional cancellation</param>
        /// <returns>Known letters have non-null <see cref="TextNumber.Number"/> and unknown null one</returns>
        public IEnumerable<TextNumber> Read(string? text, CancellationToken cancellation =default)
        {
            if (string.IsNullOrWhiteSpace(text))
                yield break;
            int letterStart = 0;
            while (ReadLetter(text, ref letterStart, out var letter)) {
                if (cancellation.IsCancellationRequested)
                    yield break;
                yield return letter;
            }
        }

        private bool ReadLetter(string text, ref int letterStart, [NotNullWhen(true)] out TextNumber? letter)
        {
            // max to min letter length
            for (int i = LetterLengths.Count - 1; i >= 0; i--) {
                var letterLength = LetterLengths[i];
                var hasNextLetter = letterStart + letterLength <= text.Length;
                if (hasNextLetter) {
                    var letterText = text.Substring(letterStart, letterLength);
                    var minLength = i == 0;
                    if (// known
                        Letters.TryGetValue(letterText, out var number) ||
                        // min length letter unknown
                        minLength) {
                        letterStart += letterLength;
                        if (number is null)
                            JoinUnknownLetters(text, ref letterStart, ref letterText);
                        letter = new TextNumber(letterText, number);
                        return true;
                    }
                }
            }
            letter = null;
            return false;
        }

        private void JoinUnknownLetters(string text, ref int letterStart, ref string letterText)
        {
            while (ReadLetter(text, ref letterStart, out var letter)) {
                if (letter.HasNumber) {
                    letterStart -= letter.Text.Length;
                    break;
                }
                else
                    letterText += letter.Text;
            }
        }
    }
}