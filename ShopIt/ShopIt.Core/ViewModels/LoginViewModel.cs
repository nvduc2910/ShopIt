using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;
using Validators;
using ShopIt.Core.Models;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ShopIt.Core.Helpers;
using System.Diagnostics;

namespace ShopIt.Core.ViewModels
{
	public interface ILoginView
	{
		void ShowPopUpForgotPassword();
		void HidePopupForgotPassword();

		void HideKeyboard();
	}

	public class LoginViewModel : BaseViewModel
	{
		#region Constructors
        public LoginViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			 : base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
			
		}
		#endregion

		#region view 

		public ILoginView View { get; set; }

		#endregion

		#region Properties

		#region IsShowPopupForgotPassword
		private bool mIsShowPopupForgotPassword;

		public bool IsShowPopupForgotPassword
		{
			get
			{
				return mIsShowPopupForgotPassword;
			}
			set
			{
				mIsShowPopupForgotPassword = value;
				RaisePropertyChanged();
			}
		}
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

		#region ForgotEmail
		private string mForgotEmail = string.Empty;

		public string ForgotEmail
		{
			get
			{
				return mForgotEmail;
			}
			set
			{
				mForgotEmail = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region UserProfile

		private UserProfile mUserProfile = new UserProfile();

		#endregion
		#endregion

		#region Init
		public void Init()
		{
           
        }
		#endregion

		#region Commands

		#region ForgotPasswordCommand
        private MvxCommand mForgotPasswordCommand;

		public MvxCommand ForgotPasswordCommand
		{
			get
			{
				if (mForgotPasswordCommand == null)
				{
					mForgotPasswordCommand = new MvxCommand(this.ForgotPassword);
				}
				return mForgotPasswordCommand;
			}
		}

		private void ForgotPassword()
		{
			View.HideKeyboard();
			View.ShowPopUpForgotPassword();
		}
		#endregion

		#region CancelForgotPasswordCommand
        private MvxCommand mCancelForgotPasswordCommand;

		public MvxCommand CancelForgotPasswordCommand
		{
			get
			{
				if (mCancelForgotPasswordCommand == null)
				{
					mCancelForgotPasswordCommand = new MvxCommand(this.CancelForgotPassword);
				}
				return mCancelForgotPasswordCommand;
			}
		}

		private void CancelForgotPassword()
		{
			View.HidePopupForgotPassword();
		}
		#endregion

		#region SignInCommand
        private MvxCommand mSignInCommand;

		public MvxCommand SignInCommand
		{
			get
			{
				if (mSignInCommand == null)
				{
					mSignInCommand = new MvxCommand(this.SignIn);
				}
				return mSignInCommand;
			}
		}

		private async void SignIn()
		{
            if (View != null)
			{
				View.HideKeyboard();
			}

			LoginValidator validator = new LoginValidator();

			Login login = new Login
			{
				Email = Email,
				Password = Password,
			};

			var result = validator.Validate(login);

			if (result.IsValid)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var response = await mApiService.PostLogin(login.Email, login.Password);

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var userData = response.Data;
					mCacheService.CurrentUserData = userData;
					mApiService.UseBeaerToken = true;

					// get user key

					//mPlatformService.ShowNetworkIndicator();
					//mProgressDialogService.ShowProgressDialog();

					var response2 = await mApiService.PostSession(mPlatformService.GetDeviceUDID(), userData.UserName);

					//mPlatformService.HideNetworkIndicator();
					//mProgressDialogService.HideProgressDialog();

					if (response2.StatusCode == System.Net.HttpStatusCode.OK)
					{
						userData.UserKey = response2.Data;
						mCacheService.CurrentUserData = userData;

						await GetProfile();

						ShowViewModel<MainViewModel>();
					}
					else
					{
						mApiService.UseBeaerToken = false;
					}
				}
				else
				{
                    mPlatformService.HideNetworkIndicator();
                    mProgressDialogService.HideProgressDialog();

                    if (!string.IsNullOrEmpty(response.ErrorData))
					{
						string errorData = response.ErrorData;
						ErrorLoginResponse err = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorData);
						mMessageboxService.ShowToast(err.Description);
					}
				}
			}
			else if(result.Errors != null)
			{
				var firstError = result.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
			}
		}
		#endregion

		#region SenMyPasswordCommand
        private MvxCommand mSendMyPasswordCommand;

		public MvxCommand SendMyPasswordCommand
		{
			get
			{
				if (mSendMyPasswordCommand == null)
				{
					mSendMyPasswordCommand = new MvxCommand(this.SendMyPassword);
				}
				return mSendMyPasswordCommand;
			}
		}

		private async void SendMyPassword()
		{

			View.HideKeyboard();

			ForgotPasswordValidator validator = new ForgotPasswordValidator();

			string email = ForgotEmail;

			var result = validator.Validate(email);

			if (result.IsValid)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var response = await mApiService.PostForgotPassword(email);

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					mMessageboxService.Show("Forgot password", Messages.ForgotPasswordSuccess);
					View.HidePopupForgotPassword();
				}
				else
				{
					mMessageboxService.Show("Opps...", Messages.FotgotPasswordFail);
				}

			}
			else if(result.Errors != null)
			{
				var firstError = result.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
			}


		}
		#endregion

		#region RegisterCommand
        private MvxCommand mRegisterCommand;

		public MvxCommand RegisterCommand
		{
			get
			{
				if (mRegisterCommand == null)
				{
					mRegisterCommand = new MvxCommand(this.Register);
				}
				return mRegisterCommand;
			}
		}

		private void Register()
		{
			ShowViewModel<SignUpViewModel>();
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
			//mPlatformService.ShowNetworkIndicator();
			//mProgressDialogService.ShowProgressDialog();

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
