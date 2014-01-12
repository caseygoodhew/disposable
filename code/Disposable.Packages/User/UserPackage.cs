using System.Runtime.InteropServices.ComTypes;
using Disposable.Packages.Core;

namespace Disposable.Packages.User
{
    internal class UserPackage : Package, IUserPackage
    {
        private static readonly string PackageName = "user_pkg";

        public override string Schema
        {
            get { return PackageConstants.Disposable; } 
        }

        public override string Name
        {
            get { return PackageName; } 
        }

        public AuthenticateUserProcedure AuthenticateUser(string username, string password)
        {
            var procedure = GetProcedure<AuthenticateUserProcedure>();
            procedure.SetParameters(username, password);
            return procedure;
        }

        
        
    }
}
