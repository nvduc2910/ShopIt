using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Newtonsoft.Json;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using Validators;
using ViewModels.Items;

namespace ViewModels
{
	public interface IEditProfileView
	{
		void HidenKeyboard();

		void ShowTradeContractorCert();
		void HidenTradeContractorCert();


		void ShowOperationCert();
		void HidenOperationCert();

		void ShowCompanyProfile();
		void HideCompanyProfile();

		void ShowSetTimeDialog();
		void HiddenTimeDialog();


		void ShowLicences();
		void HidenLicences();

		void ShowWorksCompenstation();
		void HidenWordsCompenstation();

		void ShowPublicLiability();
		void HidenPublicLiability();

		void ShowProductLiability();
		void HidenProductLiability();


		void ShowAddress();
		void HidenAddress();

		void AddMoreOperationalCert();
		void RemoveOperationalCert();

		void AddMoreLicences();
		void RemoveLicences();

		void ShowAddWordCompenstation();
		void HiddenAddWordCompenstation();

		void ShowAddPublicLiability();
		void HiddenAddPublicLiabiliy();

		void ShowAddProductLiability();
		void HiddenAddProductLiability();

		void ShowIncomeInsured();
		void HidenIncomeInsured();

		void ShowFirstAid();
		void HidenFirstAid();

		void ShowPerPublicLiablity();
		void HidenPerPublicLiability();

		void ShowAddIncomeInsured();
		void HidenAddIncomeInsured();

		void ShowAddFirstAid();
		void HidenAddFirstAid();

		void ShowAddPerPublicLiability();
		void HidenAddPerPublicLiability();


	}


	public class EditProfileViewModel : BaseViewModel
	{
		#region Constructor
		public EditProfileViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{

		}
		#endregion

		#region IEditProjectView

		public IEditProfileView View { get; set; }

		#endregion

		#region Init

		public void Init()
		{
			var dataUserProfile = mCacheService.UserProfileItem.Clone();

			if (dataUserProfile != null)
			{
				LoadData(dataUserProfile);
			}
		}

		#endregion

		#region Properties

		#region Personal Properties

		#region Private

		private bool mIsCheckDOBSelect = false;

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

		#region ConstructionCardItem
		private CertTypeItemViewModel mConstructionCardItem = new CertTypeItemViewModel("Card");

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

		#region TraceLicenseItemViewModel
		private CertTypeItemViewModel mTraceLicenseItem = new CertTypeItemViewModel("Licence");

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
		private CertTypeItemViewModel mIncomeInsured = new CertTypeItemViewModel("Document");

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
		private CertTypeItemViewModel mFirstAidCertified = new CertTypeItemViewModel("");

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
		private CertTypeItemViewModel mGSTRegistered = new CertTypeItemViewModel("");

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

		#region PerPublicLiability
		private CertTypeItemViewModel mPerPublicLiability = new CertTypeItemViewModel(string.Empty);

		public CertTypeItemViewModel PerPublicLiability
		{
			get
			{
				return mPerPublicLiability;
			}
			set
			{
				mPerPublicLiability = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region OperationalCerts
		private MvxObservableCollection<CertTypeItemViewModel> mOperationalCerts = new MvxObservableCollection<CertTypeItemViewModel>();

		public MvxObservableCollection<CertTypeItemViewModel> OperationalCerts
		{
			get
			{
				return mOperationalCerts;
			}
			set
			{
				mOperationalCerts = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region NewOperationalCerts
		private MvxObservableCollection<CertTypeItemViewModel> mNewOperationalCerts = new MvxObservableCollection<CertTypeItemViewModel>();

		public MvxObservableCollection<CertTypeItemViewModel> NewOperationalCerts
		{
			get
			{
				return mNewOperationalCerts;
			}
			set
			{
				mNewOperationalCerts = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region FirstName
		private string mFirstName;

		public string FirstName
		{
			get
			{
				return mFirstName;
			}
			set
			{
				mFirstName = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region LastName
		private string mLastName;

		public string LastName
		{
			get
			{
				return mLastName;
			}
			set
			{
				mLastName = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Email         private string mEmail;  		public string Email
		{ 			get
			{ 				return mEmail; 			} 			set
			{ 				mEmail = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region DOB
		private DateTimeOffset mDOB;

		public DateTimeOffset DOB
		{
			get
			{
				return mDOB;
			}
			set
			{
				mDOB = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region DBOText
		private string mDOBText;

		public string DOBText
		{
			get
			{
				return mDOBText;
			}
			set
			{
				mDOBText = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region MobileNumber
		private string mMobileNumber;

		public string MobileNumber
		{
			get
			{
				return mMobileNumber;
			}
			set
			{
				mMobileNumber = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region UnitNumber
		private string mUnitNumber;

		public string UnitNumber
		{
			get
			{
				return mUnitNumber;
			}
			set
			{
				mUnitNumber = value;
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

		#region NOK
		private string mNOK;

		public string NOK
		{
			get
			{
				return mNOK;
			}
			set
			{
				mNOK = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region NOKPhone
		private string mNOKPhone;

		public string NOKPhone
		{
			get
			{
				return mNOKPhone;
			}
			set
			{
				mNOKPhone = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region PersonalProfile
		private PersonalProfile mPersonalProfile = new PersonalProfile();

		public PersonalProfile PersonalProfile
		{
			get
			{
				return mPersonalProfile;
			}
			set
			{
				mPersonalProfile = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsVerify
		private bool mIsVerify = false;

		public bool IsVerify
		{
			get
			{
				return mIsVerify;
			}
			set
			{
				mIsVerify = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsTickIncomeInsured
		private bool mIsTickIncomeInsured = true;

		public bool IsTickIncomeInsured
		{
			get
			{
				return mIsTickIncomeInsured;
			}
			set
			{
				mIsTickIncomeInsured = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsTickGSTRegistered
		private bool mIsTickGSTRegistered;

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

		#region IsTickFirstAid
		private bool mIsTickFirstAid = false;

		public bool IsTickFirstAid
		{
			get
			{
				return mIsTickFirstAid;
			}
			set
			{
				mIsTickFirstAid = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsTickPerPublicLiability
		private bool mIsTickPerPublicLiability;

		public bool IsTickPerPublicLiability
		{
			get
			{
				return mIsTickPerPublicLiability;
			}
			set
			{
				mIsTickPerPublicLiability = value;
				RaisePropertyChanged();
			}
		}
		#endregion

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

		#region IsShowOperationalCert         private bool mIsShowOperationalCert;  		public bool IsShowOperationalCert
		{ 			get
			{ 				return mIsShowOperationalCert; 			} 			set
			{ 				mIsShowOperationalCert = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region IsShowTradeContractor         private bool mIsShowTradeContractor;  		public bool IsShowTradeContractor
		{ 			get
			{ 				return mIsShowTradeContractor; 			} 			set
			{ 				mIsShowTradeContractor = value;
				if (value)
				{
					View.ShowTradeContractorCert();
				}
				else
				{
					View.HidenTradeContractorCert();
				} 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region AddTradeContructorText         private string mAddTradeContructorText;  		public string AddTradeContructorText
		{ 			get
			{ 				return mAddTradeContructorText; 			} 			set
			{ 				mAddTradeContructorText = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#endregion

		#region Company Properties

		#region CPBusinessName         private string mCPBusinessName;  		public string CPBusinessName
		{ 			get
			{ 				return mCPBusinessName; 			} 			set
			{ 				mCPBusinessName = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region CPABN         private string mCPABN;  		public string CPABN
		{ 			get
			{ 				return mCPABN; 			} 			set
			{ 				mCPABN = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region CPPositionJobTitle         private string mCPPositionJobTitle;  		public string CPPositionJobTitle
		{ 			get
			{ 				return mCPPositionJobTitle; 			} 			set
			{ 				mCPPositionJobTitle = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region CPUnitNumber         private string mCPUnitNumber;  		public string CPUnitNumber
		{ 			get
			{ 				return mCPUnitNumber; 			} 			set
			{ 				mCPUnitNumber = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region CPStreetName         private string mCPStreetName;  		public string CPStreetName
		{ 			get
			{ 				return mCPStreetName; 			} 			set
			{ 				mCPStreetName = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region CPPostCode         private string mCPPostCode;  		public string CPPostCode
		{ 			get
			{ 				return mCPPostCode; 			} 			set
			{ 				mCPPostCode = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region CPState         private string mCPState;  		public string CPState
		{ 			get
			{ 				return mCPState; 			} 			set
			{ 				mCPState = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region IsTickGSTRegisterCompany
		private bool mIsTickGSTRegistedCompany;

		public bool IsTickGSTRegistedCompany
		{
			get
			{
				return mIsTickGSTRegistedCompany;
			}
			set
			{
				mIsTickGSTRegistedCompany = value;
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
				if (mIsShowSameAsPersonal == true)
				{
					View.HidenAddress();
				}
				else
				{
					View.ShowAddress();
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowCompensation
		private bool mIsTickCompensation = true;

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

		#region IsShowPublicLiability
		private bool mIsTickPublicLiability;

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

		#region IsShowProductLiability
		private bool mIsTickProductLiability;

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

		#region IsCPVerify         private bool mIsCPVerify;  		public bool IsCPVerify
		{ 			get
			{ 				return mIsCPVerify; 			} 			set
			{ 				mIsCPVerify = value; 				RaisePropertyChanged(); 			} 		}
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
		private CertTypeItemViewModel mGSTRegisteredCompanyCert = new CertTypeItemViewModel(string.Empty, true);

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
		private CertTypeItemViewModel mCompensationCert = new CertTypeItemViewModel(string.Empty, true);

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
		private CertTypeItemViewModel mPublicLiabilityCert = new CertTypeItemViewModel(string.Empty, true);

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
		private CertTypeItemViewModel mProductLiabilityCert = new CertTypeItemViewModel(string.Empty, true);

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

		#region NewLicences         private ObservableCollection<CertTypeItemViewModel> mNewLicences = new ObservableCollection<CertTypeItemViewModel>();  		public ObservableCollection<CertTypeItemViewModel> NewLicences
		{ 			get
			{ 				return mNewLicences; 			} 			set
			{ 				mNewLicences = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region CompanyData         private Company mCompanyProfile;  		public Company CompanyProfile
		{ 			get
			{ 				return mCompanyProfile; 			} 			set
			{ 				mCompanyProfile = value; 				RaisePropertyChanged(); 			} 		}
		#endregion


		#endregion

		#endregion

		#region Commands

		#region CheckIncomeInsuredCommand
		private MvxCommand mCheckIncomeInsuredCommand;

		public MvxCommand CheckIncomeInsuredCommand
		{
			get
			{
				if (mCheckIncomeInsuredCommand == null)
				{
					mCheckIncomeInsuredCommand = new MvxCommand(this.CheckIncomeInsured);
				}
				return mCheckIncomeInsuredCommand;
			}
		}

		private void CheckIncomeInsured()
		{
			if (IsTickIncomeInsured == true)
			{
				if (IncomeInsured.CertItem.Document != null)
				{
					IsTickIncomeInsured = false;
					IncomeInsured.CertItem.Ticked = false;
					View.HidenIncomeInsured();
					return;
				}
				else
				{
					IsTickIncomeInsured = false;
					IncomeInsured.CertItem.Ticked = false;
					View.HidenAddIncomeInsured();
					return;
				}
			}
			else if (IsTickIncomeInsured == false)
			{
				if (IncomeInsured.CertItem.Document != null)
				{
					IsTickIncomeInsured = true;
					IncomeInsured.CertItem.Ticked = true;
					View.ShowIncomeInsured();
					return;
				}
				else
				{
					IsTickIncomeInsured = true;
					IncomeInsured.CertItem.Ticked = true;
					View.ShowAddIncomeInsured();
					return;
				}
			}
		}
		#endregion

		#region CheckFirstAidCommand
		private MvxCommand mCheckFirstAidCommand;

		public MvxCommand CheckFirstAidCommand
		{
			get
			{
				if (mCheckFirstAidCommand == null)
				{
					mCheckFirstAidCommand = new MvxCommand(this.CheckFirstAid);
				}
				return mCheckFirstAidCommand;
			}
		}

		private void CheckFirstAid()
		{
			if (IsTickFirstAid == true)
			{
				if (FirstAidCertified.CertItem.Document != null)
				{
					IsTickFirstAid = false;
					FirstAidCertified.CertItem.Ticked = false;
					View.HidenFirstAid();
					return;
				}
				else
				{
					IsTickFirstAid = false;
					FirstAidCertified.CertItem.Ticked = false;
					View.HidenAddFirstAid();
					return;
				}
			}
			else if (IsTickFirstAid == false)
			{
				if (FirstAidCertified.CertItem.Document != null)
				{
					IsTickFirstAid = true;
					FirstAidCertified.CertItem.Ticked = true;
					View.ShowFirstAid();
					return;
				}
				else
				{
					IsTickFirstAid = true;
					FirstAidCertified.CertItem.Ticked = true;
					View.ShowAddFirstAid();
					return;
				}
			}
		}
		#endregion

		#region CheckGSTRegisteredCommand         private MvxCommand mCheckGSTRegisteredCommand;  		public MvxCommand CheckGSTRegisteredCommand
		{ 			get
			{ 				if (mCheckGSTRegisteredCommand == null)
				{ 					mCheckGSTRegisteredCommand = new MvxCommand(this.CheckGSTRegistered); 				} 				return mCheckGSTRegisteredCommand; 			} 		}  		private void CheckGSTRegistered()
		{
			if (IsTickGSTRegistered == true)
			{
				IsTickGSTRegistered = false;
				GSTRegistered.CertItem.Ticked = false;
				return;
			}
			else if (IsTickGSTRegistered == false)
			{
				IsTickGSTRegistered = true;
				GSTRegistered.CertItem.Ticked = true;
				return;
			} 		}
		#endregion

		#region CheckPerPublicLiablityCommand
		private MvxCommand mCheckPerPublicLiablityCommand;

		public MvxCommand CheckPerPublicLiablityCommand
		{
			get
			{
				if (mCheckPerPublicLiablityCommand == null)
				{
					mCheckPerPublicLiablityCommand = new MvxCommand(this.CheckPerPublicLiablity);
				}
				return mCheckPerPublicLiablityCommand;
			}
		}

		private void CheckPerPublicLiablity()
		{

			if (IsTickPerPublicLiability == true)
			{
				if (PerPublicLiability.CertItem.Document != null)
				{
					IsTickPerPublicLiability = false;
					PerPublicLiability.CertItem.Ticked = false;
					View.HidenPerPublicLiability();
					return;
				}
				else
				{
					IsTickPerPublicLiability = false;
					PerPublicLiability.CertItem.Ticked = false;
					View.HidenAddPerPublicLiability();
					return;
				}
			}
			else if (IsTickPerPublicLiability == false)
			{
				if (PerPublicLiability.CertItem.Document != null)
				{
					IsTickPerPublicLiability = true;
					PerPublicLiability.CertItem.Ticked = true;
					View.ShowPerPublicLiablity();
					return;
				}
				else
				{
					IsTickPerPublicLiability = true;
					PerPublicLiability.CertItem.Ticked = true;
					View.ShowAddPerPublicLiability();
					return;
				}
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
			if (mIsCheckDOBSelect == false)
			{

				mEdittingTimeItem.CertItem.Expiry = dateTime;
				//RaisePropertyChanged("")
				mEdittingTimeItem.ExpiryText = dateTime.ToString();
			}
			else if (mIsCheckDOBSelect == true)
			{
				View.HidenKeyboard();
				DOB = dateTime;
				DOBText = dateTime.ToString().Split(' ')[0];
				mIsCheckDOBSelect = false;
			}
			View.HiddenTimeDialog();
		}
		#endregion

		#region DOBCommand
		private MvxCommand mDOBCommand;

		public MvxCommand DOBCommand
		{
			get
			{
				if (mDOBCommand == null)
				{
					mDOBCommand = new MvxCommand(this.DOBChoose);
				}
				return mDOBCommand;
			}
		}

		private void DOBChoose()
		{
			View.HidenKeyboard();
			mIsCheckDOBSelect = true;
			View.ShowSetTimeDialog();
		}
		#endregion

		#region TapToAddOperationalCertCommand
		private MvxCommand mTapToAddOperationalCertCommand;

		public MvxCommand TapToAddOperationalCertCommand
		{
			get
			{
				if (mTapToAddOperationalCertCommand == null)
				{
					mTapToAddOperationalCertCommand = new MvxCommand(this.TapToAddOperationalCert);
				}
				return mTapToAddOperationalCertCommand;
			}
		}

		private void TapToAddOperationalCert()
		{
			NewOperationalCerts.Add(new CertTypeItemViewModel("OPERATIONAL CERTIFICATES") { EditProfileVM = this });
			NewOperationalCerts[NewOperationalCerts.Count - 1].CertItem.CertType = mCacheService.UserProfileItem.PersonalProfile.Certs[6].CertType;
			NewOperationalCerts[NewOperationalCerts.Count - 1].CertItem.CertTypeId = 6;
			NewOperationalCerts[NewOperationalCerts.Count - 1].CertItem.UserId = mCacheService.CurrentUserData.UserId;
			View.AddMoreOperationalCert();
		}
		#endregion

		#region VerifyCommand
		private MvxCommand mVerifyCommand;

		public MvxCommand VerifyCommand
		{
			get
			{
				if (mVerifyCommand == null)
				{
					mVerifyCommand = new MvxCommand(this.Verify);
				}
				return mVerifyCommand;
			}
		}

		private void Verify()
		{
			if (IsVerify == false)
			{
				IsVerify = true;
				PersonalProfile.StatementAgree = DateTime.Now;
				return;
			}
			if (IsVerify == true)
			{
				IsVerify = false;
				PersonalProfile.StatementAgree = null;
				return;
			}
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
				View.HidenKeyboard();
				UpdatePersonalProfile();
			}
			catch (Exception ex)
			{
#if DEBUG
				mMessageboxService.Show("Error", ex.Message);
#endif
			}

		}
		#endregion

		#region CheckGSTRegisteredCompanyCommand         private MvxCommand mCheckGSTRegisteredCompanyCommand;  		public MvxCommand CheckGSTRegisteredCompanyCommand
		{ 			get
			{ 				if (mCheckGSTRegisteredCompanyCommand == null)
				{ 					mCheckGSTRegisteredCompanyCommand = new MvxCommand(this.CheckGSTRegisteredCompany); 				} 				return mCheckGSTRegisteredCompanyCommand; 			} 		}  		private void CheckGSTRegisteredCompany()
		{
			if (IsTickGSTRegistedCompany == true)
			{
				IsTickGSTRegistedCompany = false;
				GSTRegisteredCompanyCert.CertItem.Ticked = false;
				return;
			}
			else if (IsTickGSTRegistedCompany == false)
			{
				IsTickGSTRegistedCompany = true;
				GSTRegisteredCompanyCert.CertItem.Ticked = true;
				return;
			} 		}
		#endregion


		#region CheckCompensationCommand         private MvxCommand mCheckCompensationCommand;  		public MvxCommand CheckCompensationCommand
		{ 			get
			{ 				if (mCheckCompensationCommand == null)
				{ 					mCheckCompensationCommand = new MvxCommand(this.CheckCompensation); 				} 				return mCheckCompensationCommand; 			} 		}  		private void CheckCompensation()
		{
			if (IsTickCompensation == true)
			{
				if (CompensationCert.CertItem.Document != null)
				{
					IsTickCompensation = false;
					CompensationCert.CertItem.Ticked = false;
					View.HidenWordsCompenstation();
					return;
				}
				else
				{
					IsTickCompensation = false;
					CompensationCert.CertItem.Ticked = false;
					View.HiddenAddWordCompenstation();
					return;
				}
			}
			else if (IsTickCompensation == false)
			{
				if (CompensationCert.CertItem.Document != null)
				{
					IsTickCompensation = true;
					CompensationCert.CertItem.Ticked = true;
					View.ShowWorksCompenstation();
					return;
				}
				else
				{
					IsTickCompensation = true;
					CompensationCert.CertItem.Ticked = true;
					View.ShowAddWordCompenstation();
					return;
				}
			}
 		}
		#endregion

		#region CheckPublicLiabilityCommand         private MvxCommand mCheckPublicLiabilityCommand;  		public MvxCommand CheckPublicLiabilityCommand
		{ 			get
			{ 				if (mCheckPublicLiabilityCommand == null)
				{ 					mCheckPublicLiabilityCommand = new MvxCommand(this.CheckPublicLiability); 				} 				return mCheckPublicLiabilityCommand; 			} 		}  		private void CheckPublicLiability()
		{

			if (IsTickPublicLiability == true)
			{
				if (PublicLiabilityCert.CertItem.Document != null)
				{
					IsTickPublicLiability = false;
					View.HidenPublicLiability();
					PublicLiabilityCert.CertItem.Ticked = false;
					return;
				}
				else
				{
					IsTickPublicLiability = false;
					PublicLiabilityCert.CertItem.Ticked = false;
					View.HiddenAddPublicLiabiliy();
					return;
				}
			}
			else if (IsTickPublicLiability == false)
			{
				if (PublicLiabilityCert.CertItem.Document != null)
				{
					IsTickPublicLiability = true;
					View.ShowPublicLiability();
					PublicLiabilityCert.CertItem.Ticked = true;
					return;
				}
				else
				{
					IsTickPublicLiability = true;
					PublicLiabilityCert.CertItem.Ticked = true;
					View.ShowAddPublicLiability();
					return;
				}
			} 		}
		#endregion

		#region CheckProductLiabilityCommand         private MvxCommand mCheckProductLiabilityCommand;  		public MvxCommand CheckProductLiabilityCommand
		{ 			get
			{ 				if (mCheckProductLiabilityCommand == null)
				{ 					mCheckProductLiabilityCommand = new MvxCommand(this.CheckProductLiability); 				} 				return mCheckProductLiabilityCommand; 			} 		}  		private void CheckProductLiability()
		{
			if (IsTickProductLiability == true)
			{
				if (ProductLiabilityCert.CertItem.Document != null)
				{
					IsTickProductLiability = false;
					View.HidenProductLiability();
					ProductLiabilityCert.CertItem.Ticked = false;
					return;
				}
				else
				{
					IsTickProductLiability = false;
					ProductLiabilityCert.CertItem.Ticked = false;
					View.HiddenAddProductLiability();
					return;
				}
			}
			else if (IsTickProductLiability == false)
			{
				if (ProductLiabilityCert.CertItem.Document != null)
				{
					IsTickProductLiability = true;
					View.ShowProductLiability();
					ProductLiabilityCert.CertItem.Ticked = true;
					return;
				}
				else
				{
					IsTickProductLiability = true;
					ProductLiabilityCert.CertItem.Ticked = true;
					View.ShowAddProductLiability();
					return;
				}
			} 		}
		#endregion

		#region CheckSameAsAddressCommand         private MvxCommand mCheckSameAsAddressCommand;  		public MvxCommand CheckSameAsAddressCommand
		{ 			get
			{ 				if (mCheckSameAsAddressCommand == null)
				{ 					mCheckSameAsAddressCommand = new MvxCommand(this.CheckSameAsAddress); 				} 				return mCheckSameAsAddressCommand; 			} 		}  		private void CheckSameAsAddress()
		{
			if (IsShowSameAsPersonal == true)
			{
				IsShowSameAsPersonal = false;
				CompanyProfile.AddressSameAs = false;
				return;
			}
			else if (IsShowSameAsPersonal == false)
			{
				IsShowSameAsPersonal = true;
				CompanyProfile.AddressSameAs = true;
				return;
			} 		}
		#endregion

		#region CheckDisplayMyBusinessCommand         private MvxCommand mCheckDisplayMyBusinessCommand;  		public MvxCommand CheckDisplayMyBusinessCommand
		{ 			get
			{ 				if (mCheckDisplayMyBusinessCommand == null)
				{ 					mCheckDisplayMyBusinessCommand = new MvxCommand(this.CheckDisplayMyBusiness); 				} 				return mCheckDisplayMyBusinessCommand; 			} 		}  		private void CheckDisplayMyBusiness()
		{
			if (IsShowDisplayMyBusiness == true)
			{
				IsShowDisplayMyBusiness = false;
				return;
			}
			else if (IsShowDisplayMyBusiness == false)
			{
				IsShowDisplayMyBusiness = true;
				return;
			} 		}
		#endregion

		#region CheckVerifyCompanyCommand         private MvxCommand mCheckVerifyCompanyCommand;  		public MvxCommand CheckVerifyCompanyCommand
		{ 			get
			{ 				if (mCheckVerifyCompanyCommand == null)
				{ 					mCheckVerifyCompanyCommand = new MvxCommand(this.CheckVerifyCompany); 				} 				return mCheckVerifyCompanyCommand; 			} 		}  		private void CheckVerifyCompany()
		{
			if (IsCPVerify == true)
			{
				IsCPVerify = false;
				return;
			}
			else if (IsCPVerify == false)
			{
				IsCPVerify = true;
				return;
			}
 		}
		#endregion

		#region TapToAddLicencesCommand         private MvxCommand mTapToAddLicencesCommand;  		public MvxCommand TapToAddLicencesCommand
		{ 			get
			{ 				if (mTapToAddLicencesCommand == null)
				{ 					mTapToAddLicencesCommand = new MvxCommand(this.TapToAddLicences); 				} 				return mTapToAddLicencesCommand; 			} 		}  		private void TapToAddLicences()
		{
			NewLicences.Add(new CertTypeItemViewModel("LICENCES") { EditProfileVM = this });
			NewLicences[NewLicences.Count - 1].CertItem.CertType = mCacheService.UserProfileItem.CompanyProfile.Certs[4].CertType;
			NewLicences[NewLicences.Count - 1].CertItem.CertTypeId = 10;
			NewLicences[NewLicences.Count - 1].CertItem.UserId = mCacheService.CurrentUserData.UserId;
			View.AddMoreLicences(); 		}
		#endregion



		#endregion

		#region Method

		#region InitData

		public void LoadData(UserProfile userProfile)
		{

			//personal profile

			PersonalProfile = userProfile.PersonalProfile;
			FirstName = userProfile.PersonalProfile.FirstName;
			LastName = userProfile.PersonalProfile.LastName;
			Email = userProfile.PersonalProfile.Email;
			DOB = userProfile.PersonalProfile.DOB;
			DOBText = userProfile.PersonalProfile.DOB.ToString().Split(' ')[0];
			MobileNumber = userProfile.PersonalProfile.Mobile;
			UnitNumber = userProfile.PersonalProfile.BulidingNo;
			StreetName = userProfile.PersonalProfile.Street;
			PostCode = userProfile.PersonalProfile.PostCode;
			State = userProfile.PersonalProfile.State;
			NOK = userProfile.PersonalProfile.NOK;
			NOKPhone = userProfile.PersonalProfile.NOKPhone;

			ConstructionCardItem.EditProfileVM = this;
			TraceLicenseItem.EditProfileVM = this;
			IncomeInsured.EditProfileVM = this;
			FirstAidCertified.EditProfileVM = this;
			GSTRegistered.EditProfileVM = this;
			PerPublicLiability.EditProfileVM = this;

			ConstructionCardItem.CertItem = userProfile.PersonalProfile.Certs[0];
			ConstructionCardItem.IsCanedit = true;

			TraceLicenseItem.CertItem = userProfile.PersonalProfile.Certs[1];
			TraceLicenseItem.IsCanedit = true;


			IncomeInsured.CertItem = userProfile.PersonalProfile.Certs[2];
			IncomeInsured.IsCanedit = true;

			FirstAidCertified.CertItem = userProfile.PersonalProfile.Certs[3];
			FirstAidCertified.IsCanedit = true;

			GSTRegistered.CertItem = userProfile.PersonalProfile.Certs[4];


			PerPublicLiability.CertItem = userProfile.PersonalProfile.Certs[userProfile.PersonalProfile.Certs.Count - 1];
			PerPublicLiability.IsCanedit = true;

			//company profile
			CompanyProfile = userProfile.CompanyProfile;

			CPBusinessName = userProfile.CompanyProfile.BusinessName;
			CPABN = userProfile.CompanyProfile.ABN;
			CPPositionJobTitle = userProfile.CompanyProfile.JobTitle;
			CPUnitNumber = userProfile.CompanyProfile.BulidingNo;
			CPStreetName = userProfile.CompanyProfile.Street;
			CPPostCode = userProfile.CompanyProfile.PostCode;
			CPState = userProfile.CompanyProfile.State;

			CompensationCert.CertItem = userProfile.CompanyProfile.Certs[0];
			PublicLiabilityCert.CertItem = userProfile.CompanyProfile.Certs[1];
			ProductLiabilityCert.CertItem = userProfile.CompanyProfile.Certs[2];
			GSTRegisteredCompanyCert.CertItem = userProfile.CompanyProfile.Certs[3];


			CompensationCert.EditProfileVM = this;
			PublicLiabilityCert.EditProfileVM = this;
			ProductLiabilityCert.EditProfileVM = this;
			GSTRegisteredCompanyCert.EditProfileVM = this;

			CheckCertForUpdateUI(userProfile);
		}
		#endregion

		#region CheckCertForUpdateUI

		public async Task CheckCertForUpdateUI(UserProfile userProfile)
		{

			while (View == null)
			{
				await Task.Delay(150);
			}
			//var userProfile = mCacheService.UserProfileItem.Clone();

			if (userProfile != null)
			{

				//Check UI for personal profile
				IsTickIncomeInsured = userProfile.PersonalProfile.Certs[2].Document != null ? true : false;
				IsTickFirstAid = userProfile.PersonalProfile.Certs[3].Document != null ? true : false;
				IsTickGSTRegistered = userProfile.PersonalProfile.Certs[4].Ticked == true ? true : false;
				IsTickPerPublicLiability = userProfile.PersonalProfile.Certs[userProfile.PersonalProfile.Certs.Count - 1].Ticked == true ? true : false;

				IsVerify = userProfile.PersonalProfile.StatementAgree == null ? false : true;
				IsShowTradeContractor = userProfile.PersonalProfile.Certs[1].Document != null ? true : false;
				if (IsTickIncomeInsured == true)
				{
					View.ShowIncomeInsured();
				}
				else
				{
					View.HidenIncomeInsured();
				}

				if (IsTickFirstAid == true)
				{
					View.ShowFirstAid();
				}
				else
				{
					View.HidenFirstAid();
				}
				if (IsTickPerPublicLiability == true)
				{
					View.ShowPerPublicLiablity();
				}
				else
				{
					View.HidenPerPublicLiability();
				}
				for (int i = 5; i < userProfile.PersonalProfile.Certs.Count - 1; i++)
				{
					if (userProfile.PersonalProfile.Certs[i].Document != null)
					{
						OperationalCerts.Add(new CertTypeItemViewModel(String.Empty, true) { CertItem = userProfile.PersonalProfile.Certs[i], EditProfileVM = this });
					}
				}
				if (OperationalCerts.Count != 0)
				{
					IsShowOperationalCert = true;
					//View.ShowOperationCert();
				}
				else
				{
					IsShowOperationalCert = false;
					//View.HidenOperationCert();
				}

				//Check UI for company profile
				if (userProfile.CompanyProfile.ProfileCompletionStep != ProfileCompletionStep.CPComplete)
				{
					IsShowCompanyProfile = false;
					View.HideCompanyProfile();
				}
				else
				{
					IsShowCompanyProfile = true;
					IsShowSameAsPersonal = userProfile.CompanyProfile.AddressSameAs == true ? true : false;
					IsTickCompensation = userProfile.CompanyProfile.Certs[0].Document != null ? true : false;
					IsTickPublicLiability = userProfile.CompanyProfile.Certs[1].Document != null ? true : false;
					IsTickProductLiability = userProfile.CompanyProfile.Certs[2].Document != null ? true : false;

					if (IsTickCompensation == true)
					{
						View.ShowWorksCompenstation();
					}
					else
					{
						View.HidenWordsCompenstation();
					}

					if (IsTickPublicLiability == true)
					{
						View.ShowPublicLiability();
					}
					else
					{
						View.HidenPublicLiability();
					}

					if (IsTickProductLiability == true)
					{
						View.ShowProductLiability();
					}
					else
					{
						View.HidenProductLiability();
					}

					IsTickGSTRegistedCompany = userProfile.CompanyProfile.Certs[3].Ticked == true ? true : false;
					IsShowDisplayMyBusiness = userProfile.CompanyProfile.UseBusinessName == true ? true : false;
					IsCPVerify = userProfile.CompanyProfile.StatementAgree == null ? false : true;

					View.ShowCompanyProfile();

					for (int i = 4; i < userProfile.CompanyProfile.Certs.Count; i++)
					{
						if (userProfile.CompanyProfile.Certs[i].Document != null)
						{
							LicencesCert.Add(new CertTypeItemViewModel(string.Empty, true) { CertItem = userProfile.CompanyProfile.Certs[i], EditProfileVM = this });
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
				}
			}
		}
		#endregion

		public async void UpdatePersonalProfile()
		{
			if (ValidatorPersonalProfile() != true)
			{
				return;
			}

			if (CompanyProfile.ProfileCompletionStep == ProfileCompletionStep.CPComplete)
			{
				if (ValidatorCompanyProfile() != true)
				{
					return;
				}
			}

			if (IsTickIncomeInsured != true)
			{
				IncomeInsured.CertItem.Document = null;
				IncomeInsured.CertItem.Expiry = null;
			}
			if (IsTickFirstAid != true)
			{
				FirstAidCertified.CertItem.Document = null;
				FirstAidCertified.CertItem.Expiry = null;
			}

			if (IsTickPerPublicLiability != true)
			{
				PerPublicLiability.CertItem.Document = null;
				PerPublicLiability.CertItem.Expiry = null;
			}
			PersonalProfile.Certs[1] = TraceLicenseItem.CertItem;
			PersonalProfile.Certs[2] = IncomeInsured.CertItem;
			PersonalProfile.Certs[3] = FirstAidCertified.CertItem;
			PersonalProfile.Certs[4] = GSTRegistered.CertItem;
			PersonalProfile.Certs[PersonalProfile.Certs.Count - 1] = PerPublicLiability.CertItem;

			PersonalProfile.StatementAgree = DateTime.Now;


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
			Debug.WriteLine("Upload photo done");

			for (int i = 0; i < OperationalCerts.Count; i++)
			{
				PersonalProfile.Certs[i + 5] = OperationalCerts[i].CertItem;
			}

			for (int k = 0; k < NewOperationalCerts.Count; k++)
			{
				PersonalProfile.Certs.Add(NewOperationalCerts[k].CertItem);
			}

			var response = await mApiService.UpdatePersonalProfile(PersonalProfile);

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				if (CompanyProfile.ProfileCompletionStep == ProfileCompletionStep.CPComplete)
				{
					UpdateCompanyProfile();
				}
				else
				{
					mMessageboxService.ShowToast("Update Profile Sccessful");
					BackToViewModel<MainViewModel>();
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(response.ErrorData))
				{
					string errorData = response.ErrorData;
					ErrorLoginResponse err = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorData);
					mMessageboxService.ShowToast(err.Description);
				}
			}
		}

		public async void UpdateCompanyProfile()
		{
			if (IsTickPublicLiability != true)
			{
				PublicLiabilityCert.CertItem.Amount = null;
				PublicLiabilityCert.CertItem.Document = null;
				PublicLiabilityCert.CertItem.Expiry = null;
			}
			if (IsTickPublicLiability != true)
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

			CompanyProfile.Certs[0] = CompensationCert.CertItem;
			CompanyProfile.Certs[1] = PublicLiabilityCert.CertItem;
			CompanyProfile.Certs[2] = ProductLiabilityCert.CertItem;
			CompanyProfile.Certs[3] = GSTRegisteredCompanyCert.CertItem;

			CompanyProfile.StatementAgree = DateTime.Now;
			CompanyProfile.UseBusinessName = IsShowDisplayMyBusiness;

			for (int j = 0; j < LicencesCert.Count; j++)
			{
				CompanyProfile.Certs[j + 5] = LicencesCert[j].CertItem;
			}

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
			Debug.WriteLine("Upload photo done");

			for (int l = 0; l < NewLicences.Count; l++)
			{
				CompanyProfile.Certs.Add(NewLicences[l].CertItem);
			}


			var response = await mApiService.UpdateCompanyProfile(CompanyProfile);

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				mMessageboxService.ShowToast("Update Profile Sccessful");
				BackToViewModel<MainViewModel>();
			}
			else
			{
				if (!string.IsNullOrEmpty(response.ErrorData))
				{
					string errorData = response.ErrorData;
					ErrorLoginResponse err = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorData);
					mMessageboxService.ShowToast(err.Description);
				}
			}
		}

		public bool ValidatorPersonalProfile()
		{
			PersonalProfile.FirstName = FirstName;
			PersonalProfile.LastName = LastName;
			PersonalProfile.Email = Email;
			PersonalProfile.DOB = DOB;
			PersonalProfile.Mobile = MobileNumber;
			PersonalProfile.BulidingNo = UnitNumber;
			PersonalProfile.Street = StreetName;
			PersonalProfile.State = State;
			PersonalProfile.PostCode = PostCode;
			PersonalProfile.NOK = NOK;
			PersonalProfile.NOKPhone = NOKPhone;

			PersonalProfileValidator validatorConstructionCard = new PersonalProfileValidator();
			var result1 = validatorConstructionCard.Validate(PersonalProfile);
			if (!result1.IsValid)
			{
				var firstError = result1.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}

			CertValidator validatorPersonalProfile = new CertValidator();
			var result2 = validatorPersonalProfile.Validate(ConstructionCardItem);
			if (!result2.IsValid)
			{
				var firstError = result2.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}
			PersonalProfile.Certs[0] = ConstructionCardItem.CertItem;


			CertValidator validatorTraceLicense = new CertValidator();
			var result3 = validatorTraceLicense.Validate(TraceLicenseItem);
			if (!result3.IsValid)
			{
				var firstError = result3.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}


			CertValidator validatorIncomeInsuredCert = new CertValidator();
			var result5 = validatorIncomeInsuredCert.Validate(IncomeInsured);

			if (!result5.IsValid)
			{
				var firstError = result5.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}

			CertValidator validatorFirstAidCert = new CertValidator();
			var result6 = validatorFirstAidCert.Validate(FirstAidCertified);

			if (!result6.IsValid)
			{
				var firstError = result6.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}

			for (int i = 0; i < NewOperationalCerts.Count; i++)
			{
				CertValidator validatorOperationalCert = new CertValidator();
				var result4 = validatorOperationalCert.Validate(NewOperationalCerts[i]);

				if (!result4.IsValid)
				{
					var firstError = result4.Errors.First();
					mMessageboxService.ShowToast(firstError.ErrorMessage);
					return false;
				}
			}
			if (PersonalProfile.StatementAgree == null)
			{
				mMessageboxService.ShowToast("Please verify this is information is true");
				return false;
			}
			return true;

		}

		public bool ValidatorCompanyProfile()
		{
			CompanyProfile.BusinessName = CPBusinessName;
			CompanyProfile.ABN = CPABN;
			CompanyProfile.JobTitle = CPPositionJobTitle;
			CompanyProfile.BulidingNo = CPUnitNumber;
			CompanyProfile.Street = CPStreetName;
			CompanyProfile.PostCode = CPPostCode;
			CompanyProfile.State = CPState;

			CompanyProfileValidator validatorStep1 = new CompanyProfileValidator();
			var result0 = validatorStep1.Validate(mCompanyProfile);
			if (!result0.IsValid)
			{
				var firstError = result0.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}

			CertValidator validatorPublicLiability = new CertValidator();
			var result1 = validatorPublicLiability.Validate(PublicLiabilityCert);
			if (!result1.IsValid)
			{
				var firstError = result1.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}

			CertValidator validatorProductLiability = new CertValidator();
			var result2 = validatorProductLiability.Validate(ProductLiabilityCert);
			if (!result2.IsValid)
			{
				var firstError = result2.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}

			CertValidator validatorWorksCompenstation = new CertValidator();
			var result4 = validatorWorksCompenstation.Validate(CompensationCert);
			if (!result4.IsValid)
			{
				var firstError = result4.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
				return false;
			}


			for (int i = 0; i < NewLicences.Count; i++)
			{
				CertValidator validatorOperationalCert = new CertValidator();
				var result3 = validatorOperationalCert.Validate(NewLicences[i]);

				if (!result3.IsValid)
				{
					var firstError = result3.Errors.First();
					mMessageboxService.ShowToast(firstError.ErrorMessage);
					return false;
				}
			}

			if (IsCPVerify == false)
			{
				mMessageboxService.ShowToast("Please verify this is information is true ");
				return false;
			}

			return true;
		}

		#endregion
	}

}
