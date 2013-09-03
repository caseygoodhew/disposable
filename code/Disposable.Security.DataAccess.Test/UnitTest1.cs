using System;
using System.Diagnostics;
using System.IO;
using Disposable.Security.DataAccess.Entities;
using Disposable.Security.DataAccess.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;


namespace Disposable.Security.DataAccess.Test
{
	[TestClass]
	public class UnitTest1
	{

		private ISessionFactory _sessionFactory;
		
		[TestInitialize]
		public void TestMethod0()
		{
			_sessionFactory = CreateSessionFactory();

		}

		[TestMethod]
		public void TestMethod1()
		{
			// create a couple of Stores each with some Products and Employees
			var barginBasin = new Store { Name = "Bargin Basin" };
			var superMart = new Store { Name = "SuperMart" };

			var potatoes = new Product { Name = "Potatoes", Price = 3.60 };
			var fish = new Product { Name = "Fish", Price = 4.49 };
			var milk = new Product { Name = "Milk", Price = 0.79 };
			var bread = new Product { Name = "Bread", Price = 1.29 };
			var cheese = new Product { Name = "Cheese", Price = 2.10 };
			var waffles = new Product { Name = "Waffles", Price = 2.41 };

			var daisy = new Employee { FirstName = "Daisy", LastName = "Harrison" };
			var jack = new Employee { FirstName = "Jack", LastName = "Torrance" };
			var sue = new Employee { FirstName = "Sue", LastName = "Walkters" };
			var bill = new Employee { FirstName = "Bill", LastName = "Taft" };
			var joan = new Employee { FirstName = "Joan", LastName = "Pope" };

			// add products to the stores, there's some crossover in the products in each
			// store, because the store-product relationship is many-to-many
			AddProductsToStore(barginBasin, potatoes, fish, milk, bread, cheese);
			AddProductsToStore(superMart, bread, cheese, waffles);

			// add employees to the stores, this relationship is a one-to-many, so one
			// employee can only work at one store at a time
			AddEmployeesToStore(barginBasin, daisy, jack, sue);
			AddEmployeesToStore(superMart, bill, joan);
			
			var watch = Stopwatch.StartNew();
			long _1openSessionET;
			long _2beginTransactionET;
			long _3saveDataET;
			long _4commitET;
			long _5endTransactionET;
			long _6closeSessionET;

			using (var session = _sessionFactory.OpenSession())
			{
				_1openSessionET = watch.ElapsedMilliseconds;
				using (var transaction = session.BeginTransaction())
				{
					_2beginTransactionET = watch.ElapsedMilliseconds;
					
					// save both stores, this saves everything else via cascading
					session.SaveOrUpdate(barginBasin);
					session.SaveOrUpdate(superMart);
					_3saveDataET = watch.ElapsedMilliseconds;
					
					transaction.Commit();
					_4commitET = watch.ElapsedMilliseconds;
				}
				_5endTransactionET = watch.ElapsedMilliseconds;
			}
			_6closeSessionET = watch.ElapsedMilliseconds;
			int y = 0;
		}

		public static void AddProductsToStore(Store store, params Product[] products)
		{
			foreach (var product in products)
			{
				store.AddProduct(product);
			}
		}

		public static void AddEmployeesToStore(Store store, params Employee[] employees)
		{
			foreach (var employee in employees)
			{
				store.AddEmployee(employee);
			}
		}

		private static ISessionFactory CreateSessionFactory()
		{
			/*return Fluently.Configure()
				.Database(
				  SQLiteConfiguration.Standard
					.UsingFile("firstProject.db")
				)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Product>())
				.BuildSessionFactory();*/

			// TODO: http://stackoverflow.com/questions/6559682/connection-string-for-fluent-nhibernate-with-mysql

			return Fluently.Configure()
				.Database(
					MySQLConfiguration.Standard.ConnectionString("Server=localhost;Database=disposable;Uid=root;Pwd=manager;")
				)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMap>())
				//.ExposeConfiguration(BuildSchema)
				.BuildSessionFactory();
		}

		private static void BuildSchema(Configuration config)
        {
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config)
                .Create(false, true);
        }
	}
}
