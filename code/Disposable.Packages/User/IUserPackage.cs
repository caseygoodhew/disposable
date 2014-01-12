using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Disposable.Packages.Core;

namespace Disposable.Packages.User
{
    public interface IUserPackage : IPackage
    {
        IStoredProcedure AuthenticateUser(string username, string password);
    }
}
