using System.Collections.Generic;

namespace Disposable.Data.ObjectMapping
{
    internal interface IObjectBinding<T> : IEnumerable<IMemberMapper<T>> where T : class
    {
    }
}
