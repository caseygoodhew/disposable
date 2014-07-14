using System;
using System.Collections.Generic;
using System.Linq;

using Disposable.Common.Extensions;

namespace Disposable.Text
{
    public class Phrase : Component
    {
        private readonly IList<Word> wordList;

        public Phrase(string phrase) : this(phrase, CaseStyles.Auto) { }

        public Phrase(string phrase, CaseStyles style) : this(phrase, Dummy(phrase, style)) { }

        private Phrase(string phrase, IList<Word> wordList) : base(
            phrase,
            () => wordList.Select(x => x.Upper()).Aggregate(StringAggregate),
            () => wordList.Select(x => x.Lower()).Aggregate(StringAggregate),
            () => wordList.Select(x => x.Proper()).Aggregate(StringAggregate))
        {
            if (wordList.IsNullOrEmpty())
            {
                throw new ArgumentNullException("phrase");
            }
            
            this.wordList = wordList;
        }

        private static IList<Word> Dummy(string phrase, CaseStyles style)
        {
            throw new NotImplementedException();
        }

        private static string StringAggregate(string current, string next)
        {
            return current + next;
        }
    }
}
