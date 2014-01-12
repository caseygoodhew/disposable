using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Disposable.Packages.Core;

namespace Disposable.Packages.User
{
    internal interface IUserPackage : IPackage
    {
        AuthenticateUserProcedure AuthenticateUser(string username, string password);
    }
}
