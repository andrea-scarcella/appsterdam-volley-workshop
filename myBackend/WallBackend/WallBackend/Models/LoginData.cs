using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WallBackend.Models
{
	public class LoginData
	{
		[Required(AllowEmptyStrings=false)]
		public string Username { get; set; }
		[Required(AllowEmptyStrings = false)]//("Password cannot be blank")]
		public string Password { get; set; }
	}
}