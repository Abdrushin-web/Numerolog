using System.Diagnostics.CodeAnalysis;

namespace Numerology
{
    /// <summary>
    /// Text with number
    /// </summary>
    public class TextNumber
    {
        public TextNumber(string text, Number? number)
        {
            if (string.IsNullOrEmpty(text)) {
                throw new ArgumentException($"'{nameof(text)}' cannot be null or empty.", nameof(text));
            }
            Text = text;
            Number = number;
        }

        public TextNumber(IEnumerable<TextNumber> source) :
            this(GetText(source, out var number, out var sourceList), number)
            => Source = sourceList;

        private static string GetText(IEnumerable<TextNumber> source, out Number? number, out IReadOnlyList<TextNumber> sourceList)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            sourceList = source.ToReadOnlyList();
            if (sourceList.Count == 0)
                throw new ArgumentException("At least one source item is required", nameof(source));
            var text = string.Concat(sourceList.Select((source, index) =>
            {
                if (source is null)
                    throw new ArgumentNullException($"{nameof(source)}[{index}]");
                return source.Text;
            }));
            number = sourceList.Select(source => source.Number).SumByDigits();
            return text;
        }

        public static implicit operator TextNumber((string text, ushort number) item) => new(item.text, item.number);

        /// <summary>
        /// Non-empty text
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// <see cref="Text"/> number if known, otherwise null
        /// </summary>
        public Number? Number { get; }
        public bool HasNumber => Number != null;

        /// <summary>
        /// Optional source text numbers
        /// </summary>
        public IReadOnlyList<TextNumber>? Source { get; }
        [MemberNotNullWhen(true, nameof(Source))]
        public bool HasSource => Source?.Count > 0;

        public string TextWithNumber => HasSource ?
            ToStringLocal(Source.Select(i => i.ToStringLocal(i.Text, false)).JoinLines()) :
            ToStringLocal();

        public override string ToString() => TextWithNumber;

        /// <summary>
        /// Gets all sources from <see cref="Source"/> tree having false <see cref="HasSource"/> which are usually letters from <see cref="Alphabet.Read"/>.
        /// If this object has <see cref="HasSource"/> false, it is returned.
        /// </summary>
        /// <returns>Base sources</returns>
        public IEnumerable<TextNumber> Letters => HasSource ?
            Source.SelectMany(i => i.Letters) :
            this.ToEnumerable();

        /// <summary>
        /// Number of text levels this object represents
        /// </summary>
        /// <value>1 + (maximum <see cref="LevelCount"/> from <see cref="Source"/> if any, otherwise 0)</value>
        public byte LevelCount => (byte)(1 + (Source?.Max(i => i.LevelCount) ?? 0));

        public TextNumber Join(IEnumerable<TextNumber> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            var result = new TextNumber(HasSource ?
                Source.Concat(source) :
                this.ToEnumerable().Concat(source));
            return result;
        }

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
    }
}