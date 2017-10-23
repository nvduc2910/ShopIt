using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using Services;
using ShopIt.Core.Messengers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using Validators;
using ViewModels.Items;

namespace ViewModels
{

	public interface ICompanyProfileView
	{
		//void ShowNextStep();

		void ShowAddGSTRegistered();
		void HidenAddGSTRegisterd();

		void ShowAddCompensationCert();
		void HiddenAddCompensationCert();

		void ShowAddPublicLiabilityCert();
		void HidenAddPublicLiabilityCert();

		void ShowAddProductLiabilityCert();
		void HidenAddProductLiabilityCert();

		void ShowAddMoreLicencesCert();

		void ShowSetTimeDialog();
		void HiddenTimeDialog();

		void ShowAddBusinessAddress();
		void HidenBusinessAddress();

		void ScrollToBottom();

		void PanToPage(int page);
		void HideKeyboard();
	}

	public class CreateCompanyProfileViewModel : BaseViewModel
	{
		#region Constructors
        public CreateCompanyProfileViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			 : base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{

		}
		#endregion

		#region ICompanyProfileView

		public ICompanyProfileView View { get; set; }

		#endregion

		#region Properties

		#region CompanyProfile
		private Company mCompanyProfile = new Company();
		#endregion

		#region TitleStep
		private string mTitleStep = "Step 1 of 2";

		public string TitleStep
		{
			get
			{
				return mTitleStep;
			}
			set
			{
				mTitleStep = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region TitleButtonNext

		private string mTitleButtonNext = "Next";

		public string TitleButtonNext
		{
			get
			{
				return mTitleButtonNext;
			}
			set
			{
				mTitleButtonNext = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CurrentPage
		private int mCurrentPage = 1;

		public int CurrentPage
		{
			get
			{
				return mCurrentPage;
			}
			set
			{
				mCurrentPage = value;

				if (CurrentPage < 2)
				{
					mTitleStep = "Step " + mCurrentPage.ToString() + " of 2";
					mTitleButtonNext = "Next";
					RaisePropertyChanged("TitleButtonNext");
					RaisePropertyChanged("TitleStep");
				}
				else if (CurrentPage == 2)
				{
					mTitleStep = "Step " + mCurrentPage.ToString() + " of 2";
					RaisePropertyChanged("TitleStep");
					mTitleButtonNext = "Done";
					RaisePropertyChanged("TitleButtonNext");
				}

				RaisePropertyChanged();

			}
		}
		#endregion

		#region BusinessName
		private string mBusinessName;

		public string BusinessName
		{
			get
			{
				return mBusinessName;
			}
			set
			{
				mBusinessName = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ABN
		private string mABN;

		public string ABN
		{
			get
			{
				return mABN;
			}
			set
			{
				mABN = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region JobPosition
		private string mJobPosition;

		public string JobPosition
		{
			get
			{
				return mJobPosition;
			}
			set
			{
				mJobPosition = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Unit
		private string mUnit;

		public string Unit
		{
			get
			{
				return mUnit;
			}
			set
			{
				mUnit = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region StreetName
		private string mStreetName;

		public string StreetName
		{
			get
			{
				return mStreetName;
			}
			set
			{
				mStreetName = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region PostCode
		private string mPostCode;

		public string PostCode
		{
			get
			{
				return mPostCode;
			}
			set
			{
				mPostCode = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region State
		private string mState;

		public string State
		{
			get
			{
				return mState;
			}
			set
			{
				mState = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsCheckVerify
		private bool mIsCheckVerify = false;

		public bool IsCheckVerify
		{
			get
			{
				return mIsCheckVerify;
			}
			set
			{
				mIsCheckVerify = value;
				RaisePropertyChanged();
			}
		}
		#endregion


		#region IsCheckDisplay
		private bool mIsCheckDisplay = false;

		public bool IsCheckDisplay
		{
			get
			{
				return mIsCheckDisplay;
			}
			set
			{
				mIsCheckDisplay = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowAddCompensation
		private bool mIsTickCompensation = false;

		public bool IsTickCompensation
		{
			get
			{
				return mIsTickCompensation;
			}
			set
			{
				mIsTickCompensation = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowAddPublicLiability
		private bool mIsTickPublicLiability = false;

		public bool IsTickPublicLiability
		{
			get
			{
				return mIsTickPublicLiability;
			}
			set
			{
				mIsTickPublicLiability = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowAddProductLiability
		private bool mIsTickProductLiability = false;

		public bool IsTickProductLiability
		{
			get
			{
				return mIsTickProductLiability;
			}
			set
			{
				mIsTickProductLiability = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsCheckSameAsPersonal
		private bool mIsTickSameAsPersonal = false;

		public bool IsTickSameAsPersonal
		{
			get
			{
				return mIsTickSameAsPersonal;
			}
			set
			{
				mIsTickSameAsPersonal = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowGSTRegistered
		private bool mIsTickGSTRegistered = false;

		public bool IsTickGSTRegistered
		{
			get
			{
				return mIsTickGSTRegistered;
			}
			set
			{
				mIsTickGSTRegistered = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CompensationCert
        private CertTypeItemViewModel mCompensationCert = new CertTypeItemViewModel("Compensation");

		public CertTypeItemViewModel CompensationCert
		{
			get
			{
				return mCompensationCert;
			}
			set
			{
				mCompensationCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region PublicLiabilityCert
		private CertTypeItemViewModel mPublicLiabilityCert = new CertTypeItemViewModel("PublicLiability");

		public CertTypeItemViewModel PublicLiabilityCert
		{
			get
			{
				return mPublicLiabilityCert;
			}
			set
			{
				mPublicLiabilityCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ProductLiabilityCert
		private CertTypeItemViewModel mProductLiabilityCert = new CertTypeItemViewModel("ProductLiability");

		public CertTypeItemViewModel ProductLiabilityCert
		{
			get
			{
				return mProductLiabilityCert;
			}
			set
			{
				mProductLiabilityCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region GSTRegisteredCert
		private CertTypeItemViewModel mGSTRegisteredCert = new CertTypeItemViewModel("GSTRegistered");

		public CertTypeItemViewModel GSTRegisteredCert
		{
			get
			{
				return mGSTRegisteredCert;
			}
			set
			{
				mGSTRegisteredCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region LicenceCerts
		private MvxObservableCollection<CertTypeItemViewModel> mLicenceCerts = new MvxObservableCollection<CertTypeItemViewModel>();

		public MvxObservableCollection<CertTypeItemViewModel> LicenceCerts
		{
			get
			{
				return mLicenceCerts;
			}
			set
			{
				mLicenceCerts = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region EdittingCardItem
		private CertTypeItemViewModel mEdittingTimeItem = new CertTypeItemViewModel(null);

		public CertTypeItemViewModel EdittingTimeItem
		{
			get
			{
				return mEdittingTimeItem;
			}
			set
			{
				mEdittingTimeItem = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
			var companyProfile = mCacheService.UserProfileItem.CompanyProfile;

			this.mCompanyProfile = companyProfile;

			if (companyProfile != null)
			{
				CompensationCert.CreateCompanyProfileVM = this;
				PublicLiabilityCert.CreateCompanyProfileVM = this;
				ProductLiabilityCert.CreateCompanyProfileVM = this;
				GSTRegisteredCert.CreateCompanyProfileVM = this;
				EdittingTimeItem.CreateCompanyProfileVM = this;



				CompensationCert.CertItem = companyProfile.Certs[0];
				CompensationCert.CertItem.Ticked = false;

				PublicLiabilityCert.CertItem = companyProfile.Certs[1];
				//PublicLiabilityCert.CertItem.Ticked = false;

				ProductLiabilityCert.CertItem = companyProfile.Certs[2];
				//ProductLiabilityCert.CertItem.Ticked = false;

				GSTRegisteredCert.CertItem = companyProfile.Certs[3];
				GSTRegisteredCert.CertItem.Ticked = false;

				mCompanyProfile.UseBusinessName = false;
				mCompanyProfile.AddressSameAs = false;

				LicenceCerts.Add(new CertTypeItemViewModel("Licence") { CreateCompanyProfileVM = this,});
				LicenceCerts[LicenceCerts.Count - 1].CertItem.CertType = mCompanyProfile.Certs[4].CertType;
				LicenceCerts[LicenceCerts.Count - 1].CertItem.CertTypeId = 10;
				LicenceCerts[LicenceCerts.Count - 1].CertItem.UserId = mCacheService.CurrentUserData.UserId;

			}
		}
		#endregion

		#region Commands

		#region ShowNextCommand
        private MvxCommand mShowNextCommand;

		public MvxCommand ShowNextCommand
		{
			get
			{
				if (mShowNextCommand == null)
				{
					mShowNextCommand = new MvxCommand(this.ShowNext);
				}
				return mShowNextCommand;
			}
		}

		private void ShowNext()
		{
			View.HideKeyboard();
			if (CurrentPage < 3)
			{
				CurrentPage++;
			}
			if (CurrentPage == 2)
			{
				
				mCompanyProfile.BusinessName = BusinessName;
				mCompanyProfile.ABN = ABN;
				mCompanyProfile.JobTitle = JobPosition;

				if (IsTickSameAsPersonal == true)
				{
					mCompanyProfile.AddressSameAs = true;
				}
				else
				{
					mCompanyProfile.AddressSameAs = false;
					mCompanyProfile.Street = StreetName;
					mCompanyProfile.State = State;
					mCompanyProfile.PostCode = PostCode;
					mCompanyProfile.BulidingNo = Unit;
				}

				CompanyProfileValidator validatorCompanyStep1 = new CompanyProfileValidator();
				var result1 = validatorCompanyStep1.Validate(mCompanyProfile);
				if (!result1.IsValid)
				{
					CurrentPage -= 1;
					var firstError = result1.Errors.First();
					mMessageboxService.ShowToast(firstError.ErrorMessage);
					return;
				}

				View.PanToPage(CurrentPage);
			}
			if (CurrentPage == 3)
			{
				Save();
			}

		}
		#endregion

		#region ShowBackStepCommand
		private MvxCommand mShowBackStepCommand;

		public MvxCommand ShowBackStepCommand
		{
			get
			{
				if (mShowBackStepCommand == null)
				{
					mShowBackStepCommand = new MvxCommand(this.ShowBackStep);
				}
				return mShowBackStepCommand;
			}
		}

		private void ShowBackStep()
		{
			if (CurrentPage >= 1)
			{
				CurrentPage -= 1;
			}

			if (CurrentPage == 0) // Back to previ view.
			{
				GoBackCommand.Execute();
			}
			else if (CurrentPage == 1)
			{
				View.PanToPage(CurrentPage);

			}
		}
		#endregion

		#region TickGSTRegisteredCommand
        private MvxCommand mTickGSTRegisteredCommand;

		public MvxCommand TickGSTRegisteredCommand
		{
			get
			{
				if (mTickGSTRegisteredCommand == null)
				{
					mTickGSTRegisteredCommand = new MvxCommand(this.TickGSTRegistered);
				}
				return mTickGSTRegisteredCommand;
			}
		}

		private void TickGSTRegistered()
		{
			if (IsTickGSTRegistered == true)
			{
				IsTickGSTRegistered = false;
				GSTRegisteredCert.CertItem.Ticked = false;
				//View.HidenAddGSTRegisterd();
				return;
			}
			else if(IsTickGSTRegistered == false)
			{
				IsTickGSTRegistered = true;
				GSTRegisteredCert.CertItem.Ticked = true;
				//View.ShowAddGSTRegistered();
				return;
			}
		}
		#endregion

		#region TickCompensationCommand
        private MvxCommand mTickCompensationCommand;

		public MvxCommand TickCompensationCommand
		{
			get
			{
				if (mTickCompensationCommand == null)
				{
					mTickCompensationCommand = new MvxCommand(this.TickCompensation);
				}
				return mTickCompensationCommand;
			}
		}

		private void TickCompensation()
		{
			if (IsTickCompensation == true)
			{
				IsTickCompensation = false;
				CompensationCert.CertItem.Ticked = false;
				View.HiddenAddCompensationCert();
				return;
			}
			else if (IsTickCompensation == false)
			{
				IsTickCompensation = true;
				CompensationCert.CertItem.Ticked = true;
				View.ShowAddCompensationCert();
				return;
			}
		}
		#endregion

		#region TickPublicLiabilityCommand
        private MvxCommand mTickPublicLiabilityCommand;

		public MvxCommand TickPublicLiabilityCommand
		{
			get
			{
				if (mTickPublicLiabilityCommand == null)
				{
					mTickPublicLiabilityCommand = new MvxCommand(this.TickPublicLiability);
				}
				return mTickPublicLiabilityCommand;
			}
		}

		private void TickPublicLiability()
		{
			if (IsTickPublicLiability == true)
			{
				IsTickPublicLiability = false;
				PublicLiabilityCert.CertItem.Ticked = false;
				View.HidenAddPublicLiabilityCert();
				return;
			}
			else if(IsTickPublicLiability == false)
			{
				IsTickPublicLiability = true;
				PublicLiabilityCert.CertItem.Ticked = true;
				View.ShowAddPublicLiabilityCert();
				return;
			}
		}
		#endregion

		#region TickProductLiabilityCommand
        private MvxCommand mTickProductLiabilityCommand;

		public MvxCommand TickProductLiabilityCommand
		{
			get
			{
				if (mTickProductLiabilityCommand == null)
				{
					mTickProductLiabilityCommand = new MvxCommand(this.TickProductLiability);
				}
				return mTickProductLiabilityCommand;
			}
		}

		private void TickProductLiability()
		{
			if (IsTickProductLiability == true)
			{
				IsTickProductLiability = false;
				ProductLiabilityCert.CertItem.Ticked = false;
				View.HidenAddProductLiabilityCert();
				return;
			}
			else if (IsTickProductLiability == false)
			{
				IsTickProductLiability = true;
				ProductLiabilityCert.CertItem.Ticked = true;
				View.ShowAddProductLiabilityCert();
				return;
			}
				
		}
		#endregion

		#region TickAddressSameAsCommand
		private MvxCommand mTickAddressSameAsCommand;

		public MvxCommand TickAddressSameAsCommand
		{
			get
			{
				if (mTickAddressSameAsCommand == null)
				{
					mTickAddressSameAsCommand = new MvxCommand(this.TickAddressSameAs);
				}
				return mTickAddressSameAsCommand;
			}
		}

		private void TickAddressSameAs()
		{
			if (IsTickSameAsPersonal == false)
			{
				IsTickSameAsPersonal = true;
				View.HidenBusinessAddress();
				return;
			}
			else
			{
				IsTickSameAsPersonal = false;
				View.ShowAddBusinessAddress();
				return;
			}
		}
		#endregion

		#region TapAddMoreLicenceCommand
		private MvxCommand mTapAddMoreLicenceCommand;

		public MvxCommand TapAddMoreLicenceCommand
		{
			get
			{
				if (mTapAddMoreLicenceCommand == null)
				{
					mTapAddMoreLicenceCommand = new MvxCommand(this.TapAddMoreLicence);
				}
				return mTapAddMoreLicenceCommand;
			}
		}

		private void TapAddMoreLicence()
		{
			LicenceCerts.Add(new CertTypeItemViewModel("Licence") { CreateCompanyProfileVM = this });
			LicenceCerts[LicenceCerts.Count - 1].CertItem.CertType = mCompanyProfile.Certs[4].CertType;
			LicenceCerts[LicenceCerts.Count - 1].CertItem.CertTypeId = 11;
			LicenceCerts[LicenceCerts.Count - 1].CertItem.UserId = mCacheService.CurrentUserData.UserId;
			View.ShowAddMoreLicencesCert();
		}
		#endregion

		#region TickDisplayCommand
		private MvxCommand mTickDisplayCommand;

		public MvxCommand TickDisplayCommand
		{
			get
			{
				if (mTickDisplayCommand == null)
				{
					mTickDisplayCommand = new MvxCommand(this.TickDisplay);
				}
				return mTickDisplayCommand;
			}
		}

		private void TickDisplay()
		{
			if (IsCheckDisplay == false)
			{
				IsCheckDisplay = true;
				mCompanyProfile.UseBusinessName = true;
				return;
			}
			else if (IsCheckDisplay == true)
			{
				IsCheckDisplay = false;
				mCompanyProfile.UseBusinessName = false;
				return;
			}
		}
		#endregion

		#region TickVerifyCommand
		private MvxCommand mTickVerifyCommand;

		public MvxCommand TickVerifyCommand
		{
			get
			{
				if (mTickVerifyCommand == null)
				{
					mTickVerifyCommand = new MvxCommand(this.TickVerify);
				}
				return mTickVerifyCommand;
			}
		}

		private void TickVerify()
		{
			if (IsCheckVerify == false)
			{
				IsCheckVerify = true;
				mCompanyProfile.StatementAgree = DateTime.Now;
				return;
			}
			else if (IsCheckVerify == true)
			{
				IsCheckVerify = false;
				mCompanyProfile.StatementAgree = null;
				return;
			}
		}
		#endregion

		#region SelectedTimeCommand
		private MvxCommand<DateTime> mSelectedTimeCommand;

		public MvxCommand<DateTime> SelectedTimeCommand
		{
			get
			{
				if (mSelectedTimeCommand == null)
				{
					mSelectedTimeCommand = new MvxCommand<DateTime>(this.SelectedTime);
				}
				return mSelectedTimeCommand;
			}
		}

		private void SelectedTime(DateTime dateTime)
		{

			mEdittingTimeItem.CertItem.Expiry = dateTime;
			mEdittingTimeItem.ExpiryText = dateTime.ToString();
			View.HiddenTimeDialog();

		}
		#endregion

		#region SaveCommand
		private MvxCommand mSaveCommand;

		public MvxCommand SaveCommand
		{
			get
			{
				if (mSaveCommand == null)
				{
					mSaveCommand = new MvxCommand(this.Save);
				}
				return mSaveCommand;
			}
		}

		private async void Save()
		{
			try
			{
				CertValidator validatorWorkCompenstation = new CertValidator();
				var result3 = validatorWorkCompenstation.Validate(CompensationCert);
				if (!result3.IsValid)
				{
					var firstError = result3.Errors.First();
					mMessageboxService.ShowToast(firstError.ErrorMessage);
					return;
				}
				
				CertValidator validatorPublicLiability = new CertValidator();
				var result = validatorPublicLiability.Validate(PublicLiabilityCert);
				if (!result.IsValid)
				{
					var firstError = result.Errors.First();
					mMessageboxService.ShowToast(firstError.ErrorMessage);
					return;
				}

				CertValidator validatorProductLiability = new CertValidator();
				var result1 = validatorProductLiability.Validate(ProductLiabilityCert);
				if (!result1.IsValid)
				{
					var firstError = result1.Errors.First();
					mMessageboxService.ShowToast(firstError.ErrorMessage);
					return;
				}


				for (int j = 0; j < LicenceCerts.Count; j++)
				{
					CertValidator validatorLicences2 = new CertValidator();
					var result2 = validatorLicences2.Validate(LicenceCerts[j]);
					if (!result2.IsValid)
					{
						var firstError = result2.Errors.First();
						mMessageboxService.ShowToast(firstError.ErrorMessage);
						return;
					}
				}
				if (IsCheckVerify == false)
				{
					mMessageboxService.ShowToast("Please verify this is information is true ");
					View.ScrollToBottom();
					return;
				}
				if (IsTickPublicLiability != true)
				{
					PublicLiabilityCert.CertItem.Amount = null;
					PublicLiabilityCert.CertItem.Document = null;
					PublicLiabilityCert.CertItem.Expiry = null;
				}
				if (IsTickProductLiability != true)
				{
					ProductLiabilityCert.CertItem.Amount = null;
					ProductLiabilityCert.CertItem.Document = null;
					ProductLiabilityCert.CertItem.Expiry = null;
				}
				if (IsTickCompensation != true)
				{
					CompensationCert.CertItem.Document = null;
					CompensationCert.CertItem.Expiry = null;
				}
				mCompanyProfile.Certs[0] = CompensationCert.CertItem;
				mCompanyProfile.Certs[1] = PublicLiabilityCert.CertItem;
				mCompanyProfile.Certs[2] = ProductLiabilityCert.CertItem;
				mCompanyProfile.Certs[3] = GSTRegisteredCert.CertItem;

	 			for (int i = 0; i < LicenceCerts.Count; i++)
				{
					mCompanyProfile.Certs.Add(LicenceCerts[i].CertItem);
				}
				mCompanyProfile.ProfileCompletionStep = ProfileCompletionStep.CPComplete;
				mCompanyProfile.StatementAgree = DateTime.Now;

				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				while (!Mvx.Resolve<ITaskManagementService>().IsAllTaskDone())
				{
					int numOfImages = Mvx.Resolve<ITaskManagementService>().NumberOfTasksRunning();
					if (numOfImages > 0)
					{
						string uploadStr = numOfImages + (numOfImages > 1 ? " images " : " image ") + "processing";
						mProgressDialogService.ShowProgressDialog(uploadStr);
					}
					else
					{
						mProgressDialogService.ShowProgressDialog("");
					}
					await Task.Delay(150);
				}

				var response = await mApiService.CreateCompanyProfile(mCompanyProfile);

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					mCacheService.IsShowProfileView = true;
					mMessageboxService.ShowToast("Company Profile Complete");
					Mvx.Resolve<ITrackingService>().SendEvent("Company Profile completed");
					ViewModelAction action = new ViewModelAction
					{
						ActionType = ViewModelActionType.Reload,
						ViewModelType = typeof(ProfileViewModel),
						Data = "COMPLETED_PROFILE"
					};
					Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action));
					
					BackToViewModel<MainViewModel>();
				}
				else
				{
					RefreshListLicences();
					if (!string.IsNullOrEmpty(response.ErrorData))
					{
						string errorData = response.ErrorData;
						ErrorLoginResponse err = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorData);
						mMessageboxService.ShowToast(err.Description);
					}
				}
			}
			catch (Exception ex)
			{
				RefreshListLicences();
				mMessageboxService.Show("Error", ex.Message);
			}
		}
		#endregion

		#endregion

		#region Methods

		public void RefreshListLicences()
		{
			for (int i = 4; i < mCompanyProfile.Certs.Count; i++)
			{
				mCompanyProfile.Certs.Remove(mCompanyProfile.Certs[i]);	
			}
		}

		#endregion
	}
}
