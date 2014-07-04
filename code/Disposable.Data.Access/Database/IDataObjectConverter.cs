using System.Collections.Generic;

namespace Disposable.Data.Access.Database
{
    public interface IDataObjectConverter
    {
        T ConvertTo<T>(IEnumerable<object> values);
    }
}
