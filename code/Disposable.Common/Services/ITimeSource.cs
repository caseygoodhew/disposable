using System;

namespace Disposable.Common.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITimeSource
    {
        /// <summary>
        /// 
        /// </summary>
        DateTime Now { get; }
    }
}
