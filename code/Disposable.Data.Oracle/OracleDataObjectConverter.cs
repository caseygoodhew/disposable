using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Disposable.Data.Database;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Disposable.Data.Oracle
{
    /// <summary>
    /// Converting oracle database data structures to well known data types.
    /// </summary>
    internal class OracleDataObjectConverter : DataObjectConverter
    {
        /// <summary>
        /// Converts an enumeration of objects to a DataSet.
        /// </summary>
        /// <param name="values">The values to be converted.</param>
        /// <returns>A DataSet of the values.</returns>
        protected override DataSet ToDataSet(IEnumerable<object> values)
        {
            var adapter = new OracleDataAdapter();
            var ds = new DataSet();

            ToRefCursors(values).ForEach(value => adapter.Fill(ds.Tables.Add(), value));

            return ds;
        }

        /// <summary>
        /// Converts an enumeration of objects to an IDataReader.
        /// </summary>
        /// <param name="values">The values to be converted.</param>
        /// <returns>An IDataReader of the values.</returns>
        protected override IDataReader ToIDataReader(IEnumerable<object> values)
        {
            var value = values.Single();
            ValidateIsRefCursor(value);
            return (value as OracleRefCursor).GetDataReader();
        }

        /// <summary>
        /// Converts an enumeration of objects to an enumeration of IDataReaders.
        /// </summary>
        /// <param name="values">The values to be converted.</param>
        /// <returns>An enumeration of IDataReaders.</returns>
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
