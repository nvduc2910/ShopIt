using System;

namespace ShopIt.Core.Models
{
	public class Alert
	{
		public long Id { get; set; }
		public long? ProjectId { get; set; }   // alerts can be direct (not always related to a project)
		public long? InviteId { get; set; }
		public int? ToUserId { get; set; }
		public DateTimeOffset Created { get; set; } // Displayed on the Alert
		public string Title { get; set; }
		public string Body { get; set; }
		public DateTimeOffset? Read { get; set; }  // marked as read when != null
		public DateTimeOffset? Archived { get; set; }  // marked as Trash when != null

		public Invite Invite { get; set; } // On GET.  Populated when the alert type is an Invite (inviteId != null)
		//public ProjectHeading ProjectHeading { get; set; }  // Detailed info on Project is from Invite

		public AlertProjectHeading AlertProjectHeading { get; set; }  // Detailed info on Project is from Invite

		public Alert Clone()
		{
			Alert alert = new Alert();

			alert.Id = this.Id;
			alert.ProjectId = this.ProjectId;
			alert.InviteId = this.InviteId;
			alert.ToUserId = this.ToUserId;
			alert.Created = this.Created;
			alert.Title = this.Title;
			alert.Body = this.Body;
			alert.Read = this.Read;
			alert.Archived = this.Archived;
			alert.Invite = this.Invite;

			return alert;
		}
	}

	public class AlertProjectHeading
	{
		public long ProjectId { get; set; }

		public ProjectStage Stage { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public bool UserIsCreator { get; set; }

		public System.DateTimeOffset CreatedJoined { get; set; }  // if creator - Created Date else Joined Date

		public bool? Alert { get; set; } // set on GET of ProjectList

		public ProjectOwner ProjectOwner { get; set; }
	}

	//Includes a reference to:

    public class ProjectOwner
	{
		public string Address { get; set; }

		public string CompanyName { get; set; }

		public string Email { get; set; }

		public string Mobile { get; set; }

		public string Name { get; set; }
	}
}
