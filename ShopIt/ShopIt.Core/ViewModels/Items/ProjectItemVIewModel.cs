using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;

namespace ShopIt.Core.ViewModels.Items
{
	public class ProjectItemViewModel : MvxViewModel
	{
		#region Constructors
		public ProjectItemViewModel(ProjectHeading projectHeading)
		{
			this.ProjectHeading = projectHeading;
		}

		#endregion

		#region Properties

		#region ProjectHeading
		private ProjectHeading mProjectHeading;

		public ProjectHeading ProjectHeading
		{
			get
			{
				return mProjectHeading;
			}
			set
			{
				mProjectHeading = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CreatedJoinedText
		public string CreatedJoinedText
		{
			get
			{
				return mProjectHeading == null ? "" : mProjectHeading.CreatedJoined.ToLocalTime().ToString("dd/MM/yyyy");
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
		}
		#endregion

		#region Commands

		#endregion

		#region Methods

		#endregion
	}
}
