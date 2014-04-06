using System;

namespace Disposable.Common.Services
{
    /// <summary>
    /// Time source interface to ensure that time can be controlled for production and unit testing
    /// </summary>
    public interface ITimeSource
    {
        /// <summary>
        /// Gets the time now
        /// </summary>
        DateTime Now { get; }
    }
}
