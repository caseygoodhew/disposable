using System;

namespace Disposable.Common.Services
{
    public class LocalTimeSource : ITimeSource
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
