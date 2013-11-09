using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WallBackend.Models
{
	public class LoginData
	{
		[Required("Username cannot be blank")]
		public string Username { get; set; }
		[Required("Password cannot be blank")]
		public string Password { get; set; }
	}
}