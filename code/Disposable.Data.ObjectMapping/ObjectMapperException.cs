using System;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// <see cref="IObjectMapper"/> exception.
    /// </summary>
    [Serializable]
    public sealed class ObjectMapperException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectMapperException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ObjectMapperException(string message) : base(message)
        {
        }
    }
}
