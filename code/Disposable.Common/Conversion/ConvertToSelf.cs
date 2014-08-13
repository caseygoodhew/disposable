using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable.Common.Conversion
{
    internal class ConvertToSelf<T> : IConvert<T, T> where T : class
    {
        public T Convert(T fromType)
        {
            return fromType;
        }
    }
}
