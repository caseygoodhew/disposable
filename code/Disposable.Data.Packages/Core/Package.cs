using System;

using Disposable.Common.ServiceLocator;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract implementation of a database package.
    /// </summary>
    public abstract class Package : IPackage
    {
        private BaseRegistrar registrar = new BaseRegistrar();
        
        protected Package(string schema, string name)
        {
            Schema = schema;
            Name = name;
        }
        
        /// <summary>
        /// Gets the package schema name.
        /// </summary>
        public string Schema { get; private set; }

        /// <summary>
        /// Gets the package name.
        /// </summary>
        public string Name { get; private set;  }

        /// <summary>
        /// Adds a new <see cref="IStoredMethod"/> to the procedure.
        /// </summary>
        /// <typeparam name="TStoredMethod">The type of <see cref="IStoredMethod"/>.</typeparam>
        /// <param name="locatorFunc">The <see cref="IStoredMethod"/> locator function.</param>
        protected void Add<TStoredMethod>(Func<TStoredMethod> locatorFunc) where TStoredMethod : class, IStoredMethod
        {
            registrar.Register(locatorFunc);
        }

        /// <summary>
        /// Gets a <see cref="IStoredMethod"/> from the procedure.
        /// </summary>
        /// <typeparam name="TStoredMethod">The type of <see cref="IStoredMethod"/>.</typeparam>
        /// <returns>The underlying <see cref="IStoredMethod"/>.</returns>
        protected TStoredMethod Get<TStoredMethod>() where TStoredMethod : class, IStoredMethod
        {
            return registrar.Instance<TStoredMethod>();
        }

    }
}
