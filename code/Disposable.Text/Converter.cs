using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common.Extensions;

namespace Disposable.Text
{
    public static class Converter
    {
        private static readonly List<CaseStyles> CaseStyles = Enum.GetValues(typeof(CaseStyles)).Cast<CaseStyles>().ToList();
        
        private static readonly List<CaseStyles> SplitAutoExcludes =  new List<CaseStyles> { Text.CaseStyles.Auto, Text.CaseStyles.UpperCamelCase };

        private static readonly List<CaseStyles> SplitAutoIncludes = CaseStyles.Where(x => !SplitAutoExcludes.Contains(x)).ToList();

        public static IList<Word> Split(string value, CaseStyles style)
        {
            return Split(new Word(value), style);
        }
        
        public static IList<Word> Split(Word word, CaseStyles style)
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

        public static IList<Word> Split(IList<Word> words, CaseStyles style)
        {
            return words.SelectMany(x => Split(x, style)).ToList();
        }

        public static string ToLowerCamelCase(IList<Word> words)
        {
            return words.First().Lower() + words.Where((x, i) => i > 0).Concat(x => x.Proper());
        }

        public static string ToUpperCamelCase(IList<Word> words)
        {
            return words.Concat(x => x.Proper());
        }

        public static string ToSpaceDelimited(IList<Word> words)
        {
            return words.Concat(x => x.Value(), " ");
        }

        public static string ToDashDelimited(IList<Word> words)
        {
            return words.Concat(x => x.Value(), "-");
        }

        public static string ToUnderscoreDelimited(IList<Word> words)
        {
            return words.Concat(x => x.Value(), "_");
        }

        private static IList<Word> SplitLowerCamelCase(Word word)
        {
            // if there's no difference between upper or lower then this isn't a camel case
            if (word.Value() == word.Upper() || word.Value() == word.Lower())
            {
                return new List<Word> { word };
            }

            var result = new List<Word>();
            var lastCapAt = 0;

            for (var i = 1; i < word.Value().Length; i++)
            {
                if (!Char.IsUpper(word[i]))
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
            return word.Value().Split(c).Where(x => x.Any()).Select(x => new Word(x)).ToList();
        }
    }
}
