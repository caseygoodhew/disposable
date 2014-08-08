using Disposable.Common;
using Disposable.Common.ServiceLocator;
using Disposable.Initialization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Disposable.Validation.Factory;

using FluentValidation.Mvc;

namespace Disposable.Web
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();

            DisposableCore.Initialize();
            
		    var locator = Locator.Current as Locator;

            FluentValidationModelValidatorProvider.Configure(x => x.ValidatorFactory = new ValidatorFactory());
            locator.Register<IApplication>(() => new Application("DISPOSABLE APP NAME", "DISPOSABLE APP DESCRIPTION"));
		}
	}
}