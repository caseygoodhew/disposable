using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text.RegularExpressions;

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

        private static readonly Regex StoredMethodFormat = new Regex("^[a-z][a-z_0-9]*.[a-z][a-z_0-9]*.[a-z][a-z_0-9]*$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Executes a call to the given stored method over the given connection.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="connection">The database connection.</param>
        /// <param name="storedMethod">The stored method instaqnce to call.</param>
        /// <returns>An object of the required type.</returns>
        public T Execute<T>(IDbConnection connection, IStoredMethodInstance storedMethod)
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
        /// <param name="storedMethod">The stored method instance to call.</param>
        /// <param name="out1">The first return object.</param>
        /// <param name="out2">The second return object.</param>
        public void Execute<TOut1, TOut2>(IDbConnection connection, IStoredMethodInstance storedMethod, out TOut1 out1, out TOut2 out2)
        {
            var values = Execute(connection, storedMethod).ToList();

            out1 = (TOut1)values[0];
            out2 = (TOut2)values[1];
        }

        private static IEnumerable<object> Execute(IDbConnection connection, IStoredMethodInstance storedMethodInstance)
        {
            var command = CreateCommand(connection, storedMethodInstance);
            
            var outputParameters = ApplyParameters(command, storedMethodInstance).ToList();

            try
            {
                command.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                OracleExceptionAdapter.Throw(oe, storedMethodInstance); 
            }
            
            return outputParameters.Select(p => OracleDataTypeMapper.Map(p.AsOutputParameter(), p.OracleParameter.Value));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Security", 
            "CA2100:Review SQL queries for security vulnerabilities", 
            Justification = "Only stored methods can called in this block and the command text is validated to ensure that only stored method signatures are present.")]
        private static OracleCommand CreateCommand(IDbConnection connection, IStoredMethodInstance storedMethodInstance)
        {
            Guard.ArgumentIsType<OracleDbConnection>(connection, "connection");

            var commandText = string.Format(
                "{0}.{1}.{2}", 
                storedMethodInstance.Package.Schema, 
                storedMethodInstance.Package.Name, 
                storedMethodInstance.Name);

            if (!StoredMethodFormat.IsMatch(commandText))
            {
                throw new SecurityException(string.Format(@"""{0}"" failed match. Possible security breach.", commandText));
            }
            
            var command = new OracleCommand(commandText, (connection as OracleDbConnection).Connection)
            {
                InitialLONGFetchSize = 1000,
                CommandType = CommandType.StoredProcedure
            };

            return command;
        }

        private static IEnumerable<OracleOutputParameter> ApplyParameters(OracleCommand command, IStoredMethodInstance storedMethod)
        {
            ApplyInputParameters(command, storedMethod);
            return ApplyOutputParameters(command, storedMethod);
        }

        private static void ApplyInputParameters(OracleCommand command, IStoredMethodInstance storedMethod)
        {
            foreach (var parameterValue in storedMethod.GetValues<IInputParameterValue>(true))
            {
                var parameter = parameterValue.AsInputParameter();
                command.Parameters.Add(parameter.Name, OracleDataTypeMapper.Map(parameter), OracleDataTypeMapper.Map(parameter, parameterValue.Value), ParameterDirection.Input);
            }
        }

        private static IEnumerable<OracleOutputParameter> ApplyOutputParameters(OracleCommand command, IStoredMethodInstance storedMethod)
        {
            var oracleOutputParameter = new List<OracleOutputParameter>();

            foreach (var parameter in storedMethod.GetValues<IOutputParameterValue>(true).Select(x => x.AsOutputParameter()))
            {
                var oracleParameter = command.Parameters.Add(parameter.Name, OracleDataTypeMapper.Map(parameter), parameter.Direction);
                oracleOutputParameter.Add(new OracleOutputParameter(oracleParameter, parameter));
            }

            return oracleOutputParameter;
        }
    }
}
