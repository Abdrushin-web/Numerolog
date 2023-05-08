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
        /// <param name="cancellation">Optional cancellation</param>
        /// <returns>Computed result if <paramref name="source"/> is neither null nor empty, otherwise null</returns>
        public static TextNumber? Compute(this IEnumerable<TextNumber>? source, CancellationToken cancellation = default)
        {
            var sourceList = source?.ToReadOnlyList();
            return sourceList?.Count > 0 ?
                sourceList.Count == 1 ?
                    sourceList[0] :
                    new TextNumber(sourceList, cancellation) :
                null;
        }

        /// <summary>
        /// Joins <paramref name="source"/> items by <see cref="HasNumber"/> to distinguish known and unknown text parts
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="cancellation">Optional cancellation</param>
        /// <returns>Joined items</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null</exception>
        public static IEnumerable<TextNumber> JoinByHasNumber(this IEnumerable<TextNumber> source, CancellationToken cancellation = default)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));
            TextNumber? previous = null;
            foreach (var item in source) {
                if (cancellation.IsCancellationRequested)
                    yield break;
                if (previous is null)
                    previous = item;
                else if (previous.HasNumber == item.HasNumber)
                    previous = previous.Join(item.ToEnumerable(), cancellation);
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
