using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using ShopIt.Core.Messengers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using ViewModels.Items;

namespace ViewModels
{
	public interface IViewProfileView
	{
		void HidenKeyboard();

		void ShowTradeContractorCert();
		void HidenTradeContractorCert();

		void ShowIncomeInsured();
		void HidenIncomeInsured();

		void ShowFirstAid();
		void HidenFirstAid();

		void ShowGSTRegistered();
		void HidenGSTRegistered();

		void ShowOtherInformation();
		void HidenOtherInformation();

		void ShowOperationCert();
		void HidenOperationCert();

		void ShowCompanyProfile();
		void HideCompanyProfile();

		void ShowAddCompanyDetails();
		void HidenAddCompanyDetails();

		void ShowCompensation();
		void HidenCompensation();

		void ShowPublicLiability();
		void HidenPublicLiability();

		void ShowProductLiability();
		void HidenProductLiability();

		void ShowAddressCompany();
		void HidenAddressCompany();

		void ShowLicences();
		void HidenLicences();

		void ShowContructorCardExpiryDate();
		void HidenContructorCardExpiryDate();

		void ShowPerPublicLiability();
		void HidenPerPublicLiability();


	}

	public class ViewProfileViewModel : BaseViewModel
	{
		#region Constructors
        public ViewProfileViewModel	(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region View

		public IViewProfileView View { get; set; }

		#endregion

		#region Properties

		#region UserProfile
		private UserProfile mUserProfile;

		public UserProfile UserProfile
		{
			get
			{
				return mUserProfile;
			}
			set
			{
				mUserProfile = value;


				RaisePropertyChanged("DOBText");
				RaisePropertyChanged();
			}
		}
		#endregion

		#region PersonalProperties

		#region IsShowCompanyProfile
        private bool mIsShowCompanyProfile = false;

		public bool IsShowCompanyProfile
		{
			get
			{
				return mIsShowCompanyProfile;
			}
			set
			{
				mIsShowCompanyProfile = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowTradeContractorCert
		private bool mIsShowTradeContractorCert = false;

		public bool IsShowTradeContractorCert
		{
			get
			{
				return mIsShowTradeContractorCert;
			}
			set
			{
				mIsShowTradeContractorCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowIncomeInsuredCert
		private bool mIsShowIncomeInsuredCert = false;

		public bool IsShowIncomeInsuredCert
		{
			get
			{
				return mIsShowIncomeInsuredCert;
			}
			set
			{
				mIsShowIncomeInsuredCert = value;

				if (value)
				{
					View.ShowIncomeInsured();
				}
				else
				{
					View.HidenIncomeInsured();
				}

				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowContructorCardExpiry
		private bool mIsShowContructorCardExpiry;

		public bool IsShowContructorCardExpiry
		{
			get
			{
				return mIsShowContructorCardExpiry;
			}
			set
			{
				mIsShowContructorCardExpiry = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowFirstAidCert
		private bool mIsShowFirstAidCert;

		public bool IsShowFirstAidCert
		{
			get
			{
				return mIsShowFirstAidCert;
			}
			set
			{
				mIsShowFirstAidCert = value;
				if (value)
				{
					View.ShowFirstAid();
				}
				else
				{
					View.HidenFirstAid();
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowGSTRegistered
        private bool mIsShowGSTRegistered;

		public bool IsShowGSTRegistered
		{
			get
			{
				return mIsShowGSTRegistered;
			}
			set
			{
				mIsShowGSTRegistered = value;
				if (value)
				{
					View.ShowGSTRegistered();
				}
				else
				{
					View.HidenGSTRegistered();
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowOtherInformation
		private bool mIsShowOtherInformation;

		public bool IsShowOtherInformation
		{
			get
			{
				return mIsShowOtherInformation;
			}
			set
			{
				mIsShowOtherInformation = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowOperationalCert
		private bool mIsShowOperationalCert;

		public bool IsShowOperationalCert
		{
			get
			{
				return mIsShowOperationalCert;
			}
			set
			{
				mIsShowOperationalCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowAddCompanyDetails
		private bool mIsShowAddCompanyDetails;

		public bool IsShowAddCompanyDetails
		{
			get
			{
				return mIsShowAddCompanyDetails;
			}
			set
			{
				mIsShowAddCompanyDetails = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowPerPublicLiability 		private bool mIsShowPerPublicLiability;  		public bool IsShowPerPublicLiability
		{ 			get
			{ 				return mIsShowPerPublicLiability; 			} 			set
			{ 				mIsShowPerPublicLiability = value;
				if (value)
				{
					View.ShowPerPublicLiability();
				}
				else
				{
					View.HidenPerPublicLiability();
				}
				RaisePropertyChanged(); 			} 		}
		#endregion

		#region ConstructionCard
		private CertTypeItemViewModel mConstructionCardItem = new CertTypeItemViewModel("Card", false);

		public CertTypeItemViewModel ConstructionCardItem
		{
			get
			{
				return mConstructionCardItem;
			}
			set
			{
				mConstructionCardItem = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region TraceLicense
		private CertTypeItemViewModel mTraceLicenseItem = new CertTypeItemViewModel("Licence", false);

		public CertTypeItemViewModel TraceLicenseItem
		{
			get
			{
				return mTraceLicenseItem;
			}
			set
			{
				mTraceLicenseItem = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IncomeInsured
		private CertTypeItemViewModel mIncomeInsured = new CertTypeItemViewModel("Document", false);

		public CertTypeItemViewModel IncomeInsured
		{
			get
			{
				return mIncomeInsured;
			}
			set
			{
				mIncomeInsured = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region FirstAidCertified
		private CertTypeItemViewModel mFirstAidCertified = new CertTypeItemViewModel("Document", false);

		public CertTypeItemViewModel FirstAidCertified
		{
			get
			{
				return mFirstAidCertified;
			}
			set
			{
				mFirstAidCertified = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region GSTRegistered
		private CertTypeItemViewModel mGSTRegistered = new CertTypeItemViewModel("Document", false);

		public CertTypeItemViewModel GSTRegistered
		{
			get
			{
				return mGSTRegistered;
			}
			set
			{
				mGSTRegistered = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region OperationalCert
		private ObservableCollection<CertTypeItemViewModel> mOperationalCert = new ObservableCollection<CertTypeItemViewModel>();

		public ObservableCollection<CertTypeItemViewModel> OperationalCert
		{
			get
			{
				return mOperationalCert;
			}
			set
			{
				mOperationalCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region PerPublicLiabilityCert 		private CertTypeItemViewModel mPerPublicLiabilityCert = new CertTypeItemViewModel(string.Empty);  		public CertTypeItemViewModel PerPublicLiabilityCert
		{ 			get
			{ 				return mPerPublicLiabilityCert; 			} 			set
			{ 				mPerPublicLiabilityCert = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region DOBText
		public string DOBText
		{
			get
			{
				return mUserProfile == null ? "" : mUserProfile.PersonalProfile.DOB.ToLocalTime().ToString("dd/MM/yyyy");
			}
		}
		#endregion

		#region Titile
		private string mTitile;

		public string Titile
		{
			get
			{
				return mTitile;
			}
			set
			{
				mTitile = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region CompanyProperties

		#region IsShowGSTRegisterCompany
		private bool mIsShowGSTRegisterCompany;

		public bool IsShowGSTRegisterCompany
		{
			get
			{
				return mIsShowGSTRegisterCompany;
			}
			set
			{
				mIsShowGSTRegisterCompany = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowSameAsPersonal
		private bool mIsShowSameAsPersonal;

		public bool IsShowSameAsPersonal
		{
			get
			{
				return mIsShowSameAsPersonal;
			}
			set
			{
				mIsShowSameAsPersonal = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowCompensation
		private bool mIsShowCompensation = true;

		public bool IsShowCompensation
		{
			get
			{
				return mIsShowCompensation;
			}
			set
			{
				mIsShowCompensation = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowPublicLiability
		private bool mIsShowPublicLiability;

		public bool IsShowPublicLiability
		{
			get
			{
				return mIsShowPublicLiability;
			}
			set
			{
				mIsShowPublicLiability = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowProductLiability
		private bool mIsShowProductLiability;

		public bool IsShowProductLiability
		{
			get
			{
				return mIsShowProductLiability;
			}
			set
			{
				mIsShowProductLiability = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowLicences
		private bool mIsShowLicences;

		public bool IsShowLicences
		{
			get
			{
				return mIsShowLicences;
			}
			set
			{
				mIsShowLicences = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowDisplayMyBusiness
        private bool mIsShowDisplayMyBusiness;

		public bool IsShowDisplayMyBusiness
		{
			get
			{
				return mIsShowDisplayMyBusiness;
			}
			set
			{
				mIsShowDisplayMyBusiness = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region GSTRegisteredCompanyCert
		private CertTypeItemViewModel mGSTRegisteredCompanyCert = new CertTypeItemViewModel(string.Empty, false);

		public CertTypeItemViewModel GSTRegisteredCompanyCert
		{
			get
			{
				return mGSTRegisteredCompanyCert;
			}
			set
			{
				mGSTRegisteredCompanyCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CompensationCert
        private CertTypeItemViewModel mCompensationCert = new CertTypeItemViewModel(string.Empty, false);

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

		#region PublicLiability
		private CertTypeItemViewModel mPublicLiabilityCert = new CertTypeItemViewModel(string.Empty, false);

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
        private CertTypeItemViewModel mProductLiabilityCert = new CertTypeItemViewModel(string.Empty, false);

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

		#region LicencesCert
		private ObservableCollection<CertTypeItemViewModel> mLicencesCert = new ObservableCollection<CertTypeItemViewModel>();

		public ObservableCollection<CertTypeItemViewModel> LicencesCert
		{
			get
			{
				return mLicencesCert;
			}
			set
			{
				mLicencesCert = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowEditButton
		private bool mIsShowEditButton;

		public bool IsShowEditButton
		{
			get
			{
				return mIsShowEditButton;
			}
			set
			{
				mIsShowEditButton = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#endregion

		#region Init
        public async void Init(bool isThisUser, int userId)
		{
            if (mPlatformService.OS == OS.Droid)
            {
                await Task.Delay(50);
            }
			if (isThisUser)
			{
				LoadMyProfile();
				IsShowEditButton = true;
			}
			else
			{
				LoadInviteeProfile(userId);
				IsShowEditButton = false;
			}


		}
		#endregion

		#region Commands

		#region EditProfileCommand
		private MvxCommand mEditProfileCommand;

		public MvxCommand EditProfileCommand
		{
			get
			{
				if (mEditProfileCommand == null)
				{
					mEditProfileCommand = new MvxCommand(this.EditProfile);
				}
				return mEditProfileCommand;
			}

		}

		private void EditProfile()
		{
			ShowViewModel<EditProfileViewModel>();
		}
		#endregion

		#region AddCompanyDetailsCommand
        private MvxCommand mAddCompanyDetailsCommand;

		public MvxCommand AddCompanyDetailsCommand
		{
			get
			{
				if (mAddCompanyDetailsCommand == null)
				{
					mAddCompanyDetailsCommand = new MvxCommand(this.AddCompanyDetails);
				}
				return mAddCompanyDetailsCommand;
			}
		}

		private void AddCompanyDetails()
		{
			mCacheService.UserProfileItem = this.UserProfile;
			ShowViewModel<CreateCompanyProfileViewModel>();

		}
		#endregion


		#endregion

		#region Methods

		#region LoadMyProfile

		public async void LoadMyProfile()
		{
			mPlatformService.ShowNetworkIndicator();
			mProgressDialogService.ShowProgressDialog();

			var response = await mApiService.GetProfile();

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				UserProfile = response.Data;



				CheckCertForUpdateUI(UserProfile);

				//personal profile
				if (UserProfile.PersonalProfile != null)
				{
					ConstructionCardItem.CertItem = UserProfile.PersonalProfile.Certs[0];
					TraceLicenseItem.CertItem = UserProfile.PersonalProfile.Certs[1];
					IncomeInsured.CertItem = UserProfile.PersonalProfile.Certs[2];
					FirstAidCertified.CertItem = UserProfile.PersonalProfile.Certs[3];
					GSTRegistered.CertItem = UserProfile.PersonalProfile.Certs[4];
					PerPublicLiabilityCert.CertItem = UserProfile.PersonalProfile.Certs[UserProfile.PersonalProfile.Certs.Count - 1];
				}
				//company profile
				if (UserProfile.CompanyProfile != null)
				{
					CompensationCert.CertItem = UserProfile.CompanyProfile.Certs[0];
					PublicLiabilityCert.CertItem = UserProfile.CompanyProfile.Certs[1];
					ProductLiabilityCert.CertItem = UserProfile.CompanyProfile.Certs[2];
					GSTRegistered.CertItem = UserProfile.CompanyProfile.Certs[3];
				}
				//cache for edit profile
				mCacheService.UserProfileItem = UserProfile;

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

		#region LoadInviteeProfile

		public async void LoadInviteeProfile(int inviteeUserId)
		{
			mPlatformService.ShowNetworkIndicator();
			mProgressDialogService.ShowProgressDialog();

			var response = await mApiService.GetInviteeProfile(inviteeUserId);

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();



			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				UserProfile = response.Data;

				CheckCertForUpdateUI(UserProfile);

				if (UserProfile.PersonalProfile != null)
				{
					ConstructionCardItem.CertItem = UserProfile.PersonalProfile.Certs[0];
					TraceLicenseItem.CertItem = UserProfile.PersonalProfile.Certs[1];
					IncomeInsured.CertItem = UserProfile.PersonalProfile.Certs[2];
					FirstAidCertified.CertItem = UserProfile.PersonalProfile.Certs[3];
					GSTRegistered.CertItem = UserProfile.PersonalProfile.Certs[4];
					PerPublicLiabilityCert.CertItem = UserProfile.PersonalProfile.Certs[UserProfile.PersonalProfile.Certs.Count - 1];
				}

				//company profile
				if (UserProfile.CompanyProfile != null)
				{
					CompensationCert.CertItem = UserProfile.CompanyProfile.Certs[0];
					PublicLiabilityCert.CertItem = UserProfile.CompanyProfile.Certs[1];
					ProductLiabilityCert.CertItem = UserProfile.CompanyProfile.Certs[2];
					GSTRegistered.CertItem = UserProfile.CompanyProfile.Certs[3];
				}

				View.HidenAddCompanyDetails();

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

		#region CheckCertForUpdateUI

		public void CheckCertForUpdateUI(UserProfile userProfile)
		{
			#region PersonalProfile

			if (userProfile.PersonalProfile.Certs[0].Expiry == null)
			{
				IsShowContructorCardExpiry = false;
				View.HidenContructorCardExpiryDate();
			}
			else
			{
				IsShowContructorCardExpiry = true;
				View.ShowContructorCardExpiryDate();
			}

			if (userProfile.PersonalProfile.Certs[1].Document != null)
			{
				IsShowTradeContractorCert = true;
				View.ShowTradeContractorCert();
			}
			else
			{
				IsShowTradeContractorCert = false;
				View.HidenTradeContractorCert();
			}
			IsShowIncomeInsuredCert = userProfile.PersonalProfile.Certs[2].Document != null ? true : false;
			IsShowFirstAidCert = userProfile.PersonalProfile.Certs[3].Document != null ? true : false;
			IsShowGSTRegistered = userProfile.PersonalProfile.Certs[4].Ticked == true ? true : false;
			IsShowPerPublicLiability = userProfile.PersonalProfile.Certs[userProfile.PersonalProfile.Certs.Count - 1].Document != null ? true : false;

			if (IsShowIncomeInsuredCert == false && IsShowFirstAidCert == false && IsShowGSTRegistered == false && IsShowPerPublicLiability == false)
			{
				IsShowOtherInformation = false;
				View.HidenOtherInformation();
			}
			else
			{
				IsShowOtherInformation = true;
				View.ShowOtherInformation();
			}
			for (int i = 5; i < userProfile.PersonalProfile.Certs.Count - 1; i++)
			{
				if (userProfile.PersonalProfile.Certs[i].Document != null)
				{
					OperationalCert.Add(new CertTypeItemViewModel(String.Empty, false) { CertItem = userProfile.PersonalProfile.Certs[i] });
				}
			}
			if (OperationalCert.Count != 0)
			{
				IsShowOperationalCert = true;
				View.ShowOperationCert();
			}
			else
			{
				IsShowOperationalCert = false;
				View.HidenOperationCert();
			}
			#endregion

			if (userProfile.CompanyProfile.ProfileCompletionStep == ProfileCompletionStep.CPComplete)
			{
				IsShowCompanyProfile = true;
				IsShowAddCompanyDetails = false;
				View.ShowCompanyProfile();
				View.HidenAddCompanyDetails();

				#region CompanyProfile

				if (userProfile.CompanyProfile.AddressSameAs == true)
				{
					IsShowSameAsPersonal = true;
					View.HidenAddressCompany();
				}
				else
				{
					IsShowSameAsPersonal = false;
					View.ShowAddressCompany();
				}

				if (userProfile.CompanyProfile.Certs[0].Document != null)
				{
					IsShowCompensation = true;
					View.ShowCompensation();
				}
				else
				{
					IsShowCompensation = false;
					View.HidenCompensation();
				}
				if (userProfile.CompanyProfile.Certs[1].Document != null)
				{
					IsShowPublicLiability = true;
					View.ShowPublicLiability();
				}
				else
				{
					IsShowPublicLiability = false;
					View.HidenPublicLiability();
				}

				if (userProfile.CompanyProfile.Certs[2].Document != null)
				{
					IsShowProductLiability = true;
					View.ShowProductLiability();
				}
				else
				{
					IsShowProductLiability = false;
					View.HidenProductLiability();
				}

				IsShowGSTRegisterCompany = userProfile.CompanyProfile.Certs[3].Ticked == true ? true : false;
				IsShowDisplayMyBusiness = userProfile.CompanyProfile.UseBusinessName == true ? true : false;

				for (int i = 4; i < userProfile.CompanyProfile.Certs.Count; i++)
				{
					if (userProfile.CompanyProfile.Certs[i].Document != null)
					{
						LicencesCert.Add(new CertTypeItemViewModel(string.Empty, false) { CertItem = userProfile.CompanyProfile.Certs[i] });
					}
				}
				if (LicencesCert.Count != 0)
				{
					IsShowLicences = true;
					View.ShowLicences();
				}
				else
				{
					IsShowLicences = false;
					View.HidenLicences();
				}
				#endregion
			}
			else // if company profile not complete
			{
				IsShowAddCompanyDetails = true;
				IsShowCompanyProfile = false;
				View.ShowAddCompanyDetails();
				View.HideCompanyProfile();
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
					if (obj.Action.SubViewModelType == typeof(ProjectsViewModel))
					{
						//mIsProjectsNeedLoad = true;
					}
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

		#endregion
    }
}
