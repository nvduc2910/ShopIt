using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using Validators;
using ShopIt.Core.Models;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using ShopIt.Core.Helpers;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ShopIt.Core.ViewModels
{
	public interface ISignUpView
	{
		void HideKeyboard();
	}

	public class SignUpViewModel : BaseViewModel
	{
		#region Constructors
		public SignUpViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
			
		}

		#endregion

		#region View

		public ISignUpView View { get; set; }

		#endregion

		#region Properties
		#region UserProfile

		private UserProfile mUserProfile = new UserProfile();

		#endregion

		#region Email
        private string mEmail;

		public string Email
		{
			get
			{
				return mEmail;
			}
			set
			{
				mEmail = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Password
        private string mPassword;

		public string Password
		{
			get
			{
				return mPassword;
			}
			set
			{
				mPassword = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ConfirmPassword
        private string mConfirmPassword;

		public string ConfirmPassword
		{
			get
			{
				return mConfirmPassword;
			}
			set
			{
				mConfirmPassword = value;
				RaisePropertyChanged();
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

		#region SignUpCommand
        private MvxCommand mSignUpCommand;

		public MvxCommand SignUpCommand
		{
			get
			{
				if (mSignUpCommand == null)
				{
					mSignUpCommand = new MvxCommand(this.SignUp);
				}
				return mSignUpCommand;
			}
		}

		private async void SignUp()
		{
			View.HideKeyboard();

			SignUpValidator validator = new SignUpValidator();
			Register register = new Register
			{
				Email = Email,
				Password = Password,
				ConfirmPassword = ConfirmPassword
			};
			var result = validator.Validate(register);
			if (result.IsValid)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var response = await mApiService.PostSignup(register);

				//mPlatformService.HideNetworkIndicator();
				//mProgressDialogService.HideProgressDialog();

				if (response.StatusCode == HttpStatusCode.OK)
				{
					//mPlatformService.ShowNetworkIndicator();
					//mProgressDialogService.ShowProgressDialog();

					var response2 = await mApiService.PostLogin(Email, Password);

					//mPlatformService.HideNetworkIndicator();
					//mProgressDialogService.HideProgressDialog();

					if (response2.StatusCode == HttpStatusCode.OK)
					{
						var userData = response2.Data;
						mCacheService.CurrentUserData = userData;
						mApiService.UseBeaerToken = true;

						// get userkey
						//mPlatformService.ShowNetworkIndicator();
						//mProgressDialogService.ShowProgressDialog();

						var response3 = await mApiService.PostSession(mPlatformService.GetDeviceUDID(), userData.UserName);

						//mPlatformService.HideNetworkIndicator();
						//mProgressDialogService.HideProgressDialog();

						if (response3.StatusCode == HttpStatusCode.OK)
						{
							userData.UserKey = response3.Data;
							mCacheService.CurrentUserData = userData;

							await GetProfile();

							ShowViewModel<MainViewModel>();
						}
						else
						{
							mApiService.UseBeaerToken = false;
						}
					}

				}
				else if (response.StatusCode == HttpStatusCode.BadRequest)
				{
					//string errorData = response.ErrorData;
					//mMessageboxService.ShowToast(errorData);
					mMessageboxService.ShowToast("Email already in use!");
					mPlatformService.HideNetworkIndicator();
					mProgressDialogService.HideProgressDialog();
				}
			}
			else if (result.Errors != null)
			{
				var firstError = result.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();
			}
		}
		#endregion

		#region PrivacyPolicyCommand
		private MvxCommand mPrivacyPolicyCommand;

		public MvxCommand PrivacyPolicyCommand
		{
			get
			{
				if (mPrivacyPolicyCommand == null)
				{
					mPrivacyPolicyCommand = new MvxCommand(this.PrivacyPolicy);
				}
				return mPrivacyPolicyCommand;
			}
		}

		private void PrivacyPolicy()
		{
			ShowViewModel<LocalWebViewModel>(new { title = "Privacy Policy", fileName = "PP.html" });
		}
		#endregion

		#region TermsOfServiceCommand
		private MvxCommand mTermsOfServiceCommand;

		public MvxCommand TermsOfServiceCommand
		{
			get
			{
				if (mTermsOfServiceCommand == null)
				{
					mTermsOfServiceCommand = new MvxCommand(this.TermsOfService);
				}
				return mTermsOfServiceCommand;
			}
		}

		private void TermsOfService()
		{
			ShowViewModel<LocalWebViewModel>(new { title = "Terms of Service", fileName = "ToS.html" });
		}
		#endregion

		#endregion

		#region Methods
		public async Task GetProfile()
		{
			
			var response = await mApiService.GetProfile();

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

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

		#endregion
    }
}
