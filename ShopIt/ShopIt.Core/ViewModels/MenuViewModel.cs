using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Helpers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;

namespace ShopIt.Core.ViewModels
{
	public class MenuViewModel : BaseViewModel
	{
		#region Constructors
		public MenuViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region MainVM

		public MainViewModel MainVM { get; set; }

		#endregion

		#region Properties

		#endregion

		#region Init
		public void Init()
		{
		}
		#endregion

		#region Commands

		#region AboutCommand
		private MvxCommand mAboutCommand;

		public MvxCommand AboutCommand
		{
			get
			{
				if (mAboutCommand == null)
				{
					mAboutCommand = new MvxCommand(this.About);
				}
				return mAboutCommand;
			}
		}

		private void About()
		{
			MainVM.View.HideMenu();
			ShowViewModel<AboutViewModel>();
		}
		#endregion

		#region TourCommand
		private MvxCommand mTourCommand;

		public MvxCommand TourCommand
		{
			get
			{
				if (mTourCommand == null)
				{
					mTourCommand = new MvxCommand(this.Tour);
				}
				return mTourCommand;
			}
		}

		private void Tour()
		{
			MainVM.View.HideMenu();
			ShowViewModel<TourViewModel>(new { mode = TourViewMode.Menu });
		}
		#endregion

		#region LicenseSearchCommand         private MvxCommand mLicenseSearchCommand;  		public MvxCommand LicenseSearchCommand
		{ 			get
			{ 				if (mLicenseSearchCommand == null)
				{ 					mLicenseSearchCommand = new MvxCommand(this.LicenseSearch); 				} 				return mLicenseSearchCommand; 			} 		}  		private void LicenseSearch()
		{
			mPlatformService.OpenWeb("http://www.ShopIt.com.au/licence_search.html"); 		}
		#endregion

		#region SignOutCommand
		private MvxCommand mSignOutCommand;

		public MvxCommand SignOutCommand
		{
			get
			{
				if (mSignOutCommand == null)
				{
					mSignOutCommand = new MvxCommand(this.SignOut);
				}
				return mSignOutCommand;
			}
		}

		private void SignOut()
		{
			MainVM.View.HideMenu();
			ClearStackAndShowViewModel<LoginViewModel>();
			mCacheService.CurrentUserData = null;
			DataHelper.SaveToUserPref<PersonalProfile>(null, "PERSONAL_STEP");
			mApiService.UseBeaerToken = false;
		}
		#endregion

		#region HideCommand
		private MvxCommand mHideCommand;

		public MvxCommand HideCommand
		{
			get
			{
				if (mHideCommand == null)
				{
					mHideCommand = new MvxCommand(this.Hide);
				}
				return mHideCommand;
			}
		}

		private void Hide()
		{
			MainVM.View.HideMenu();
		}
		#endregion

		#endregion

		#region Methods

		#endregion
	}
}
