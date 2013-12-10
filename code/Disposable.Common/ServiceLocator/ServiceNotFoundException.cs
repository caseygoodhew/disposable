using System;
using System.Diagnostics.CodeAnalysis;

namespace Disposable.Common.ServiceLocator
{
    /// <summary>
    /// Indicates that a service could not be found.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ServiceNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceNotFoundException"/> class.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> that could not be found</param>
        public ServiceNotFoundException(Type type) : base(string.Format("There is no service registered for type {0}", type.FullName))
        {
        }
    }
}
