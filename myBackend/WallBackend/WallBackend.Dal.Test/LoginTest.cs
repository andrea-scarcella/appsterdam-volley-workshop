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
		System.Web.Security.MembershipProvider p = null;
		[SetUp]
		public void Setup()
		{
			DalCfg = new DalConfiguration();
			DalCfg.Configure();
			SessionFactory = DalCfg.getSessionFactory();
			Session = SessionFactory.OpenSession();
			p = new WallBackend.Providers.nHibernateMembershipProvider(Session);
			DalCfg.getSchemaExport().Drop(true, true);
			DalCfg.getSchemaExport().Create(true, true);
		}
		[Test]
		public override void Can_generate_schema()
		{
			base.Can_generate_schema();
		}
		[Test]
		public void Non_existing_account_returns_null()
		{

			User u = new User() { Username = "XXX", Password = "test" };
			//session and transactions should be handled by provider implementation!
			MembershipUser u1 = p.GetUser(u.Username, false);
			Assert.IsNull(u1);
		}
		[Test]
		public void Can_return_account()
		{

			User u = new User() { Username = "test", Password = "test" };
			//session and transactions should be handled by provider implementation!
			//sessions are shared by all objects taking part in a request
			MembershipUser u1 = p.GetUser(u.Username, false);
			Assert.IsNotNull(u1);
			Assert.AreEqual(u.Username, u1.UserName);

		}
		[Test]
		public void Can_create_account()
		{
			//System.Web.Security.MembershipProvider p = new WallBackend.Providers.nHibernateMembershipProvider();
			User u = new User() { Username = "baz", Password = "baz" };
			MembershipCreateStatus status;
			MembershipUser mu = null;
			var s0 = Session;
			using (var t0 = s0.BeginTransaction())
			{
				mu = p.CreateUser(u.Username, u.Password, null, null, null, true, null, out status);
				Assert.IsNotNull(mu);
				Assert.AreEqual(u.Username, mu.UserName);
				Assert.AreEqual(status, MembershipCreateStatus.Success);
				t0.Rollback();
			}
		}




	}
}
