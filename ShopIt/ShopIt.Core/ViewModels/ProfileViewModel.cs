using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;
using ViewModels;
using MvvmCross.Platform;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using ShopIt.Core.Models;
using Newtonsoft.Json;
using ShopIt.Core.Helpers;
using MvvmCross.Plugins.Messenger;
using ShopIt.Core.Messengers;
using Services;

namespace ShopIt.Core.ViewModels
{
	public class ProfileViewModel : BaseViewModel
	{
		#region Constructors
		public ProfileViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
			RegisterMessengers();
		}

		#endregion

		#region HomeVM

		public HomeViewModel HomeVM { get; set; }

		#endregion

		#region Properties

		#region UserProfile

		private UserProfile mUserProfile = new UserProfile();

		#endregion

		#region IsShowViewProfileView
		private bool mIsShowViewProfileView = false;

		public bool IsShowViewProfileView
		{
			get
			{
				return mIsShowViewProfileView;
			}
			set
			{
				mIsShowViewProfileView = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowAlert
		private bool mIsShowAlert;

		public bool IsShowAlert
		{
			get
			{
				return mIsShowAlert;
			}
			set
			{
				mIsShowAlert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public async void Init()
		{
			mUserProfile = DataHelper.RetrieveFromUserPref<UserProfile>("UserProfile");
			IsShowViewProfileView = mCacheService.IsShowProfileView ? true : false;

			//mPlatformService.ShowNetworkIndicator();
			//mProgressDialogService.ShowProgressDialog();

			//var response = await mApiService.GetProfile();

			//mPlatformService.HideNetworkIndicator();
			//mProgressDialogService.HideProgressDialog();

			//if (response.StatusCode == System.Net.HttpStatusCode.OK)
			//{

			//	mUserProfile = response.Data;
			//	DataHelper.SaveToUserPref(mUserProfile, "UserProfile");

			//	mCacheService.UserProfileItem = mUserProfile;

			//	if (mUserProfile.PersonalProfile.ProfileCompletionStep == ProfileCompletionStep.PPComplete)
			//	{
			//		mCacheService.IsShowProfileView = true;
			//	}
			//	else
			//	{
			//		mCacheService.IsShowProfileView = false;
			//	}
			//}
			//else
			//{
			//	if (!string.IsNullOrEmpty(response.ErrorData))
			//	{
			//		string errorData = response.ErrorData;
			//		ErrorLoginResponse err = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorData);
			//		Debug.WriteLine("Error" + err.Description);
			//		mMessageboxService.ShowToast(err.Description);
			//	}
			//}
		}
		#endregion

		#region Commands

		#region ShowMenuCommand
		private MvxCommand mShowMenuCommand;

		public MvxCommand ShowMenuCommand
		{
			get
			{
				if (mShowMenuCommand == null)
				{
					mShowMenuCommand = new MvxCommand(this.ShowMenu);
				}
				return mShowMenuCommand;
			}
		}

		private void ShowMenu()
		{
			HomeVM.MainVM.ShowMenuCommand.Execute();
		}
		#endregion

		#region CreateProfileCommand
		private MvxCommand  mCreateProfileCommand;

		public MvxCommand  CreateProfileCommand
		{
			get
			{
				if (mCreateProfileCommand == null)
				{
					mCreateProfileCommand = new MvxCommand(this.CreateProfile);
				}
				return mCreateProfileCommand;
			}
		}

		private void CreateProfile()
		{
			DataHelper.SaveToUserPref(mUserProfile, "UserProfile");
			ShowViewModel<CreatePersonalProfileViewModel>();
		}
		#endregion

		#region ViewProfileCommand
        private MvxCommand mViewProfileCommand;

		public MvxCommand ViewProfileCommand
		{
			get
			{
				if (mViewProfileCommand == null)
				{
					mViewProfileCommand = new MvxCommand(this.ViewProfile);
				}
				return mViewProfileCommand;
			}
		}

		private void ViewProfile()
		{
			ShowViewModel<ViewProfileViewModel>(new { isThisUser = true, userId = -1 });
			//mCacheService.UserProfileItem = mUserProfile;
			//ShowViewModel<CreateCompanyProfileViewModel>();
		}
		#endregion

		#region ShareProfileCommand

		private MvxAsyncCommand mShareProfileCommand;

		public MvxAsyncCommand ShareProfileCommand
		{
			get
			{
				if (mShareProfileCommand == null)
				{
					mShareProfileCommand = new MvxAsyncCommand(this.ShareProfile);
				}
				return mShareProfileCommand;
			}
		}

		private async Task ShareProfile()
		{
			mPlatformService.ShowNetworkIndicator();
			mProgressDialogService.ShowProgressDialog();

			var succeed = await mApiService.ShareProfile(mUserProfile.PersonalProfile.UserId);

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

			if (succeed)
			{
				Mvx.Resolve<ITrackingService>().SendEvent("Main send action");
				mMessageboxService.Show("Share Profile", "Check your inbox in a few minutes. Then forward your ShopIt profile to your intended recipients");
			}
		}

		#endregion

		#endregion

		#region Methods

		#region UserProfileCompleted

		public bool UserProfileCompleted()
		{
			if (IsShowViewProfileView)
			{
				return true;
			}
			return false;
		}
		#endregion

		#region Messenger
		MvxSubscriptionToken actionSubscriptionToken;

		private void RegisterMessengers()
		{
			actionSubscriptionToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<ViewModelActionMessage>(HandleViewModelAction);
		}

		private void HandleViewModelAction(ViewModelActionMessage obj)
		{
			if (obj.Action.ViewModelType == this.GetType())
			{
				if (obj.Action.ActionType == ViewModelActionType.Reload)
				{
					string completedProfile = obj.Action.Data as string;
					IsShowViewProfileView = completedProfile == "COMPLETED_PROFILE" ? true : false;
				}
			}
		}
		private void UnRegisterMessengers()
		{
			if (actionSubscriptionToken != null)
			{
				Mvx.Resolve<IMvxMessenger>().Unsubscribe<ViewModelActionMessage>(actionSubscriptionToken);
				actionSubscriptionToken = null;
			}
		}
		#endregion

		#region Destroy

		public override void Destroy()
		{
			base.Destroy();
			UnRegisterMessengers();
		}

		#endregion

		#endregion
	}
}
			