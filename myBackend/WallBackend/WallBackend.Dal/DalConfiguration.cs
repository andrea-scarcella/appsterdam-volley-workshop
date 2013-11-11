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
using System.Reflection;
using NHibernate.Tool.hbm2ddl;

namespace WallBackend.Dal
{
	public class DalConfiguration
	{
		protected static Configuration cfg;
		protected static ISessionFactory sessionFactory;
		protected static Type coreType;
		protected static SchemaExport se;
		public DalConfiguration()
		{
			//ugly but functional
			coreType = typeof(UserMap);
		}

		public ISessionFactory getSessionFactory() { return sessionFactory; }
		public SchemaExport getSchemaExport() { return se; }
		public void Configure()
		{
			if (cfg == null)
			{
				cfg = new Configuration();
				var nhConfig = cfg.Proxy(proxy =>
				proxy.ProxyFactoryFactory<ProxyFactoryFactory>())
				.DataBaseIntegration(db =>
				{
					db.Dialect<MsSql2008Dialect>();
					db.ConnectionStringName = "db";
					db.BatchSize = 100;
				}
				);


				var mapper = new ModelMapper();
				mapper.AddMappings(coreType.Assembly.GetExportedTypes());
				HbmMapping domainMapping =
				  mapper.CompileMappingForAllExplicitlyAddedEntities();
				cfg.AddMapping(domainMapping);
				cfg.CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>();

				sessionFactory = cfg.BuildSessionFactory();
				se = new SchemaExport(cfg);
			}




		}

		//public static Configuration Configure(string coreAssemblyFQN, Configuration cfg)
		//{
		//	var nhConfig = cfg.Proxy(proxy =>
		//		proxy.ProxyFactoryFactory<ProxyFactoryFactory>())
		//		.DataBaseIntegration(db =>
		//		{
		//			db.Dialect<MsSql2008Dialect>();
		//			db.ConnectionStringName = "db";
		//			db.BatchSize = 100;

		//		}
		//		)
		//		.AddAssembly(coreAssemblyFQN);//"WallBackend.Core"
		//	//var sessionFactory = nhConfig.BuildSessionFactory();

		//	return nhConfig;
		//	//Console.WriteLine("NHibernate Configured!");
		//}
		//public static ISessionFactory GetSessionFactory(Configuration Cfg)
		//{
		//	//Source : http://devio.wordpress.com/2012/07/05/getting-started-with-sqlite-and-loquacious-nhibernate-3-2/
		//	var mapper = new ModelMapper();
		//	//typeof(Account).Assembly
		//	mapper.AddMappings(typeof(UserMap).Assembly.GetExportedTypes());
		//	HbmMapping domainMapping =
		//	  mapper.CompileMappingForAllExplicitlyAddedEntities();


		//	Cfg.AddMapping(domainMapping);

		//	var sessionFactory = Cfg.BuildSessionFactory();
		//	return sessionFactory;
		//}

		//public static Configuration Configure()
		//{
		//	Configuration Cfg = new Configuration();

		//	var nhConfig = Cfg.Proxy(proxy =>
		//	proxy.ProxyFactoryFactory<ProxyFactoryFactory>())
		//	.DataBaseIntegration(db =>
		//	{
		//		db.Dialect<MsSql2008Dialect>();
		//		db.ConnectionStringName = "db";
		//		db.BatchSize = 100;
		//	}
		//	);
		//	var typeName = "WallBackend.Core";
		//	Assembly coreAssembly = null;
		//	foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
		//	{
		//		Type t = currentassembly.GetType(typeName, false, true);
		//		if (t != null) { coreAssembly = currentassembly; }
		//	}
		//	if (coreAssembly == null)
		//	{
		//		throw new ApplicationException("Core not found!");
		//	}
		//	var mapper = new ModelMapper();
		//	mapper.AddMappings(coreAssembly.GetExportedTypes());
		//	HbmMapping domainMapping =
		//	  mapper.CompileMappingForAllExplicitlyAddedEntities();
		//	Cfg.AddMapping(domainMapping);
		//	return Cfg;
		//}
	}
}
