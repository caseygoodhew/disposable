using System;
using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Disposable.Data.Access.Database.Oracle
{
    /// <summary>
    /// Provides oracle data type mapping functionality.
    /// </summary>
    /// <remarks>TODO: Create this as a mapper entity</remarks>
    internal static class OracleDataTypeMapper
    {
        /// <summary>
        /// Maps <see cref="IParameter.DataType"/> to <see cref="OracleDbType"/>.
        /// </summary>
        /// <param name="parameter">The <see cref="IParameter"/> to map.</param>
        /// <returns>The <see cref="OracleDbType"/>.</returns>
        internal static OracleDbType Map(IParameter parameter)
        {
            //// http://stackoverflow.com/questions/1334574/c-sharp-datatypes-oracle-datatypes</remarks>
            switch (parameter.DataType)
            {
                case DataTypes.Boolean:
                    return OracleDbType.Int16;
                case DataTypes.Byte:
                    return OracleDbType.Byte;
                case DataTypes.Cursor:
                    return OracleDbType.RefCursor;
                case DataTypes.Decimal:
                    return OracleDbType.Decimal;
                case DataTypes.Guid:
                    return OracleDbType.Raw;
                case DataTypes.Int:
                    return OracleDbType.Int32;
                case DataTypes.Long:
                    return OracleDbType.Int64;
                case DataTypes.String:
                    return OracleDbType.Varchar2;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Maps <see cref="IInputParameter"/> values to oracle compatible values.
        /// </summary>
        /// <param name="inputParameter">The <see cref="IInputParameter"/> for the corresponding <see cref="value"/>.</param>
        /// <param name="value">The value to map.</param>
        /// <returns>The oracle compatible value.</returns>
        internal static object Map(IInputParameter inputParameter, object value)
        {
            if (value == null)
            {
                return null;
            }

            switch (inputParameter.DataType)
            {
                case DataTypes.Boolean:
                    return Convert.ToInt16(value);
                case DataTypes.Byte:
                    return Convert.ToByte(value);
                case DataTypes.Cursor:
                    throw new InvalidOperationException("Cannot map an object value to a cursor");
                case DataTypes.Decimal:
                    return Convert.ToDecimal(value);
                case DataTypes.Guid:
                    return ((Guid)value).ToByteArray();
                case DataTypes.Int:
                    return Convert.ToInt32(value);
                case DataTypes.Long:
                    return Convert.ToInt64(value);
                case DataTypes.String:
                    return Convert.ToString(value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Maps oracle values to <see cref="IOutputParameter.DataType"/> compatible values.
        /// </summary>
        /// <param name="outputParameter">The <see cref="IOutputParameter.DataType"/> to map to.</param>
        /// <param name="value">The value to map.</param>
        /// <returns>The mapped value.</returns>
        internal static object Map(IOutputParameter outputParameter, object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            value = MapToValueType(value);

            switch (outputParameter.DataType)
            {
                case DataTypes.Boolean:
                    return Convert.ToBoolean(value);
                case DataTypes.Byte:
                    return Convert.ToByte(value);
                case DataTypes.Cursor:
                    return value;
                case DataTypes.Decimal:
                    return Convert.ToDecimal(value);
                case DataTypes.Guid:
                    var binaryValue = (OracleBinary)value;
                    return binaryValue.IsNull ? Guid.Empty : new Guid(binaryValue.Value);
                case DataTypes.Int:
                    return Convert.ToInt32(value);
                case DataTypes.Long:
                    return Convert.ToInt64(value);
                case DataTypes.String:
                    return Convert.ToString(value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Maps oracle specific values to .net value types.
        /// </summary>
        /// <param name="value">The value to map.</param>
        /// <returns>The mapped value.</returns>
        internal static object MapToValueType(object value)
        {
            var valueType = value.GetType();

            if (valueType == typeof(OracleDate))
            {
                return ((OracleDate)value).Value;
            }

            if (valueType == typeof(OracleDecimal))
            {
                return ((OracleDecimal)value).Value;
            }

            if (valueType == typeof(OracleString))
            {
                return ((OracleString)value).Value;
            }

            if (valueType == typeof(OracleTimeStamp))
            {
                return ((OracleTimeStamp)value).Value;
            }

            return value;
        }
    }
}
