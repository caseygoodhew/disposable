using System.Collections.Generic;
using System.Linq;

namespace Disposable.Common.Extensions
{
    /// <summary>
    /// IEnumerable extensions
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether a sequence is null or an Empty.
        /// </summary>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1"/> to check for emptiness.</param>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <returns>
        /// true if the source sequence is null or empty; otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }
    }
}
