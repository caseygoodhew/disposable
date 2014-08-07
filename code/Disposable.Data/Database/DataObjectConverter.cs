using Disposable.Common.Extensions;
using Disposable.Data.Map;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Disposable.Data.Database
{
    /// <summary>
    /// Abstract implementation for converting database data structures to well known data types.
    /// </summary>
    public abstract class DataObjectConverter : IDataObjectConverter
    {
        /// <summary>
        /// Converts an object set to a well known data type.
        /// Valid types are any value type, DataSet, IDataReader or IEnumerable{IDataReader}.
        /// These types expect that the <see cref="values"/> parameter will contain an appropriate number of elements to satisfy the data type being returned.
        /// Any other type will be passed to the registered data source mapper which will perform its own validation as needed.
        /// </summary>
        /// <typeparam name="T">The type to map to.</typeparam>
        /// <param name="values">The values to convert.</param>
        /// <returns>The converted value(s).</returns>
        public T ConvertTo<T>(IEnumerable<object> values)
        {
            var typeT = typeof(T);

            if (typeT == typeof(DataSet))
            {
                return (T)(object)ToDataSet(values);
            }

            if (typeT == typeof(IDataReader))
            {
                return (T)ToIDataReader(values);
            }

            if (typeT == typeof(IEnumerable<IDataReader>))
            {
                return (T)ToIDataReaders(values);
            }

            if (typeof(IEnumerable<IDataReader>).IsAssignableFrom(typeT))
            {
                throw new ArgumentException("IDataReader collections can only be returned as IEnumerable");
            }

            if (typeT.IsClass)
            {
                return InvokeObjectMapper<T>(values);
            }

            return (T)values.Single();
        }

        /// <summary>
        /// Converts an enumeration of objects to a DataSet.
        /// </summary>
        /// <param name="values">The values to be converted.</param>
        /// <returns>A DataSet of the values.</returns>
        protected abstract DataSet ToDataSet(IEnumerable<object> values);

        /// <summary>
        /// Converts an enumeration of objects to an IDataReader.
        /// </summary>
        /// <param name="values">The values to be converted.</param>
        /// <returns>An IDataReader of the values.</returns>
        protected abstract IDataReader ToIDataReader(IEnumerable<object> values);

        /// <summary>
        /// Converts an enumeration of objects to an enumeration of IDataReaders.
        /// </summary>
        /// <param name="values">The values to be converted.</param>
        /// <returns>An enumeration of IDataReaders.</returns>
        protected abstract IEnumerable<IDataReader> ToIDataReaders(IEnumerable<object> values);

        private T InvokeObjectMapper<T>(IEnumerable<object> values)
        {
            var typeT = typeof(T);
            var typeToBind = typeT;
            var isEnumerable = false;

            if (typeT.IsIEnumerable(true))
            {
                isEnumerable = true;
                typeToBind = typeT.GetGenericArguments()[0];
            }

            if (!typeToBind.HasDefaultConstructor())
            {
                // TODO: allow a delegate constructor to be passed in.
                throw new InvalidOperationException(string.Format("{0} does not contain a public default constructor.", typeT.Name));
            }

            var method = typeof(Mapper)
                               .GetMethod(isEnumerable ? "GetMany" : "GetOne", new[] { typeof(IDataReader) })
                               .MakeGenericMethod(typeToBind);

            return (T)method.Invoke(null, new object[] { ToIDataReader(values) });
        }
    }
}
