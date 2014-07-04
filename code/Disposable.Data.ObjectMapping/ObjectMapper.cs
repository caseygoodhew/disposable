using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Disposable.Data.ObjectMapping
{
    public class ObjectMapper : IObjectMapper
    {
        public T GetOne<T>(DataSet dataSet) where T : new()
        {
            throw new System.NotImplementedException();
        }

        public T GetOne<T>(IDataReader dataReader) where T : new()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetMany<T>(DataSet dataSet) where T : new()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetMany<T>(IDataReader dataReader) where T : new()
        {
            throw new System.NotImplementedException();
        }
    }
}
