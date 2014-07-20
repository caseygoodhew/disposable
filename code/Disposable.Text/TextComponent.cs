using System;

namespace Disposable.Text
{
    /// <summary>
    /// Base functionality for efficient text using one time lazy case conversion.
    /// </summary>
    public abstract class TextComponent
    {
        private readonly Lazy<string> upperLazy;

        private readonly Lazy<string> lowerLazy;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextComponent"/> class.
        /// </summary>
        /// <param name="value">The underlying <see cref="Value"/>.</param>
        protected TextComponent(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }
            
            Value = value;
            Length = value.Length;
            upperLazy = new Lazy<string>(Value.ToUpper);
            lowerLazy = new Lazy<string>(Value.ToLower);
        }

        /// <summary>
        /// Gets the underlying value as it was provided on initialization.
        /// </summary>
        public string Value
        {
            get; private set;
        }

        /// <summary>
        /// Gets the length of the <see cref="Value"/>.
        /// </summary>
        public int Length
        {
            get; private set;
        }
        
        /// <summary>
        /// Gets the upper case of the value using lazy loading.
        /// </summary>
        public string Upper
        {
            get
            {
                return upperLazy.Value;
            }
        }

        /// <summary>
        /// Gets the lower case of the value using lazy loading.
        /// </summary>
        public string Lower
        {
            get
            {
                return lowerLazy.Value;   
            }
        }
    }
}
