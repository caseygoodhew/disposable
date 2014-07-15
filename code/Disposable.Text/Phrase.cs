using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

using Disposable.Common.Extensions;

namespace Disposable.Text
{
    public class Phrase : Component
    {
        private readonly IList<Word> wordList;

        public Phrase(string phrase) : this(phrase, CaseStyles.Auto) { }

        public Phrase(string phrase, CaseStyles style) : this(phrase, Converter.Split(phrase, style))
        {
            if (wordList.IsNullOrEmpty())
            {
                throw new ArgumentNullException("phrase");
            }
        }

        private Phrase(string phrase, IList<Word> wordList) : base(
            phrase,
            () => wordList.Concat(x => x.Upper()),
            () => wordList.Concat(x => x.Lower()),
            () => wordList.Concat(x => x.Proper()))
        {
            this.wordList = wordList;
        }

        public string As(CaseStyles style)
        {
            switch (style)
            {
                case CaseStyles.Auto:
                    throw new InvalidOperationException("Cannot generate Auto CaseStyle as output");

                case CaseStyles.LowerCamelCase:
                    return Converter.ToLowerCamelCase(wordList);

                case CaseStyles.UpperCamelCase:
                    return Converter.ToUpperCamelCase(wordList);

                case CaseStyles.SpaceDelimiter:
                    return Converter.ToSpaceDelimited(wordList);

                case CaseStyles.DashDelimiter:
                    return Converter.ToDashDelimited(wordList);

                case CaseStyles.UnderscoreDelimiter:
                    return Converter.ToUnderscoreDelimited(wordList);

                default:
                    throw new ArgumentOutOfRangeException("style");
            }
        }
    }
}
