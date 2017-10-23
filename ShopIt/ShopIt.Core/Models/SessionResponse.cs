using System;

namespace ShopIt.Core.Models
{
	public class SessionResponse
	{
		public string SessionKey { get; set; }
		public int UserId { get; set; }
	}

	public class Register
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }

		public string FirstName { get; set; }  // ignore
		public string LastName { get; set; }  // ignore
	}

	public class Login
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
