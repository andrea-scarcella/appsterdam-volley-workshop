using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NHibernate.ByteCode.Castle;
using NHibernate.Dialect;
using NHibernate;
using NHibernate.Mapping.ByCode;
using WallBackend.Core;
using NHibernate.Cfg.MappingSchema;

namespace WallBackend.Dal
{
	public class DalConfiguration
	{
		public static Configuration Configure(string coreAssemblyFQN, Configuration cfg)
		{
			var nhConfig = cfg.Proxy(proxy =>
				proxy.ProxyFactoryFactory<ProxyFactoryFactory>())
				.DataBaseIntegration(db =>
				{
					db.Dialect<MsSql2008Dialect>();
					db.ConnectionStringName = "db";
					db.BatchSize = 100;

				}
				)
				.AddAssembly(coreAssemblyFQN);//"WallBackend.Core"
			//var sessionFactory = nhConfig.BuildSessionFactory();

			return nhConfig;
			//Console.WriteLine("NHibernate Configured!");
		}
		public static ISessionFactory GetSessionFactory(Configuration Cfg)
		{
			//Source : http://devio.wordpress.com/2012/07/05/getting-started-with-sqlite-and-loquacious-nhibernate-3-2/
			var mapper = new ModelMapper();
			//typeof(Account).Assembly
			mapper.AddMappings(typeof(UserMap).Assembly.GetExportedTypes());
			HbmMapping domainMapping =
			  mapper.CompileMappingForAllExplicitlyAddedEntities();

			//var f = Path.Combine(
			//  System.Environment.GetFolderPath(
			//	System.Environment.SpecialFolder.LocalApplicationData),
			//  "mydatabase.db");
			//var cn =
			//  (new System.Data.SQLite.SQLiteConnectionStringBuilder { DataSource = f })
			//	.ConnectionString;

			//var configuration = new NHibernate.Cfg.Configuration();
			//configuration.DataBaseIntegration(c =>
			//{
			//	c.Driver<NHibernate.Driver.SQLite20Driver>();
			//	c.Dialect<SQLiteDialect>();
			//	c.ConnectionString = cn;
			//	c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
			//	c.SchemaAction = SchemaAutoAction.Update;

			//	c.LogFormattedSql = true;
			//	c.LogSqlInConsole = true;
			//});
			Cfg.AddMapping(domainMapping);
			
			var sessionFactory = Cfg.BuildSessionFactory();
			return sessionFactory;
		}
	}
}
