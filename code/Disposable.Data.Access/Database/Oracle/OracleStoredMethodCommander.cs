using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Disposable.Common;
using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Disposable.Data.Access.Database.Oracle
{
    internal class OracleStoredMethodCommander : IStoredMethodCommander
    {
        public T Execute<T>(IDbConnection connection, IStoredMethod storedMethod)
        {
            var command = CreateCommand(connection, storedMethod);

            // in order to call execute with a single return type, there must be exactly one output parameter
            var outputParameter = ApplyParameters(command, storedMethod).Single();

            command.ExecuteNonQuery();

            var value = Map(outputParameter, outputParameter.OracleParameter.Value);

            return (T)value;
        }

        private static OracleCommand CreateCommand(IDbConnection connection, IStoredMethod storedMethod)
        {
            Guard.ArgumentIsType<OracleDbConnection>(connection, "connection");

            var commandText = string.Format("{0}.{1}.{2}", 
                                            storedMethod.Package.Schema, 
                                            storedMethod.Package.Name, 
                                            storedMethod.Name);
            
            var command = new OracleCommand(commandText, (connection as OracleDbConnection).Connection)
            {
                InitialLONGFetchSize = 1000,
                CommandType = CommandType.StoredProcedure
            };

            return command;
        }

        private static IEnumerable<OracleOutputParameter> ApplyParameters(OracleCommand command, IStoredMethod storedMethod)
        {
            if (storedMethod is IStoredProcedure)
            {
                ApplyInputParameters(command, storedMethod);
                return ApplyOutputParameters(command, storedMethod);
            }
            
            if (storedMethod is IStoredFunction)
            {
                var result = ApplyOutputParameter(command, storedMethod);
                ApplyInputParameters(command, storedMethod);
                return new List<OracleOutputParameter> { result };
            }

            throw new InvalidOperationException(string.Format("Unknown IStoredMethod type {0}", storedMethod.GetType().FullName));
        }
        
        private static void ApplyInputParameters(OracleCommand command, IStoredMethod storedMethod)
        {
            foreach (var parameter in storedMethod.GetInputParameters(true))
            {
                command.Parameters.Add(parameter.Name, Map(parameter), Map(parameter, parameter.Value), ParameterDirection.Input);
            }
        }

        private static OracleOutputParameter ApplyOutputParameter(OracleCommand command, IStoredMethod storedMethod)
        {
            var outputParameters = ApplyOutputParameters(command, storedMethod);

            if (outputParameters.Count == 0)
            {
                throw new InvalidOperationException(string.Format("{0} does not define any output parameters. Expected one.", storedMethod.GetType().FullName));
            }

            if (outputParameters.Count > 1)
            {
                throw new InvalidOperationException(string.Format("{0} defines {1} output parameters. Expected one.", storedMethod.GetType().FullName, outputParameters.Count));
            }

            return outputParameters.First();
        }

        private static IList<OracleOutputParameter> ApplyOutputParameters(OracleCommand command, IStoredMethod storedMethod)
        {
            var oracleOutputParameter = new List<OracleOutputParameter>();

            if (storedMethod is IStoredProcedure)
            {
                foreach (var parameter in (storedMethod as IStoredProcedure).GetOutputParameters())
                {
                    var oracleParameter = command.Parameters.Add(parameter.Name, Map(parameter), ParameterDirection.Output);
                    oracleOutputParameter.Add(new OracleOutputParameter(oracleParameter, parameter));
                }
            }
            else if (storedMethod is IStoredFunction)
            {
                var parameter = (storedMethod as IStoredFunction).GetOutputParameter();
                var oracleParameter = command.Parameters.Add(parameter.Name, Map(parameter), ParameterDirection.ReturnValue);
                oracleOutputParameter.Add(new OracleOutputParameter(oracleParameter, parameter));
            }
            else
            {
                throw new InvalidOperationException(string.Format("Unknown IStoredMethod type {0}", storedMethod.GetType().FullName));
            }

            return oracleOutputParameter;
        }

        // TODO: create this as a mapper
        // http://stackoverflow.com/questions/1334574/c-sharp-datatypes-oracle-datatypes
        private static OracleDbType Map(IParameter parameter)
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

        private static object Map(IInputParameter inputParameter, object value)
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

        private static object Map(IOutputParameter outputParameter, object value)
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
                    throw new InvalidOperationException("Cannot map an object value to a cursor");
                case DataTypes.Decimal:
                    return Convert.ToDecimal(value);
                case DataTypes.Guid:
                    return new Guid((byte[])value);
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

        private static object MapToValueType(object value)
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
