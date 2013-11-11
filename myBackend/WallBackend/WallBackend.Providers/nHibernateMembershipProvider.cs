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

		private ISession currentSession { get; set; }

		public nHibernateMembershipProvider()
		{
			//issue in webapp, find a way to initialize 'currentSession'
		}
		public nHibernateMembershipProvider(ISession s)
		{
			currentSession = s;
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
		protected override void OnValidatingPassword(ValidatePasswordEventArgs e)
		{
			base.OnValidatingPassword(e);

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

			status = MembershipCreateStatus.Success;
			var args = new ValidatePasswordEventArgs(username, password, true);
			this.OnValidatingPassword(args);
			if (args.Cancel)
			{
				status = MembershipCreateStatus.InvalidPassword;
				return null;
			}
			//just save it!
			WallMembershipUser u = null;
			User u0 = new User() { Password = password, Username = username };
			object currId = null;
			currId = currentSession.Save(u0);//as WallMembershipUser;
			u0 = currentSession.Get<User>(currId);
			u = new WallMembershipUser(u0.Username, u0.Password);
			return u;
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
			MembershipUser outp = null;
			QueryOver<User> q = QueryOver
		.Of<User>()
		.Where(c => c.Username == username);
			User u = null;
			if (q != null)
			{
				u = q.GetExecutableQueryOver(currentSession).SingleOrDefault();
			}
			if (u != null)
			{
				outp = new WallMembershipUser(u.Username, u.Password);
			}
			return outp;
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
