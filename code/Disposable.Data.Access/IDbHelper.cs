using System;
using System.Data;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access
{
    public interface IDbHelper : IDisposable
    {
        TResult ReturnValue<TResult, TInput>(Func<TInput, IStoredMethod> spGenerator) where TInput : class;

        void Run<TInput, TOut1>(Func<TInput, IStoredMethod> spGenerator, out TOut1 out1) where TInput : class;

        void Run<TInput, TOut1, TOut2>(Func<TInput, IStoredMethod> spGenerator, out TOut1 out1, out TOut2 out2) where TInput : class;
    }
}
