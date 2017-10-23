using System;
using System.Linq;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;

namespace ViewModels.Items
{
	public class InviteCreateProjectItemViewModel : MvxViewModel
	{

		#region Constructor
		public InviteCreateProjectItemViewModel(CreateProjectViewModel createProfileViewModel,  Invite invite)
		{
			this.Invite = invite;
			this.CreateProfileViewModel = createProfileViewModel;
		}

		#endregion


		#region Properties

		#region Invite
		private Invite mInvite;

		public Invite Invite
		{
			get
			{
				return mInvite;
			}
			set
			{
				mInvite = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CreateProfileViewModel
		private CreateProjectViewModel mCreateProfileViewModel;

		public CreateProjectViewModel CreateProfileViewModel
		{
			get
			{
				return mCreateProfileViewModel;
			}
			set
			{
				mCreateProfileViewModel = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion


		#region Commands

		#region RemoveInviteCommand
        private MvxCommand mRemoveInviteCommand;

		public MvxCommand RemoveInviteCommand
		{
			get
			{
				if (mRemoveInviteCommand == null)
				{
					mRemoveInviteCommand = new MvxCommand(this.RemoveInvite);
				}
				return mRemoveInviteCommand;
			}
		}

		private void RemoveInvite()
		{
			
			var item = this;
			if (item != null && CreateProfileViewModel != null)
			{
				CreateProfileViewModel.Invites.Remove(item);
				if (CreateProfileViewModel.Invites.Count == 0)
				{
					CreateProfileViewModel.IsShowButtonTapInvite = true;
				}

			}

		}
		#endregion
			

		#endregion


		#region Methods



		#endregion
	}
}
