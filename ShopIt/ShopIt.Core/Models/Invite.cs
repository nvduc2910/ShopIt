using System;
using Newtonsoft.Json;

namespace ShopIt.Core.Models
{
	public class Invite
	{
		public long? Id { get; set; }  // set on GET / PUT 
		public Guid? Uuid { get; set; }  // set on GET
		public string InviteeEmail { get; set; }  // one of Email or Mobile is required
		public string InviteeMobile { get; set; }
		public string InviteeName { get; set; }
		public long ProjectId { get; set; }
		public int? InviteeUserId { get; set; }  // on POST invitee email may not be known.  Later matched when accepted. 
		public bool Active { get; set; }
		public DateTimeOffset? Sent { get; set; }
		public DateTimeOffset? JoinedProject { get; set; }  // Pending until InviteeUserId!=null && JoinedProject != null
		public bool? Alert { get; set; }
		public bool ProfileComplete { get; set; }

		[JsonIgnoreAttribute]
		public bool Pending
		{
			get
			{
				if (JoinedProject == null)
					return true;
				return false;
			}
		}

		[JsonIgnoreAttribute]
		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(InviteeName))
				{
					return InviteeName;
				}

				return InviteeEmail;
			}
		}
	}
}
