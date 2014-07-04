using System;

namespace Disposable.Security.Authentication
{
    /// <summary>
    /// Interface for services which provide authentication services.
    /// </summary>
    public interface IAuthentication
    {
        /// <summary>
        /// Authenticates a username (email address), password and device.
        /// </summary>
        /// <param name="username">The username (email address) to authenticate.</param>
        /// <param name="password">The password.</param>
        /// <param name="deviceGuid">The Guid of the device.</param>
        /// <returns>The authentication result.</returns>
        AuthenticationStatus Authenticate(string username, string password, Guid deviceGuid);
    }
}
