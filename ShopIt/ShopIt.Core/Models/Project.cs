using System;
using System.Collections.Generic;

namespace ShopIt.Core.Models
{
	public class Project
	{
		public long? Id { get; set; } // for use on GET and PUT
		public int? UserId { get; set; } // Creator - GET and PUT
		public ProjectStage Stage { get; set; }
		public ProjectStatus Status { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		//public long SectionId { get; set; }  Future expansion
		//public long? CategoryId { get; set; }  Future expansion

		public ProjectOwner Owner { get; set; }

		public bool Active { get; set; }

		public DateTimeOffset? Created { get; set; }
		public DateTimeOffset? Started { get; set; }
		public DateTimeOffset? Updated { get; set; }

		public List<Invite> Invites { get; set; }
	}

	public enum ProjectStage
	{
		Tender = 0,
		Site
	}

	public enum ProjectStatus
	{
		Green = 0,
		Red = 2
	}

	public class ProjectHeading
	{
		public long ProjectId { get; set; }
		public ProjectStage Stage { get; set; }
		public string Title { get; set; }

		public string OwnerName { get; set; }
		public bool UserIsCreator { get; set; }
		public DateTimeOffset CreatedJoined { get; set; }  // if creator - Created Date else Joined Date

		public bool Alert { get; set; }
	}
}
