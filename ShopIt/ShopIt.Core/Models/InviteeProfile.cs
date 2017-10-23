using System;

namespace ShopIt.Core.Models
{
	public class InviteeProfile
	{
		public PersonalProfile PersonalProfile { get; set; }
		public Company CompanyProfile { get; set; }  // Null if never created
	}
}
