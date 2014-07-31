using System;
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
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1"/> to check for emptiness.</param>
        /// <returns>
        /// true if the source sequence is null or empty; otherwise, false.
        /// </returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Concatenates a sequence to a string using the default ToString method on each element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1"/> to concatenate.</param>
        /// <param name="separator">A string that will be used to separate the parts.</param>
        /// <returns>The concatenated string.</returns>
        public static string Concat<TSource>(this IEnumerable<TSource> source, string separator = "")
        {
            return Concat(source, x => x.ToString(), separator);
        }
        
        /// <summary>
        /// Concatenates a sequence to a string using a custom ToString method on each element.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1"/> to concatenate.</param>
        /// <param name="toString">The delegate to use to convert the <see cref="source"/> elements to strings.</param>
        /// <param name="separator">A string that will be used to separate the parts.</param>
        /// <returns>The concatenated string.</returns>
        public static string Concat<TSource>(this IEnumerable<TSource> source, Func<TSource, string> toString, string separator = "")
        {
            return source == null ? string.Empty : string.Join(separator, source.Select(toString));
        }
    }
}
