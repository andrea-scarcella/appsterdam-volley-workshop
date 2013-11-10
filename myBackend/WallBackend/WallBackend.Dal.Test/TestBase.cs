using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace WallBackend.Dal.Test
{
	public class TestBase
	{
		protected DalConfiguration DalCfg { get; set; }
		protected ISessionFactory SessionFactory { get; set; }
		protected Configuration Cfg;
		[Test]
		public virtual void Can_generate_schema()
		{
			//goede instellingen om problemen met System.Data.SqlServerCe te voorkomen:
			//copy local=true!
			//http://sarkies.blogspot.it/2010/07/could-not-create-driver-from.html
			//je moet de stand 'Build action' voor elke mappingbestand op 'Embedded resource'  zetten!
			DalCfg.getSchemaExport().Execute(true, true, false);
			//new SchemaExport(Cfg).Execute(true, true, false);
			//(false, true, false, false);
		}
	}
}
