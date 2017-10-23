using System;

namespace ShopIt.Core.Models
{
	public class UserData
	{
		public int UserId { get; set; }
		public string UserKey { get; set; }
		public string BearerToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTimeOffset Expires { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
	}
}
