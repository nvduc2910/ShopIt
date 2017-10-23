using System;
using Newtonsoft.Json;

namespace ShopIt.Core.Models
{
	public class UserResponse
	{
		[JsonProperty("userId")]
		public int user_id { get; set; }

		public string external_user_id { get; set; }

		public string access_token { get; set; }

		public string token_type { get; set; }

		public float expires_in { get; set; }

		public string username { get; set; }

		public DateTimeOffset issued { get; set; }

		[JsonProperty(".expires")]
		public DateTimeOffset expires { get; set; }

		public string sessionUserKey { get; set; }

		public string firstName { get; set; }

		public string lastName { get; set; }

		public string refresh_token { get; set; }

		public string profile_picture_url { get; set; }

		public string email_address { get; set; }
	}

	public class GetRefreshTokenResponseData
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }
		public string userName { get; set; }
		public string issued { get; set; }
		public string expires { get; set; }
		public string refresh_token { get; set; }
	}


	public class RefreshTokenResponseData
	{
		public string access_token { get; set; }
		public string token_type { get; set; }
		public int expires_in { get; set; }

		[JsonProperty(".issued")]
		public DateTimeOffset issued { get; set; }

		[JsonProperty(".expires")]
		public DateTimeOffset expires { get; set; }

		public string refresh_token { get; set; }
	}

	public class ErrorLoginResponse
	{
		[JsonProperty("error")]
		public string Error { get; set; }

		[JsonProperty("error_description")]
		public string Description { get; set; }
	}
}
