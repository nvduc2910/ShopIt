using System;
using System.Collections.ObjectModel;
using MvvmCross.Platform;
using Newtonsoft.Json;
using ShopIt.Core.ViewModels.Items;
using ShopIt.Core.Models;
using ShopIt.Core.Helpers;
using ViewModels.Items;
using MvvmCross.Core.ViewModels;

namespace ShopIt.Core.Services
{
	public interface ICacheService
	{
		// save data to pref
		UserData CurrentUserData { get; set; }

		// data between screens
		AlertItemViewModel AlertItem { get; set; }
		MvxObservableCollection<AlertItemViewModel> AlertItems { get; set; }
		MvxObservableCollection<ProjectItemViewModel> ProjectItems { get; set; }
		string TempDescription { get; set; }
		ProjectItemViewModel ProjectHeadingItem { get; set; }
		Project ProjectItem { get; set; }
		MvxObservableCollection<Invite> EdittingProjectInvitee { get; set; }
		UserProfile UserProfileItem { get; set; }
		CertTypeItemViewModel CertTypeItem { get; set;}
		bool IsShowProfileView { get; set; }
		string DeviceToken { get; set; }
		MvxObservableCollection<Invite> EdittingProjectInviteeStatic { get; set; }
		int AmountAlert { get; set; }
	}

	public class CacheService : ICacheService
	{
		// save data to pref
		#region CurrentUserData
		private UserData mCurrentUserData;
		public UserData CurrentUserData
		{
			get
			{
				if (mCurrentUserData == null)
					mCurrentUserData = DataHelper.RetrieveFromUserPref<UserData>();
				return mCurrentUserData;
			}
			set
			{
				mCurrentUserData = value;
				DataHelper.SaveToUserPref<UserData>(mCurrentUserData);
			}
		}
		#endregion

		// data between screens
		public AlertItemViewModel AlertItem { get; set; }
		public MvxObservableCollection<AlertItemViewModel> AlertItems { get; set; }
		public MvxObservableCollection<ProjectItemViewModel> ProjectItems { get; set; }
		public string TempDescription { get; set; }

		public ProjectItemViewModel ProjectHeadingItem { get; set; }

		public Project ProjectItem { get; set; }
		public MvxObservableCollection<Invite> EdittingProjectInvitee { get; set; }

		public UserProfile UserProfileItem { get; set; }

		public CertTypeItemViewModel CertTypeItem { get; set; }

		public bool IsShowProfileView { get; set; }

		public string DeviceToken { get; set; }

		public int AmountAlert
		{
			get;
			set;
		}

		public MvxObservableCollection<Invite> EdittingProjectInviteeStatic { get; set; }
	}
}
