namespace Numerology
{
    /// <summary>
    /// <see cref="TextNumber"/> helper
    /// </summary>
    public static class TextNumbers
    {
        /// <summary>
        /// Computes result from <paramref name="source"/>
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>Computed result if <paramref name="source"/> is neither null nor empty, otherwise null</returns>
        public static TextNumber? Compute(this IEnumerable<TextNumber>? source)
        {
            var sourceList = source?.ToReadOnlyList();
            return sourceList?.Count > 0 ?
                sourceList.Count == 1 ?
                    sourceList[0] :
                    new TextNumber(sourceList) :
                null;
        }

        /// <summary>
        /// Joins <paramref name="source"/> items by <see cref="HasNumber"/> to distinguish known and unknown text parts
        /// </summary>
        /// <param name="source">Source</param>
        /// <returns>Joined items</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        public static IEnumerable<TextNumber> JoinByHasNumber(this IEnumerable<TextNumber> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            TextNumber? previous = null;
            foreach (var item in source) {
                if (previous is null)
                    previous = item;
                else if (previous.HasNumber == item.HasNumber)
                    previous = previous.Join(item.ToEnumerable());
                else {
                    yield return previous;
                    previous = item;
                }
            }
            if (previous != null)
                yield return previous;
        }
    }
}
