using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common.Extensions;

namespace Disposable.Text
{
    /// <summary>
    /// Case conversion 
    /// </summary>
    public static class CaseConverter
    {
        private static readonly List<CaseStyles> CaseStyles = Enum.GetValues(typeof(CaseStyles)).Cast<CaseStyles>().ToList();
        
        private static readonly List<CaseStyles> SplitAutoExcludes = new List<CaseStyles> { Text.CaseStyles.Auto, Text.CaseStyles.UpperCamelCase };

        private static readonly List<CaseStyles> SplitAutoIncludes = CaseStyles.Where(x => !SplitAutoExcludes.Contains(x)).ToList();

        /// <summary>
        /// Splits a <see cref="String"/> into a list of <see cref="Word"/>s using the specified style. 
        /// </summary>
        /// <param name="value">The word to convert.</param>
        /// <param name="style">The case style to use when splitting the <see cref="value"/>. Defaults to <see cref="Text.CaseStyles.Auto"/> which is the least efficient parsing method.</param>
        /// <returns>A list of <see cref="Word"/>s split using the specified style.</returns>
        public static IList<Word> Split(string value, CaseStyles style = Text.CaseStyles.Auto)
        {
            return Split(new Word(value), style);
        }

        /// <summary>
        /// Splits a <see cref="Word"/> into a list of <see cref="Word"/>s using the specified style. 
        /// </summary>
        /// <param name="word">The value to convert.</param>
        /// <param name="style">The case style to use when splitting the <see cref="word"/>. Defaults to <see cref="Text.CaseStyles.Auto"/> which is the least efficient parsing method.</param>
        /// <returns>A list of <see cref="Word"/>s split using the specified style.</returns>
        public static IList<Word> Split(Word word, CaseStyles style = Text.CaseStyles.Auto)
        {
            switch (style)
            {
                case Text.CaseStyles.Auto:
                    return SplitAutoIncludes.Aggregate(new List<Word> { word } as IList<Word>, Split);
                    
                case Text.CaseStyles.LowerCamelCase:
                    return SplitLowerCamelCase(word);

                case Text.CaseStyles.UpperCamelCase:
                    return SplitUpperCamelCase(word);

                case Text.CaseStyles.SpaceDelimiter:
                    return SplitSpaceDelimited(word);

                case Text.CaseStyles.DashDelimiter:
                    return SplitDashDelimited(word);

                case Text.CaseStyles.UnderscoreDelimiter:
                    return SplitUnderscoreDelimited(word);

                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }

        /// <summary>
        /// Splits a list of <see cref="Word"/>s into a flattened list of <see cref="Word"/>s using the specified style. 
        /// </summary>
        /// <param name="words">The words to convert.</param>
        /// <param name="style">The case style to use when splitting the <see cref="words"/>. Defaults to <see cref="Text.CaseStyles.Auto"/> which is the least efficient parsing method.</param>
        /// <returns>A flattened list of <see cref="Word"/>s split using the specified style.</returns>
        public static IList<Word> Split(IList<Word> words, CaseStyles style = Text.CaseStyles.Auto)
        {
            return words.SelectMany(x => Split(x, style)).ToList();
        }

        /// <summary>
        /// Converts a list of <see cref="Word"/>s to a lowerCamelCase string.
        /// </summary>
        /// <param name="words">The words to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ToLowerCamelCase(IList<Word> words)
        {
            return words.First().Lower + words.Where((x, i) => i > 0).Concat(x => x.Proper);
        }

        /// <summary>
        /// Converts a list of <see cref="Word"/>s to an UpperCamelCase string.
        /// </summary>
        /// <param name="words">The words to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ToUpperCamelCase(IList<Word> words)
        {
            return words.Concat(x => x.Proper);
        }

        /// <summary>
        /// Converts a list of <see cref="Word"/>s to a Space Delimited string.
        /// </summary>
        /// <param name="words">The words to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ToSpaceDelimited(IList<Word> words)
        {
            return words.Concat(x => x.Value, " ");
        }

        /// <summary>
        /// Converts a list of <see cref="Word"/>s to a Dash-Delimited string.
        /// </summary>
        /// <param name="words">The words to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ToDashDelimited(IList<Word> words)
        {
            return words.Concat(x => x.Value, "-");
        }

        /// <summary>
        /// Converts a list of <see cref="Word"/>s to an Underscore_Delimited string.
        /// </summary>
        /// <param name="words">The words to convert.</param>
        /// <returns>The converted string.</returns>
        public static string ToUnderscoreDelimited(IList<Word> words)
        {
            return words.Concat(x => x.Value, "_");
        }

        private static IList<Word> SplitLowerCamelCase(Word word)
        {
            // if there's no difference between upper or lower then this isn't a camel case
            if (word.Value == word.Upper || word.Value == word.Lower)
            {
                return new List<Word> { word };
            }

            var result = new List<Word>();
            var lastCapAt = 0;

            for (var i = 1; i < word.Length; i++)
            {
                if (!char.IsUpper(word[i]))
                {
                    continue;
                }

                result.Add(word.Sub(lastCapAt, i - lastCapAt));
                lastCapAt = i;
            }

            result.Add(word.Sub(lastCapAt));

            return result;
        }

        private static IList<Word> SplitUpperCamelCase(Word word)
        {
            return SplitLowerCamelCase(word);
        }

        private static IList<Word> SplitSpaceDelimited(Word word)
        {
            return SplitCharacterDelimited(word, ' ');
        }

        private static IList<Word> SplitDashDelimited(Word word)
        {
            return SplitCharacterDelimited(word, '-');
        }

        private static IList<Word> SplitUnderscoreDelimited(Word word)
        {
            return SplitCharacterDelimited(word, '_');
        }

        private static IList<Word> SplitCharacterDelimited(Word word, char c)
        {
            return word.Value.Split(c).Where(x => x.Any()).Select(x => new Word(x)).ToList();
        }
    }
}
