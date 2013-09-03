using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable.Security.DataAccess.Entities
{
	public class Product
	{
		public virtual int Id { get; protected set; }
		public virtual string Name { get; set; }
		public virtual double Price { get; set; }
		public virtual IList<Store> StoresStockedIn { get; protected set; }

		public Product()
		{
			StoresStockedIn = new List<Store>();
		}
	}
}
