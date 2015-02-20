using System;

namespace Disposable.Common.Services
{
    /// <summary>
    /// <see cref="ITimeSource"/> implementation that uses the local server's clock as a time source
    /// </summary>
    public class LocalTimeSource : ITimeSource
    {
        /// <summary>
        /// Gets the current UTC date and time on the local server
        /// </summary>
        public DateTime Now
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
