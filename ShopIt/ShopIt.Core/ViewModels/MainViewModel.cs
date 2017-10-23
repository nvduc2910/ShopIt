using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ShopIt.Core.Services;

namespace ShopIt.Core.ViewModels
{
	public interface IMainView
	{
		void ShowMenu();
		void HideMenu();
	}

	public class MainViewModel : BaseViewModel
	{
		#region Constructors
		public MainViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region View

		public IMainView View { get; set; }

		#endregion

		#region Properties

		#region HomeVM
		private HomeViewModel mHomeVM;

		public HomeViewModel HomeVM
		{
			get
			{
				return mHomeVM;
			}
			set
			{
				mHomeVM = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region MenuVM
		private MenuViewModel mMenuVM;

		public MenuViewModel MenuVM
		{
			get
			{
				return mMenuVM;
			}
			set
			{
				mMenuVM = value;
				RaisePropertyChanged();
			}
		}
		#endregion


		#endregion

		#region Init
		public void Init()
		{
			MenuVM = new MenuViewModel(mApiService, mCacheService, mMessageboxService, mProgressDialogService, mPlatformService);
			MenuVM.MainVM = this;
			MenuVM.Init();

			HomeVM = new HomeViewModel(mApiService, mCacheService, mMessageboxService, mProgressDialogService, mPlatformService);
			HomeVM.MainVM = this;
			HomeVM.Init();


			RegisterNotification();
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
			if (View != null)
			{
				View.ShowMenu();
			}
		}
		#endregion

		#region HideMenuCommand
        private MvxCommand mHideMenuCommand;

		public MvxCommand HideMenuCommand
		{
			get
			{
				if (mHideMenuCommand == null)
				{
					mHideMenuCommand = new MvxCommand(this.HideMenu);
				}
				return mHideMenuCommand;
			}
		}

		private void HideMenu()
		{
			if (View != null)
			{
				View.HideMenu();
			}
		}
		#endregion

		#endregion

		#region Methods
		public async void RegisterNotification()
		{
			if (!string.IsNullOrEmpty(mCacheService.DeviceToken) && !string.IsNullOrEmpty(mPlatformService.GetDeviceUDID()))
			{
				var succeed = await mApiService.NotifyDevice(mPlatformService.GetDeviceUDID(), mCacheService.DeviceToken);
			}
		}
		#endregion
	}
}
