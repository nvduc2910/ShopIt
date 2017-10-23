using System;
using Newtonsoft.Json;

namespace ShopIt.Core.Models
{
	public class Document
	{
		[JsonIgnoreAttribute]
		private long? mId;

		public long? Id
		{
			get
			{
				if (mId == null)
				{
					mId = -1;
				}
				return mId;
			}
			set
			{
				mId = value;
			}
		}

		//public int UserProfileId { get; set; }
		//public string MediaType { get; set; }
		public string LocalMedia { get; set; }
		public string Media { get; set; }
		public DateTimeOffset MediaCreated { get; set; }

		private int userProfileId;
		public int UserProfileId
		{
			get
			{
				if (userProfileId == 0)
				{
					userProfileId = -1;
				}
				return userProfileId;
			}
			set
			{
				userProfileId = value;
			}
		}
	}
}
