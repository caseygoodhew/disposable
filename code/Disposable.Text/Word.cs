using System;

namespace Disposable.Text
{
    public class Word : Component
    {
        public Word(string word) : base(word, word.ToUpper, word.ToLower, () => word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower())
        {
            if (string.IsNullOrEmpty(word))
            {
                throw new ArgumentNullException("word");
            }
        }
    }
}
