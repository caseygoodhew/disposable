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
        /// <param name="upperFactory">
        /// A factory function that will provide an upper case version of the <see cref="value"/>. 
        /// This may simply be a call to value.ToUpper, or an efficient call to a cached copy of the upper case value.
        /// </param>
        /// <param name="lowerFactory">
        /// A factory function that will provide a lower case version of the <see cref="value"/>.
        /// This may simply be a call to value.ToLower, or an efficient call to a cached copy of the lower case value.
        /// </param>
        protected TextComponent(string value, Func<string> upperFactory, Func<string> lowerFactory)
        {
            Value = value;
            Length = value.Length;
            upperLazy = new Lazy<string>(upperFactory);
            lowerLazy = new Lazy<string>(lowerFactory);
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
