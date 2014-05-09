using System.Runtime.InteropServices.ComTypes;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    public class UserPackage : Package, IUserPackage
    {
        public UserPackage()
        {
            Register(() => new AuthenticateUserProcedure(this));
            Register(() => new CreateUserProcedure(this));
            Register(() => new GetUserProcedure(this));
        }
        
        public override string Schema
        {
            get { return PackageConstants.Disposable; } 
        }

        public override string Name
        {
            get { return PackageConstants.UserPkg; } 
        }

        public IStoredProcedure AuthenticateUserProcedure(string email, string password)
        {
            var procedure = Instance<AuthenticateUserProcedure>();
            procedure.SetParameterValues(email, password);
            return procedure;
        }

        public IStoredProcedure CreateUserProcedure(string email, string password, bool isApproved)
        {
            var procedure = Instance<CreateUserProcedure>();
            procedure.SetParameterValues(email, password, isApproved);
            return procedure;
        }

        public IStoredProcedure GetUserProcedure(string username)
        {
            var procedure = Instance<GetUserProcedure>();
            procedure.SetParameterValues(username);
            return procedure;
        }
    }
}
