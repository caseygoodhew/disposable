
using Disposable.Data.Packages;

namespace Disposable.Data.Database
{
    /// <summary>
    /// Interface for implementations which can command stored methods.
    /// </summary>
    public interface IStoredMethodCommander
    {
        /// <summary>
        /// Executes a call to the given stored method over the given connection.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="connection">The database connection.</param>
        /// <param name="storedMethod">The stored method instance to call.</param>
        /// <returns>An object of the required type.</returns>
        T Execute<T>(IDbConnection connection, IStoredMethodInstance storedMethod);

        /// <summary>
        /// Executes a call to the given method over the given connection.
        /// </summary>
        /// <typeparam name="TOut1">The first type to return.</typeparam>
        /// <typeparam name="TOut2">The second type to return.</typeparam>
        /// <param name="connection">The database connection.</param>
        /// <param name="storedMethod">The stored method instance to call.</param>
        /// <param name="out1">The first return object.</param>
        /// <param name="out2">The second return object.</param>
        void Execute<TOut1, TOut2>(IDbConnection connection, IStoredMethodInstance storedMethod, out TOut1 out1, out TOut2 out2);
    }
}
