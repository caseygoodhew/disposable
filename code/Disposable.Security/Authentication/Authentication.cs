using System;

namespace Disposable.Security.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public class Authentication : IAuthentication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="deviceGuid"></param>
        /// <returns></returns>
        public AuthenticationResult Authenticate(string username, string password, Guid deviceGuid)
        {
            return new AuthenticationResult(AuthenticationStatus.Succeeded);
        }
    }
}
