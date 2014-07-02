using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Disposable.Common;
using Disposable.Data.ObjectMapping;
using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace Disposable.Data.Access.Database.Oracle
{
    internal class OracleStoredMethodCommander : IStoredMethodCommander
    {
        public T Execute<T>(IDbConnection connection, IStoredMethod storedMethod)
        {
            return ConvertTo<T>(Execute(connection, storedMethod));
        }

        public void Execute<TOut1, TOut2>(IDbConnection connection, IStoredMethod storedMethod, out TOut1 out1, out TOut2 out2)
        {
            var values = Execute(connection, storedMethod).ToList();

            out1 = (TOut1)values[0];
            out2 = (TOut2)values[1];
        }

        private static IEnumerable<object> Execute(IDbConnection connection, IStoredMethod storedMethod)
        {
            var command = CreateCommand(connection, storedMethod);
            
            var outputParameters = ApplyParameters(command, storedMethod).ToList();

            try
            {
                command.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                OracleExceptionAdapter.Throw(oe, storedMethod); 
            }
            

            return outputParameters.Select(p => OracleDataTypeMapper.Map(p, p.OracleParameter.Value));
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
                command.Parameters.Add(parameter.Name, OracleDataTypeMapper.Map(parameter), OracleDataTypeMapper.Map(parameter, parameter.Value), ParameterDirection.Input);
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
                    var oracleParameter = command.Parameters.Add(parameter.Name, OracleDataTypeMapper.Map(parameter), ParameterDirection.Output);
                    oracleOutputParameter.Add(new OracleOutputParameter(oracleParameter, parameter));
                }
            }
            else if (storedMethod is IStoredFunction)
            {
                var parameter = (storedMethod as IStoredFunction).GetOutputParameter();
                var oracleParameter = command.Parameters.Add(parameter.Name, OracleDataTypeMapper.Map(parameter), ParameterDirection.ReturnValue);
                oracleOutputParameter.Add(new OracleOutputParameter(oracleParameter, parameter));
            }
            else
            {
                throw new InvalidOperationException(string.Format("Unknown IStoredMethod type {0}", storedMethod.GetType().FullName));
            }

            return oracleOutputParameter;
        }

        private static T ConvertTo<T>(IEnumerable<object> values)
        {
            var typeT = typeof(T);

            if (typeT == typeof(DataSet))
            {
                return (T)(object)(ToDataSet(values));
            }

            

            if (typeT == typeof(IDataReader))
            {
                return (T)ToIDataReader(values.Single());
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
                /*var enumerableType = typeT.GetInterfaces().First(x => x.IsGenericType
                                                               && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                                                               && x.GetGenericArguments().Count() == 1
                                                             ).GetGenericArguments()[0];

                var isEnumerable = enumerableType != null;
                typeT = isEnumerable ? enumerableType : typeT;

                
                var d1 = typeof(IObjectMapper<>);
                Type[] typeArgs = { typeof(T) };
                var makeme = d1.MakeGenericType(typeArgs);
                var o = Activator.CreateInstance(makeme) as IObjectMapper<T>;
                o as IObjectMapper<T>*/

                throw new NotImplementedException();
                
            }

            return (T)(values.Single());
        }
        
        private static DataSet ToDataSet(IEnumerable<object> values)
        {
            var adapter = new OracleDataAdapter();
            var ds = new DataSet();

            ToRefCursors(values).ForEach(value => adapter.Fill(ds.Tables.Add(), value));
            
            return ds;
        }

        private static IDataReader ToIDataReader(object value)
        {
            ValidateIsRefCursor(value);
            return (value as OracleRefCursor).GetDataReader();
        }

        private static IEnumerable<IDataReader> ToIDataReaders(IEnumerable<object> values)
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
