﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disposable.Security.DataAccess.Entities
{
	public class Store
	{
		public virtual int Id { get; protected set; }
		public virtual string Name { get; set; }
		public virtual IList<Product> Products { get; set; }
		public virtual IList<Employee> Staff { get; set; }

		public Store()
		{
			Products = new List<Product>();
			Staff = new List<Employee>();
		}

		public virtual void AddProduct(Product product)
		{
			product.StoresStockedIn.Add(this);
			Products.Add(product);
		}

		public virtual void AddEmployee(Employee employee)
		{
			employee.Store = this;
			Staff.Add(employee);
		}
	}
}
