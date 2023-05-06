namespace Numerology
{
    public static class Enumerables
    {
        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));
            var result = items is IReadOnlyList<T> list ?
                list :
                items.ToArray();
            return result;
        }

        public static IEnumerable<T> ToEnumerable<T>(this T item) => Enumerable.Repeat(item, 1);

        public static IEnumerable<T> NonNull<T>(this IEnumerable<T?> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items));
            return items.
                Where(i => i != null).
                Cast<T>();
        }
    }
}
