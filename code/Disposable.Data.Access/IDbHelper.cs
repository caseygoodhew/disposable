using System;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access
{
    /// <summary>
    /// Interface for implementations which orchestrate connections to the database.
    /// </summary>
    public interface IDbHelper : IDisposable
    {
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
        /// <param name="spGenerator">A function which can provide a <see cref="IStoredMethodInstance"/> reference type.</param>
        /// <returns>An object of the type requested.</returns>
        TResult ReturnValue<TResult, TInput>(Func<TInput, IStoredMethodInstance> spGenerator) where TInput : class;

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
        /// <param name="spGenerator">A function which can provide a <see cref="IStoredMethodInstance"/> reference type.</param>
        /// <param name="out1">An object of the type requested.</param>
        void Run<TInput, TOut1>(Func<TInput, IStoredMethodInstance> spGenerator, out TOut1 out1) where TInput : class;

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
        /// <param name="spGenerator">A function which can provide a <see cref="IStoredMethodInstance"/> reference type.</param>
        /// <param name="out1">The first object of the type requested.</param>
        /// <param name="out2">The second object of the type requested.</param>
        void Run<TInput, TOut1, TOut2>(Func<TInput, IStoredMethodInstance> spGenerator, out TOut1 out1, out TOut2 out2) where TInput : class;
    }
}
