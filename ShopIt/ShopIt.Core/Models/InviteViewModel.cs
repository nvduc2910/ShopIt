using System;

namespace Swapcerts.Core.Models
{
	public class Invite
	{
		public long? Id { get; set; }  // set on GET / PUT
		public Guid? Uuid { get; set; }  // set on GE
		public string InviteeEmail { get; set; }  // one of Email or Mobile is require
		public string InviteeMobile { get; set; }
		public long ProjectId { get; set; 
		public int? InviteeUserId { get; set; }  // on POST invitee email may not be known.  Later matched when accepted.
		public bool Active { get; set; }
		public DateTimeOffset? Sent { get; set; }
		public DateTimeOffset? JoinedProject { get; set; }  // Pending until InviteeUserId!=null && JoinedProject != nul
	}
}
