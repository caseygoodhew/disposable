using System;
using System.Collections.Generic;

using Disposable.Common.Extensions;

namespace Disposable.Text
{
    /// <summary>
    /// <see cref="TextComponent"/> representing a word or group of words.
    /// </summary>
    public class Phrase : TextComponent
    {
        private readonly IList<Word> wordList;

        private readonly Dictionary<CaseStyles, Lazy<TextComponent>> asLazy = new Dictionary<CaseStyles, Lazy<TextComponent>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Phrase"/> class.
        /// </summary>
        /// <param name="phrase">The underlying phrase.</param>
        /// <param name="style">The case style of the underlying phrase used for parsing. Defaults to Auto which is the least efficient parsing method.</param>
        public Phrase(string phrase, CaseStyles style = CaseStyles.Auto) : this(phrase, CaseConverter.Split(phrase, style))
        {
            if (wordList.IsNullOrEmpty())
            {
                throw new ArgumentNullException("phrase");
            }
        }

        private Phrase(string phrase, IList<Word> wordList) : base(
            phrase,
            () => wordList.Concat(x => x.Upper),
            () => wordList.Concat(x => x.Lower))
        {
            this.wordList = wordList;

            asLazy[CaseStyles.LowerCamelCase] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToLowerCamelCase(this.wordList)));
            asLazy[CaseStyles.UpperCamelCase] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToUpperCamelCase(this.wordList)));
            asLazy[CaseStyles.SpaceDelimiter] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToSpaceDelimited(this.wordList)));
            asLazy[CaseStyles.DashDelimiter] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToDashDelimited(this.wordList)));
            asLazy[CaseStyles.UnderscoreDelimiter] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToUnderscoreDelimited(this.wordList)));
        }

        /// <summary>
        /// Gets the phase as a <see cref="TextComponent"/> in the requested case style of the value using lazy loading.
        /// </summary>
        /// <param name="style">The style to convert the phase to.</param>
        /// <returns>A <see cref="TextComponent"/> in the requested case style of the value using lazy loading.</returns>
        public TextComponent As(CaseStyles style)
        {
            if (style == CaseStyles.Auto)
            {
                throw new InvalidOperationException("Cannot generate Auto CaseStyle as output");
            }

            return asLazy[style].Value;
        }
    }
}
