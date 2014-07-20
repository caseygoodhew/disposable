using Disposable.Common.ServiceLocator;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Responsible for registering all services provided by this project.
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// Registers all services provided by this project.
        /// </summary>
        /// <param name="registrar">The registrar.</param>
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<IObjectMapper>(() => new ObjectMapper());
        }
    }
}
