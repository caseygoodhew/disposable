using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable.Core
{
	/*public interface IServiceLocator
	{
		T GetService<T>();
	}

	public sealed class ServiceLocator : IServiceLocator
	{
		// map that contains pairs of interfaces and
		// references to concrete implementations
		private IDictionary<object, object> _services;
		private static readonly object _lock = new Object();
		private static IServiceLocator _instance;

		private ServiceLocator()
		{
			_services = new Dictionary<object, object>();
		}

		public static IServiceLocator Current
		{
			get
			{
				if (_instance == null)
				{
					lock (_lock) // thread safety
					{
						if (_instance == null)
						{
							_instance = new ServiceLocator();
						}
					}

					return _instance;
				}
			}
		}
		
		public T GetService<T>()
		{
			try
			{
				return (T)_services[typeof(T)];
			}
			catch (KeyNotFoundException)
			{
				throw new ApplicationException("The requested service is not registered");
			}
		}
	}*/
}
