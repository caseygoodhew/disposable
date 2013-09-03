using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disposable.Security.DataAccess.Entities;
using FluentNHibernate.Mapping;


namespace Disposable.Security.DataAccess.Mappings
{
	public class EmployeeMap : ClassMap<Employee>
	{
		public EmployeeMap()
		{
			Id(x => x.Id);
			Map(x => x.FirstName);
			Map(x => x.LastName);
			References(x => x.Store);
		}
	}
}
