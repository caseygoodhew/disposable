using System;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access
{
    public interface IDbHelper : IDisposable
    {
        TResult ReturnValue<TResult, TInput>(Func<TInput, IStoredMethod> spGenerator) where TInput : class;
    }
}
