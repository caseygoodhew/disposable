using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disposable.Common.ServiceLocator;
using Disposable.Common.Services;

namespace Disposable.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class DisposableCore
    {
        private static readonly Lazy<DisposableCore> DisposableCoreInstance = new Lazy<DisposableCore>(() => new DisposableCore());

        private DisposableCore()
        {
            var locator = (Locator.Current as Locator);
            
            if (locator == null)
            {
                throw new InvalidOperationException();
            }

            var registrar = locator.BaseRegistrar;

            registrar.Register<ITimeSource>(() => new LocalTimeSource());
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool Initialize()
        {
            return DisposableCoreInstance.Value != null;
        }
    }
}
