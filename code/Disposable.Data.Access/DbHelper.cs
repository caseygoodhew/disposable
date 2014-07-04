using System;
using System.Collections.Generic;
using System.Data;
using Disposable.Common.ServiceLocator;
using Disposable.Data.Access.Database;
using Disposable.Data.Access.Database.Oracle;
using Disposable.Data.Packages.Core;
using IDbConnection = Disposable.Data.Access.Database.IDbConnection;

namespace Disposable.Data.Access
{
    /// <summary>
    /// Orchestrates connections to the database.
    /// </summary>
    public class DbHelper : IDbHelper
    {
        private static readonly Lazy<IConnectionProvider> ConnectionProvider = new Lazy<IConnectionProvider>(
            () => Locator.Current.Instance<IConnectionProvider>());

        private static readonly Lazy<ICommanderFactory> StoredProcedureCreator = new Lazy<ICommanderFactory>(
            () => Locator.Current.Instance<ICommanderFactory>());
        
        private IDbConnection connection;

        private IDbConnection Connection
        {
            get
            {
                if (connection != null && connection.State == ConnectionState.Closed)
                {
                    connection.Dispose();
                    connection = null;
                }
                
                if (connection == null)
                {
                    connection = ConnectionProvider.Value.CreateConnection(ConnectionSource.Web, null);
                    connection.Open();
                }

                return connection;
            }
        }

        /// <summary>
        /// Executes a calls the provided stored method and returns a value in the type requested. 
        /// Typical valid types are:
        /// <code>
        /// Any ValueType
        /// DataSet
        /// IDataReader
        /// IEnumerable{IDataReader}
        /// ConcreteClass
        /// IEnumerable{ConcreteClass}
        /// </code>
        /// </summary>
        /// <typeparam name="TResult">The required return type.</typeparam>
        /// <typeparam name="TInput">Typically reference type which implements <see cref="IPackage"/> but could be any reference type that can generate a <see cref="IStoredMethod"/> reference type.</typeparam>
        /// <param name="spGenerator">A function which can provide a <see cref="IStoredMethod"/> reference type.</param>
        /// <returns>An object of the type requested.</returns>
        public TResult ReturnValue<TResult, TInput>(Func<TInput, IStoredMethod> spGenerator) where TInput : class
        {
            var storedMethod = spGenerator.Invoke(Locator.Current.Instance<TInput>());

            var commander = StoredProcedureCreator.Value.GetStoredMethodCommander();

            return commander.Execute<TResult>(Connection, storedMethod);
        }

        /// <summary>
        /// Executes a calls the provided stored method and returns a value in the type requested. 
        /// Typical valid types are:
        /// <code>
        /// Any ValueType
        /// DataSet
        /// IDataReader
        /// IEnumerable{IDataReader}
        /// ConcreteClass
        /// IEnumerable{ConcreteClass}
        /// </code>
        /// </summary>
        /// <typeparam name="TInput">Typically reference type which implements <see cref="IPackage"/> but could be any reference type that can generate a <see cref="IStoredMethod"/> reference type.</typeparam>
        /// <typeparam name="TOut1">The required out type.</typeparam>
        /// <param name="spGenerator">A function which can provide a <see cref="IStoredMethod"/> reference type.</param>
        /// <param name="out1">An object of the type requested.</param>
        public void Run<TInput, TOut1>(Func<TInput, IStoredMethod> spGenerator, out TOut1 out1) where TInput : class
        {
            var storedMethod = spGenerator.Invoke(Locator.Current.Instance<TInput>());

            var commander = StoredProcedureCreator.Value.GetStoredMethodCommander();

            out1 = commander.Execute<TOut1>(Connection, storedMethod);
        }

        /// <summary>
        /// Executes a calls the provided stored method and returns a value in the type requested. 
        /// Typical valid types are:
        /// <code>
        /// Any ValueType
        /// DataSet
        /// IDataReader
        /// IEnumerable{IDataReader}
        /// ConcreteClass
        /// IEnumerable{ConcreteClass}
        /// </code>
        /// </summary>
        /// <typeparam name="TInput">Typically reference type which implements <see cref="IPackage"/> but could be any reference type that can generate a <see cref="IStoredMethod"/> reference type.</typeparam>
        /// <typeparam name="TOut1">The first required out type.</typeparam>
        /// <typeparam name="TOut2">The second required out type.</typeparam>
        /// <param name="spGenerator">A function which can provide a <see cref="IStoredMethod"/> reference type.</param>
        /// <param name="out1">The first object of the type requested.</param>
        /// <param name="out2">The second object of the type requested.</param>
        public void Run<TInput, TOut1, TOut2>(Func<TInput, IStoredMethod> spGenerator, out TOut1 out1, out TOut2 out2) where TInput : class
        {
            var storedMethod = spGenerator.Invoke(Locator.Current.Instance<TInput>());

            var commander = StoredProcedureCreator.Value.GetStoredMethodCommander();

            commander.Execute(Connection, storedMethod, out out1, out out2);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (connection != null)
            {
                if (connection.State != ConnectionState.Closed)
                {
                    // see http://dev.mysql.com/doc/refman/5.5/en/server-system-variables.html#sysvar_interactive_timeout
                    connection.Close();
                }

                connection.Dispose();
            }
        }
    }
}
