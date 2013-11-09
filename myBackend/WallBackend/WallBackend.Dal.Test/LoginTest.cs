using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Cfg;
using NUnit.Framework;
using WallBackend.Core;
using WallBackend.Dal;

namespace WallBackend.Dal.Test
{
	[TestFixture]
	public class LoginTest : TestBase
	{
		[SetUp]
		public void Setup()
		{
			Cfg = new Configuration();
			Cfg = DalConfiguration.Configure("WallBackend.Core", Cfg);
			SessionFactory = DalConfiguration.GetSessionFactory(Cfg);
			//Cfg.AddAssembly(typeof(User).Assembly);
		}
		[Test]
		public override void Can_generate_schema()
		{
			base.Can_generate_schema();
		}

		
	}
}
