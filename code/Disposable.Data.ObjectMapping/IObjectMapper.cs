using System.Collections.Generic;
using System.Data;

namespace Disposable.Data.ObjectMapping
{
    public interface IObjectMapper
    {
        T GetOne<T>(DataSet dataSet) where T : new();

        T GetOne<T>(IDataReader dataReader) where T : new();

        IEnumerable<T> GetMany<T>(DataSet dataSet) where T : new();

        IEnumerable<T> GetMany<T>(IDataReader dataReader) where T : new();
    }
}
