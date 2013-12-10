using System;

namespace Disposable.Security.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="deviceGuid"></param>
        /// <returns></returns>
        AuthenticationResult Authenticate(string username, string password, Guid deviceGuid);
    }
}
