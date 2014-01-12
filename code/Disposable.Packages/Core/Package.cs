using System;
using System.Collections.Generic;

using Disposable.Common.ServiceLocator;

namespace Disposable.Packages.Core
{
    internal abstract class Package : BaseRegistrar, IPackage
    {
        public abstract string Schema { get; }

        public abstract string Name { get; }

        protected T GetProcedure<T>() where T : StoredProcedureDefinition, new()
        {
            if (!IsRegistered<T>())
            {
                var t = new T();
                t.SetPackage(this);
                Register(() => t);
            }

            return Instance<T>();
        }
    }
}
