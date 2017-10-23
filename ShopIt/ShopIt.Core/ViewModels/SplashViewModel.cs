using System.Diagnostics;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Newtonsoft.Json;
using Services;
using ShopIt.Core;
using ShopIt.Core.Helpers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;

public class SplashViewModel : BaseViewModel
{
	#region Constructors
	public SplashViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
	{
	}

	#endregion

	#region Properties

	#region UserProfile

	private UserProfile mUserProfile = new UserProfile();

	#endregion

	#endregion

	#region Init
	public async void Init()
	{
	    if (mPlatformService.OS == OS.Droid)
	    {
            await Task.Delay(100);
        }

		Mvx.Resolve<ITrackingService>().Initialize(AppConstants.MixPanelToken);

		Mvx.Resolve<ITrackingService>().SendEvent("Open App");
 		string firstTimeSignIn = mPlatformService.GetPreference(AppConstants.FirstTimeSignIn);

		if (string.IsNullOrEmpty(firstTimeSignIn))
		{
			mPlatformService.SetPreference(AppConstants.FirstTimeSignIn, "false");
			ShowViewModel<TourViewModel>(new { mode = TourViewMode.FirstTime });
		}
		else
		{
			if (mCacheService.CurrentUserData != null)
			{
				mApiService.UseBeaerToken = true;

				await GetProfile();
				//await GetAlert();
				ShowViewModel<MainViewModel>();
			}
			else
			{
				ShowViewModel<LoginViewModel>();
			}
		}
	}
	#endregion

	#region Commands

	#endregion

	#region Methods

	public async Task GetProfile()
	{
		//mPlatformService.ShowNetworkIndicator();
		//mProgressDialogService.ShowProgressDialog();

		var response = await mApiService.GetProfile();

		//mPlatformService.HideNetworkIndicator();
		//mProgressDialogService.HideProgressDialog();

		if (response.StatusCode == System.Net.HttpStatusCode.OK)
		{

			mUserProfile = response.Data;
			DataHelper.SaveToUserPref(mUserProfile, "UserProfile");

			mCacheService.UserProfileItem = mUserProfile;

			if (mUserProfile.PersonalProfile.ProfileCompletionStep == ProfileCompletionStep.PPComplete)
			{
				mCacheService.IsShowProfileView = true;
			}
			else
			{
				mCacheService.IsShowProfileView = false;
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(response.ErrorData))
			{
				string errorData = response.ErrorData;
				ErrorLoginResponse err = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorData);
				Debug.WriteLine("Error" + err.Description);
				mMessageboxService.ShowToast(err.Description);
			}
		}
	}

	public async Task GetAlert()
	{
		var response = await mApiService.GetAlertList();

		foreach (var item in response.Data)
		{
			if (item.Read == null)
			{
				mCacheService.AmountAlert++;
			}
		}
	}

	#endregion
}