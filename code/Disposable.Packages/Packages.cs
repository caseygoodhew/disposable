using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disposable.Common.ServiceLocator;
using Disposable.Packages.User;

namespace Disposable.Packages
{
    public static class Packages
    {
        public static void Register(IRegistrar registrar)
        {
            registrar.Register(() => new UserPackage());
        }
    }
}
