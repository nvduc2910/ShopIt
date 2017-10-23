using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;

namespace ShopIt.Core.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		#region Constructors
		public AboutViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}


		#endregion

		#region Properties

		#endregion

		#region Init
		public void Init()
		{
		}
		#endregion

		#region Commands

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
			ShowViewModel<LocalWebViewModel>(new { title = "Privacy Policy", fileName = "PP.html"});
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

		#endregion
	}
}
