using System;
using System.Diagnostics.CodeAnalysis;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Indicates that a service has already been registered
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class ServiceAlreadyRegisteredException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAlreadyRegisteredException"/> class.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that is being registered</param>
        public ServiceAlreadyRegisteredException(Type type) : base(string.Format("A service for type {0} has already been registered", type.FullName))
        {
        }
    }
}
