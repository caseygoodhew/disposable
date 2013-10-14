using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Disposable.Security.DataAccess
{
	public class SomeClass
	{
		public string Description;
		
		public string TestDirect()
		{
			var db = new MySqlConnection("Server=localhost;Database=disposable;Uid=root;Pwd=manager;");
			db.Open();

			var cmd = new MySqlCommand("Select * from disposable.class_type where class_type_id = 1", db);
			IDataReader rdr = cmd.ExecuteReader();
			
			rdr.Read();
			string value = rdr[rdr.GetOrdinal("description")].ToString();
			
			db.Close();
			
			return value;
		}

		public string TestDapper()
		{
			var db = new MySqlConnection("Server=localhost;Database=disposable;Uid=root;Pwd=manager;");
			db.Open();

			return
				db.Query<SomeClass>("select * from disposable.class_type where class_type_id = @ClassTypeId", new {ClassTypeId = 1})
					.First()
					.Description;

		}
	}
}
