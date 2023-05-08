using System.Diagnostics.CodeAnalysis;
using static System.Net.Mime.MediaTypeNames;

namespace Numerology
{
    /// <summary>
    /// Text with number
    /// </summary>
    public class TextNumber
    {
        #region Init

        public TextNumber(string text, Number? number)
        {
            if (string.IsNullOrEmpty(text)) {
                throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
            }
            Text = text;
            Number = number;
        }

        public TextNumber(IEnumerable<TextNumber> source, CancellationToken cancellation = default) :
            this(GetText(source, out var number, out var sourceList, cancellation), number)
            => Source = sourceList;

        private static string GetText(IEnumerable<TextNumber> source, out Number? number, out IReadOnlyList<TextNumber> sourceList, CancellationToken cancellation)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            var text = "?";
            number = null;
            sourceList = source.ToReadOnlyList();
            if (sourceList.Count == 0)
                throw new ArgumentException("At least one source item is required", nameof(source));
            if (!cancellation.IsCancellationRequested) {
                text = GetText(sourceList);
                if (!cancellation.IsCancellationRequested)
                    number = GetNumber(sourceList, cancellation);
            }
            return text;
        }

        private static string GetText(IReadOnlyList<TextNumber> source) => string.Concat(
            source.Select((source, index) =>
            {
                if (source is null)
                    throw new ArgumentNullException($"{nameof(source)}[{index}]");
                return source.Text;
            }));

        private static Number? GetNumber(IReadOnlyList<TextNumber> source, CancellationToken cancellation) => source.
            Select(source => source.Number).
            SumByDigits(cancellation);

        public static implicit operator TextNumber((string text, ushort number) item) => new(item.text, item.number);

        public TextNumber Join(IEnumerable<TextNumber> source, CancellationToken cancellation = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            var result = new TextNumber(
                HasSource ?
                    Source.Concat(source) :
                    this.ToEnumerable().Concat(source),
                cancellation);
            return result;
        }

        #endregion

        /// <summary>
        /// Non-empty text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// <see cref="Text"/> number if known, otherwise null
        /// </summary>
        public Number? Number { get; }
        
        [MemberNotNullWhen(true, nameof(Number))]
        public bool HasNumber => Number != null;

        #region Source

        /// <summary>
        /// Optional source text numbers
        /// </summary>
        public IReadOnlyList<TextNumber>? Source { get; }

        [MemberNotNullWhen(true, nameof(Source))]
        public bool HasSource => Source?.Count > 0;

        /// <summary>
        /// Gets all sources from <see cref="Source"/> tree having false <see cref="HasSource"/> which are usually letters from <see cref="Alphabet.Read"/>.
        /// If this object has <see cref="HasSource"/> false, it is returned.
        /// </summary>
        /// <returns>Base sources</returns>
        public IEnumerable<TextNumber> Letters => HasSource ?
            Source.SelectMany(i => i.Letters) :
            this.ToEnumerable();

        #endregion

        /// <summary>
        /// Number of text levels this object represents
        /// </summary>
        /// <value>1 + (maximum <see cref="LevelCount"/> from <see cref="Source"/> if any, otherwise 0)</value>
        public byte LevelCount => (byte)(1 + (Source?.Max(i => i.LevelCount) ?? 0));

        #region ToString
        
        public string TextWithNumber => HasSource ?
            ToStringLocal(Source.Select(i => i.ToStringLocal(i.Text, false)).JoinLines()) :
            ToStringLocal();

        public override string ToString() => TextWithNumber;

        private string ToStringLocal() => ToStringLocal(Text);
        private string ToStringLocal(string text, bool keepSingleLineSeparatorEnd = true)
        {
            string result;
            if (HasNumber) {
                var hasSingleLineWithSeparatorEnd = text.HasSingleLineWithSeparatorEnd();
                if (hasSingleLineWithSeparatorEnd)
                    text = text.Replace(TextLines.Separator, null);
                var separator = (text.HasMultipleLines() ? TextLines.Separator : " ") + "= ";
                result = $"{text}{separator}{Number}";
                if (hasSingleLineWithSeparatorEnd && keepSingleLineSeparatorEnd)
                    result += TextLines.Separator;
            }
            else
                result = Text;
            return result;
        }

        public string? GetText(
            Func<string?, string?>? formatKnown = null,
            Func<string?, string?>? formatUnknown = null,
            CancellationToken cancellation = default)
        {
            string? text = null;
            if (formatKnown is null &&
                formatUnknown is null) {
                text = Text;
            }
            else {
                foreach (var item in Letters.JoinByHasNumber(cancellation)) {
                    if (cancellation.IsCancellationRequested)
                        break;
                    text += item.HasNumber ?
                        Format(item.Text, formatKnown) :
                        Format(item.Text, formatUnknown);
                }
            }
            return text;
        }

        internal static string? Format(string text, Func<string?, string?>? format) => format is null ?
            text :
            format(text);

        #endregion
    }
}