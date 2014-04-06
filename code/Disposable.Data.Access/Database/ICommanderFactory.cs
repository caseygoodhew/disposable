using Disposable.Data.Packages.Core;

namespace Disposable.Data.Access.Database
{
    internal interface ICommanderFactory
    {
        IStoredMethodCommander GetStoredMethodCommander();
    }
}
