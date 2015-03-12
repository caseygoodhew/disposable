using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Adds a single instance of TSource to the enumerable.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="T:System.Collections.Generic.IEnumerable`1"/> to add the <paramref name="value"/> to.</param>
        /// <param name="value">The value to add to the <paramref name="source"/>.</param>
        /// <returns>The modified source enumerable.</returns>
        public static IEnumerable<TSource> Add<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return source.Concat(new[] { value });
        }

        /// <summary>
        /// Gets a random element from the enumerable.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">>The <see cref="T:System.Collections.Generic.IEnumerable`1"/> to get the random element from</param>
        /// <returns>A random element from the enumerable.</returns>
        public static TSource Random<TSource>(this IEnumerable<TSource> source)
        {
            return source.RandomUsing(new Random());
        }

        /// <summary>
        /// Gets a random element from the enumerable.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">>The <see cref="T:System.Collections.Generic.IEnumerable`1"/> to get the random element from</param>
        /// <param name="rand">The random value generator.</param>
        /// <returns>A random element from the enumerable.</returns>
        public static TSource RandomUsing<TSource>(this IEnumerable<TSource> source, Random rand)
        {
            Guard.ArgumentNotNull(source, "Enumerable");
            
            var enumerable = source as IList<TSource> ?? source.ToList();
            return enumerable.ElementAt(rand.Next(0, enumerable.Count()));
        }
    }
}
