﻿using System.Runtime.InteropServices.ComTypes;
using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    public class UserPackage : Package, IUserPackage
    {
        public UserPackage()
        {
            Register(() => new AuthenticateUserProcedure(this));
        }
        
        public override string Schema
        {
            get { return PackageConstants.Disposable; } 
        }

        public override string Name
        {
            get { return PackageConstants.UserPkg; } 
        }

        public IStoredProcedure AuthenticateUserProcedure(string username, string password)
        {
            var procedure = Instance<AuthenticateUserProcedure>();
            procedure.SetParameterValues(username, password);
            return procedure;
        }
    }
}
