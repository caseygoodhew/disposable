using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    public interface IUserPackage : IPackage
    {
        IStoredProcedure AuthenticateUserProcedure(string username, string password);

        IStoredFunction AuthenticateUserFunction(string username, string password);
    }
}
