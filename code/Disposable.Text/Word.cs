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

        /// <summary>
        /// Gets a <see cref="Char"/> object at a given position in the current <see cref="Word"/> object.
        /// </summary>
        /// <param name="index">A position in the current <see cref="Word.Value"/></param>
        /// <returns>The <see cref="Char"/> object at the given position in the current <see cref="Word"/> object.</returns>
        public char this[int index]
        {
            get
            {
                return value[index];
            }
        }

        public Word Sub(int startIndex)
        {
            return new Word(value.Substring(startIndex));
        }

        public Word Sub(int startIndex, int length)
        {
            return new Word(value.Substring(startIndex, length));
        }
    }
}
