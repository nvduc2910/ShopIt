using System;
using ShopIt.Core.Models;

namespace ShopIt.Core.Models
{
	public class UserProfile
	{
		public PersonalProfile PersonalProfile { get; set; }
		public Company CompanyProfile { get; set; }

		public UserProfile Clone()
		{
			UserProfile clone = new UserProfile();

			if (this.PersonalProfile != null)
			{
				clone.PersonalProfile = this.PersonalProfile.Clone();
			}

			if (this.CompanyProfile != null)
			{
				clone.CompanyProfile = this.CompanyProfile.Clone();
			}

			return clone;
		}
	}
}
