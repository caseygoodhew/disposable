using System;

namespace Disposable.Text
{
    /// <summary>
    /// <see cref="TextComponent"/> representing a single word.
    /// </summary>
    public class Word : TextComponent
    {
        private readonly Word[][] subWords;
        
        private readonly Lazy<string> properLazy;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Word"/> class.
        /// </summary>
        /// <param name="value">The underlying value.</param>
        public Word(string value) : base(value, value.ToUpper, value.ToLower)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            subWords = new Word[Length][];

            properLazy = new Lazy<string>(() => Sub(0, 1).Upper + Sub(1).Lower);
        }

        /// <summary>
        /// Gets the proper case of the value using lazy loading.
        /// </summary>
        public string Proper
        {
            get
            {
                return properLazy.Value;   
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
                return Value[index];
            }
        }

        /// <summary>
        /// Gets a <see cref="Word"/> that is a substring of the <see cref="TextComponent.Value"/>.
        /// The return value may or may not be a new <see cref="Word"/> class instance.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <returns>A <see cref="Word"/> that is a substring of the <see cref="TextComponent.Value"/>.</returns>
        public Word Sub(int startIndex)
        {
            return GetSub(startIndex, Length);
        }

        /// <summary>
        /// Gets a <see cref="Word"/> that is a substring of the <see cref="TextComponent.Value"/>.
        /// The return value may or may not be a new <see cref="Word"/> class instance.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The number of characters to include.</param>
        /// <returns>A <see cref="Word"/> that is a substring of the <see cref="TextComponent.Value"/>.</returns>
        public Word Sub(int startIndex, int length)
        {
            return GetSub(startIndex, startIndex + length - 1);
        }

        private Word GetSub(int startIndex, int endIndex)
        {
            if (startIndex < 0 || startIndex >= Length)
            {
                throw new IndexOutOfRangeException(string.Format(@"startIndex ({0}: [0=>{1}]) out range", startIndex, Length - 1));
            }

            if (endIndex < 0 || endIndex > Length)
            {
                throw new IndexOutOfRangeException(string.Format(@"endIndex ({0}: [0=>{1}]) out range", endIndex, Length));
            }

            if (subWords[startIndex] == null)
            {
                subWords[startIndex] = new Word[Length + 1];
            }
            
            if (subWords[startIndex][endIndex] == null)
            {
                subWords[startIndex][endIndex] = new Word(Value.Substring(startIndex, endIndex - startIndex));
            }

            return subWords[startIndex][endIndex];
        }
    }
}
