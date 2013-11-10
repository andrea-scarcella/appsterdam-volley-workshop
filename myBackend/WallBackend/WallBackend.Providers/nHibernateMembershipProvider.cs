using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using NHibernate;
using NHibernate.Criterion;
using WallBackend.Core;
using WallBackend.Dal;
namespace WallBackend.Providers
{
	public class nHibernateMembershipProvider : MembershipProvider
	{
		private DalConfiguration cfg = null;
		private ISessionFactory sessionFactory = null;
		private ISession currentSession { get; set; }
		private void VerifySessionBound()
		{
			if (NHibernate.Context.CurrentSessionContext.HasBind(sessionFactory))
			{
				currentSession = sessionFactory.GetCurrentSession();
			}
			else
			{
				currentSession = sessionFactory.OpenSession();
				NHibernate.Context.CurrentSessionContext.Bind(currentSession);
			}
		}
		public nHibernateMembershipProvider()
		{
			cfg = new DalConfiguration();
			cfg.Configure();
			sessionFactory = cfg.getSessionFactory();
		}
		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new NotImplementedException();
		}

		public override MembershipUser CreateUser(string username,
			string password,
			string email,
			string passwordQuestion,
			string passwordAnswer,
			bool isApproved,
			object providerUserKey,
			out MembershipCreateStatus status)
		{
			var args = new ValidatePasswordEventArgs(username, password, true);

			this.OnValidatingPassword(args);

			if (args.Cancel)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			//just save it!

			throw new NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new NotImplementedException();
		}

		public override bool EnablePasswordReset
		{
			get { throw new NotImplementedException(); }
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}
		

		
		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			VerifySessionBound();
			MembershipUser outp = null;
			//using (var tempSession = sessionFactory.OpenSession())
			//{
			//	using (var tempTransaction = tempSession.BeginTransaction())
			//	{
					QueryOver<User> q = QueryOver
				.Of<User>()
				.Where(c => c.Username == username);
					User u = null;
					if (q != null)
					{
						u=q.GetExecutableQueryOver(cfg.getSessionFactory().GetCurrentSession()).SingleOrDefault();
					}

					if (u != null)
					{
						#region deprecated
						//outp = new MembershipUser(
						//	"",
						//	username,
						//	null,
						//	null,
						//	null,
						//	null,
						//	true,
						//	false,
						//	DateTime.Now,//creation date
						//	DateTime.Now,//last login
						//	DateTime.MinValue,//last activity
						//	DateTime.MinValue,//last psw changed
						//	DateTime.MinValue);//lockout 
						#endregion
						outp = new WallMembershipUser(u.Username, u.Password);
						
					}
			//	}
			//}
			
			return outp;
			//throw new NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new NotImplementedException();
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new NotImplementedException(); }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new NotImplementedException(); }
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{
			throw new NotImplementedException();
		}
	}

	public class WallMembershipUser : MembershipUser
	{
		public WallMembershipUser(string username, string password)
			: base("WallBackend.Providers.nHibernateMembershipProvider",
					username,
					null,
					null,
					null,
					null,
					true,
					false,
					DateTime.Now,//creation date
					DateTime.Now,//last login
					DateTime.MinValue,//last activity
					DateTime.MinValue,//last psw changed
					DateTime.MinValue)
		{

		}
	}
}
