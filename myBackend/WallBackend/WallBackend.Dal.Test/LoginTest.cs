using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using NHibernate.Cfg;
using NUnit.Framework;
using WallBackend.Core;
using WallBackend.Dal;
using WallBackend.Providers;

namespace WallBackend.Dal.Test
{
	[TestFixture]
	public class LoginTest : TestBase
	{
		[SetUp]
		public void Setup()
		{
			DalCfg = new DalConfiguration();
			DalCfg.Configure();
			SessionFactory = DalCfg.getSessionFactory();
		}
		[Test]
		public override void Can_generate_schema()
		{
			base.Can_generate_schema();
		}
		[Test]
		public void Non_existing_account_returns_null()
		{
			System.Web.Security.MembershipProvider p = new WallBackend.Providers.nHibernateMembershipProvider();
			User u = new User() { Username = "XXX", Password = "test" };
			//session and transactions should be handled by provider implementation!
			//using (var s = SessionFactory.OpenSession())
			//{
			//	using (var t = s.BeginTransaction())
			//	{
			MembershipUser u1 = p.GetUser(u.Username, false);
			Assert.IsNull(u1);
			//	}
			//}

		}

		[Test]
		public void Can_create_account()
		{
			System.Web.Security.MembershipProvider p = new WallBackend.Providers.nHibernateMembershipProvider();
			User u = new User() { Username = "test", Password = "Test" };
			MembershipCreateStatus status;
			MembershipUser mu = null;
			using (var s = SessionFactory.OpenSession())
			{
				using (var t = s.BeginTransaction())
				{
					mu = p.CreateUser(u.Username, u.Password, null, null, null, true, null, out status);
					Assert.IsNotNull(mu);
					Assert.AreEqual(u.Username, mu.UserName);
				}
			}
		}


	}
}
