namespace ChatGPTDiffApp.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Yield a reversed list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        /// <remarks>Source: https://stackoverflow.com/a/69920378/6329629</remarks>
        public static IEnumerable<T> LazyReverse<T>(this IList<T> items)
        {
            for (var i = items.Count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }
    }
}
