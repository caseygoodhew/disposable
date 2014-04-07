﻿using Disposable.Data.Packages.Core;

namespace Disposable.Data.Packages.User
{
    public interface IUserPackage : IPackage
    {
        IStoredProcedure AuthenticateUserProcedure(string email, string password);
    }
}
