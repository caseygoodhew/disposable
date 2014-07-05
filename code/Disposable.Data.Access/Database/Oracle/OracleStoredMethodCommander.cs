using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Disposable.Common;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;

namespace Disposable.Data.Access.Database.Oracle
{
    /// <summary>
    /// Oracle stored method commander.
    /// </summary>
    internal class OracleStoredMethodCommander : IStoredMethodCommander
    {
        private static readonly Lazy<IDataObjectConverter> DataConverter = new Lazy<IDataObjectConverter>(
            () => Locator.Current.Instance<IDataObjectConverter>());

        /// <summary>
        /// Executes a call to the given stored method over the given connection.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="connection">The database connection.</param>
        /// <param name="storedMethod">The stored method to call.</param>
        /// <returns>An object of the required type.</returns>
        public T Execute<T>(IDbConnection connection, IStoredMethod storedMethod)
        {
            var values = Execute(connection, storedMethod);
            return DataConverter.Value.ConvertTo<T>(values);
        }

        /// <summary>
        /// Executes a call to the given method over the given connection.
        /// </summary>
        /// <typeparam name="TOut1">The first type to return.</typeparam>
        /// <typeparam name="TOut2">The second type to return.</typeparam>
        /// <param name="connection">The database connection.</param>
        /// <param name="storedMethod">The stored method to call.</param>
        /// <param name="out1">The first return object.</param>
        /// <param name="out2">The second return object.</param>
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

            var commandText = string.Format(
                "{0}.{1}.{2}", 
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
    }
}
