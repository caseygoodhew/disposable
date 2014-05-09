using System;
using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Disposable.Data.Access.Database.Oracle
{
    // TODO: Create this as a mapper entity
    internal static class OracleDataTypeMapper
    {
        // http://stackoverflow.com/questions/1334574/c-sharp-datatypes-oracle-datatypes
        internal static OracleDbType Map(IParameter parameter)
        {
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
