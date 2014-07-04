using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Disposable.Data.Access.Database.Oracle
{
    internal class OracleDataObjectConverter : DataObjectConverter
    {
        protected override DataSet ToDataSet(IEnumerable<object> values)
        {
            var adapter = new OracleDataAdapter();
            var ds = new DataSet();

            ToRefCursors(values).ForEach(value => adapter.Fill(ds.Tables.Add(), value));

            return ds;
        }

        protected override IDataReader ToIDataReader(IEnumerable<object> values)
        {
            var value = values.Single();
            ValidateIsRefCursor(value);
            return (value as OracleRefCursor).GetDataReader();
        }

        protected override IEnumerable<IDataReader> ToIDataReaders(IEnumerable<object> values)
        {
            return ToRefCursors(values).Select(x => x.GetDataReader());
        }

        private static List<OracleRefCursor> ToRefCursors(IEnumerable<object> values)
        {
            var result = new List<OracleRefCursor>();

            values.ToList().ForEach(value =>
            {
                ValidateIsRefCursor(value);
                result.Add(value as OracleRefCursor);
            });

            return result;
        }

        private static void ValidateIsRefCursor(object value)
        {
            if (!(value is OracleRefCursor))
            {
                throw new InvalidCastException(string.Format(@"value is type ""{0}"". Expected type OracleRefCursor", value.GetType()));
            }
        }
    }
}
