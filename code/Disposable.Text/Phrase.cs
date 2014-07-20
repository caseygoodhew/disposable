using System;
using System.Collections.Generic;

namespace Disposable.Text
{
    /// <summary>
    /// <see cref="TextComponent"/> representing a word or group of words.
    /// </summary>
    public class Phrase : TextComponent
    {
        private readonly IList<Word> wordList;

        private readonly Dictionary<JoinStyle, Lazy<TextComponent>> asLazy = new Dictionary<JoinStyle, Lazy<TextComponent>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Phrase"/> class.
        /// </summary>
        /// <param name="phrase">The underlying phrase.</param>
        /// <param name="style">The case style of the underlying phrase used for parsing. Defaults to Auto which is the least efficient parsing method.</param>
        public Phrase(string phrase, SplitStyle style = SplitStyle.Auto) : this(phrase, CaseConverter.Split(phrase, style))
        {
        }

        private Phrase(string phrase, IList<Word> wordList) : base(phrase)
        {
            this.wordList = wordList;

            asLazy[JoinStyle.LowerCamelCase] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToLowerCamelCase(this.wordList)));
            asLazy[JoinStyle.UpperCamelCase] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToUpperCamelCase(this.wordList)));
            
            asLazy[JoinStyle.SpaceDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToSpaceDelimited(this.wordList)));
            asLazy[JoinStyle.LowerSpaceDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToSpaceDelimited(this.wordList, CaseStyle.Lower)));
            asLazy[JoinStyle.UpperSpaceDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToSpaceDelimited(this.wordList, CaseStyle.Upper)));
            asLazy[JoinStyle.ProperSpaceDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToSpaceDelimited(this.wordList, CaseStyle.Proper)));

            asLazy[JoinStyle.DashDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToDashDelimited(this.wordList)));
            asLazy[JoinStyle.LowerDashDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToDashDelimited(this.wordList, CaseStyle.Lower)));
            asLazy[JoinStyle.UpperDashDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToDashDelimited(this.wordList, CaseStyle.Upper)));
            asLazy[JoinStyle.ProperDashDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToDashDelimited(this.wordList, CaseStyle.Proper)));

            asLazy[JoinStyle.UnderscoreDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToUnderscoreDelimited(this.wordList)));
            asLazy[JoinStyle.LowerUnderscoreDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToUnderscoreDelimited(this.wordList, CaseStyle.Lower)));
            asLazy[JoinStyle.UpperUnderscoreDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToUnderscoreDelimited(this.wordList, CaseStyle.Upper)));
            asLazy[JoinStyle.ProperUnderscoreDelimited] = new Lazy<TextComponent>(() => new Word(CaseConverter.ToUnderscoreDelimited(this.wordList, CaseStyle.Proper)));
        }

        /// <summary>
        /// Gets the phase as a <see cref="TextComponent"/> in the requested case style of the value using lazy loading.
        /// </summary>
        /// <param name="style">The style to convert the phase to.</param>
        /// <returns>A <see cref="TextComponent"/> in the requested case style of the value using lazy loading.</returns>
        public TextComponent As(JoinStyle style)
        {
            return asLazy[style].Value;
        }
    }
}
