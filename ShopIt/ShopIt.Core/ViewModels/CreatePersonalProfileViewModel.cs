using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using Services;
using ShopIt.Core.Helpers;
using ShopIt.Core.Messengers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using ShopIt.Core.ViewModels.Items;
using Validators;
using ViewModels.Items;

namespace ViewModels
{
	public enum CertTypeEnum
	{
		Card,
		License,
		Document,
		Optional,
	}

	public interface IPersonalProfileView
	{
		void ShowPopupFinish();
		void HiddenPopupFinish();
		void PanToPage(int page);
		void AddMoreOperationalCert();
		void ShowSetTimeDialog();
		void HiddenTimeDialog();
		void HideKeyboard();
		void ScrollToBottom();
		byte[] GetByteImage(string path);

		void ShowIncomeInsured();
		void HidenIncomeInsured();

		void ShowFirstAid();
		void HidenFirstAid();

		void ShowPublicLiability();
		void HidenPublicLiability();
	}

	public class CreatePersonalProfileViewModel : BaseViewModel
	{
		#region Constructors
		public CreatePersonalProfileViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			 : base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{

		}
		#endregion

		#region IPersonalProfileView

		public IPersonalProfileView View { get; set; }

		#endregion

		#region Properties

		#region Private
		private PersonalProfile mPersonalProfileLocal = new PersonalProfile();
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
		private CertTypeItemViewModel mConstructionCardItem = new CertTypeItemViewModel("Construction Induction Card");

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
		private CertTypeItemViewModel mTraceLicenseItem = new CertTypeItemViewModel("Trade Contractor Licence");

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
		private CertTypeItemViewModel mIncomeInsured = new CertTypeItemViewModel("Income Insured");

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
		private CertTypeItemViewModel mFirstAidCertified = new CertTypeItemViewModel(String.Empty);

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
		private CertTypeItemViewModel mGSTRegistered = new CertTypeItemViewModel(string.Empty);

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

		#region PublicLiablityCert 		private CertTypeItemViewModel mPublicLiablityCert = new CertTypeItemViewModel(string.Empty);  		public CertTypeItemViewModel PublicLiablityCert
		{ 			get
			{ 				return mPublicLiablityCert; 			} 			set
			{ 				mPublicLiablityCert = value; 				RaisePropertyChanged(); 			} 		}
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

		#region IsStep1
		private bool mIsStep1 = true;

		public bool IsStep1
		{
			get
			{
				return mIsStep1;
			}
			set
			{
				mIsStep1 = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsStep2
		private bool mIsStep2 = false;

		public bool IsStep2
		{
			get
			{
				return mIsStep2;
			}
			set
			{
				mIsStep2 = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsStep3
		private bool mIsStep3 = false;

		public bool IsStep3
		{
			get
			{
				return mIsStep3;
			}
			set
			{
				mIsStep3 = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsStep4
		private bool mIsStep4 = false;

		public bool IsStep4
		{
			get
			{
				return mIsStep4;
			}
			set
			{
				mIsStep4 = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region TitleStep
		private string mTitleStep = "Step 1 of 4";

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

				if (CurrentPage < 4)
				{
					mTitleStep = "Step " + mCurrentPage.ToString() + " of 4";
					mTitleButtonNext = "Next";
					RaisePropertyChanged("TitleButtonNext");
					RaisePropertyChanged("TitleStep");
				}
				else if (CurrentPage == 4)
				{
					mTitleStep = "Step " + mCurrentPage.ToString() + " of 4";
					RaisePropertyChanged("TitleStep");
					mTitleButtonNext = "Done";
					RaisePropertyChanged("TitleButtonNext");
				}
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

		#region DOBTextTemp
        private string mDOBTextTemp = "DOB";

		public string DOBTextTemp
		{
			get
			{
				return mDOBTextTemp;
			}
			set
			{
				mDOBTextTemp = value;
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
				if (mDOBText != null || mDOBText != string.Empty)
				{
					DOBTextTemp = string.Empty;
				}
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
		private bool mIsTickIncomeInsured;

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

		#region IsTickPublicLiability 		private bool mIsTickPublicLiability;  		public bool IsTickPublicLiability
		{ 			get
			{ 				return mIsTickPublicLiability; 			} 			set
			{ 				mIsTickPublicLiability = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#endregion

		#region Init
		public async void Init()
		{

			this.UserProfile = DataHelper.RetrieveFromUserPref<UserProfile>("UserProfile");
			if (UserProfile.PersonalProfile != null)
			{
				ConstructionCardItem.CreatePersonalProfileVM = this;
				TraceLicenseItem.CreatePersonalProfileVM = this;
				IncomeInsured.CreatePersonalProfileVM = this;
				FirstAidCertified.CreatePersonalProfileVM = this;
				GSTRegistered.CreatePersonalProfileVM = this;
				PublicLiablityCert.CreatePersonalProfileVM = this;

				ConstructionCardItem.CertItem = UserProfile.PersonalProfile.Certs[0];
				TraceLicenseItem.CertItem = UserProfile.PersonalProfile.Certs[1];
				IncomeInsured.CertItem = UserProfile.PersonalProfile.Certs[2];
				FirstAidCertified.CertItem = UserProfile.PersonalProfile.Certs[3];
				GSTRegistered.CertItem = UserProfile.PersonalProfile.Certs[4];

				OperationalCerts.Add(new CertTypeItemViewModel("Operational Certificates") { CreatePersonalProfileVM = this });
				OperationalCerts[OperationalCerts.Count - 1].CertItem.CertType = UserProfile.PersonalProfile.Certs[5].CertType;
				OperationalCerts[OperationalCerts.Count - 1].CertItem.CertTypeId = UserProfile.PersonalProfile.Certs[5].CertTypeId;
				OperationalCerts[OperationalCerts.Count - 1].CertItem.UserId = mCacheService.CurrentUserData.UserId;

				PublicLiablityCert.CertItem = UserProfile.PersonalProfile.Certs[6];

				var personalProfile = DataHelper.RetrieveFromUserPref<PersonalProfile>("PERSONAL_STEP");

				while (View == null)
				{
					await Task.Delay(150);
				}
				if (personalProfile != null)
				{
					ShowCompetedStep();
				}
			}
		}
		#endregion

		#region Commands


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
				HightlightStep(true, false, false, false);
			}
			else if (CurrentPage == 2)
			{
				View.PanToPage(CurrentPage);
				HightlightStep(true, true, false, false);

			}
			else if (CurrentPage == 3)
			{
				View.PanToPage(CurrentPage);
				HightlightStep(true, true, true, false);
			}

		}
		#endregion

		#region CloseCommand
		private MvxCommand mCloseCommand;

		public MvxCommand CloseCommand
		{
			get
			{
				if (mCloseCommand == null)
				{
					mCloseCommand = new MvxCommand(this.Close);
				}
				return mCloseCommand;
			}
		}

		private void Close()
		{
			//ShowViewModel<MainViewModel>();
			ViewModelAction action = new ViewModelAction
			{
				ActionType = ViewModelActionType.Reload,
				ViewModelType = typeof(ProfileViewModel),
				Data = "COMPLETED_PROFILE"
			};
			Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action));

			Close(this);
		}
		#endregion

		#region CreateCompanyProfileCommand
		private MvxCommand mCreateCompanyProfileCommand;

		public MvxCommand CreateCompanyProfileCommand
		{
			get
			{
				if (mCreateCompanyProfileCommand == null)
				{
					mCreateCompanyProfileCommand = new MvxCommand(this.CreateCompanyProfile);
				}
				return mCreateCompanyProfileCommand;
			}
		}

		private void CreateCompanyProfile()
		{
			ShowViewModel<CreateCompanyProfileViewModel>();
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
			OperationalCerts.Add(new CertTypeItemViewModel("OPERATIONAL CERTIFICATES") { CreatePersonalProfileVM = this });
			OperationalCerts[OperationalCerts.Count - 1].CertItem.CertType = UserProfile.PersonalProfile.Certs[5].CertType;
			OperationalCerts[OperationalCerts.Count - 1].CertItem.CertTypeId = UserProfile.PersonalProfile.Certs[5].CertTypeId;
			OperationalCerts[OperationalCerts.Count - 1].CertItem.UserId = mCacheService.CurrentUserData.UserId;
			OperationalCerts[OperationalCerts.Count - 1].IsShowOrignalImage = true;
			View.AddMoreOperationalCert();
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
			View.HideKeyboard();
			mIsCheckDOBSelect = true;
			View.ShowSetTimeDialog();
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
				mEdittingTimeItem.ExpiryText = dateTime.ToString();
			}
			else if (mIsCheckDOBSelect == true)
			{
				DOB = dateTime;
				DOBText = dateTime.ToString().Split(' ')[0];
				mIsCheckDOBSelect = false;
			}

			View.HiddenTimeDialog();

		}
		#endregion

		#region TickIncomeInsuredCommand
		private MvxCommand mTickIncomeInsuredCommand;

		public MvxCommand TickIncomeInsuredCommand
		{
			get
			{
				if (mTickIncomeInsuredCommand == null)
				{
					mTickIncomeInsuredCommand = new MvxCommand(this.TickIncomeInsured);
				}
				return mTickIncomeInsuredCommand;
			}
		}

		private void TickIncomeInsured()
		{
			if (IsTickIncomeInsured == true)
			{
				IsTickIncomeInsured = false;
				IncomeInsured.CertItem.Ticked = false;
				View.HidenIncomeInsured();
				return;
			}
			else if (IsTickIncomeInsured == false)
			{
				IsTickIncomeInsured = true;
				IncomeInsured.CertItem.Ticked = true;
				View.ShowIncomeInsured();
				return;
			}
		}
		#endregion

		#region TickFirstAidCertCommand
		private MvxCommand mTickFirstAidCertCommand;

		public MvxCommand TickFirstAidCertCommand
		{
			get
			{
				if (mTickFirstAidCertCommand == null)
				{
					mTickFirstAidCertCommand = new MvxCommand(this.TickFirstAidCert);
				}
				return mTickFirstAidCertCommand;
			}
		}

		private void TickFirstAidCert()
		{
			if (IsTickFirstAid == true)
			{
				IsTickFirstAid = false;
				FirstAidCertified.CertItem.Ticked = false;
				View.HidenFirstAid();
				return;
			}
			else if (IsTickFirstAid == false)
			{
				IsTickFirstAid = true;
				FirstAidCertified.CertItem.Ticked = true;
				View.ShowFirstAid();
				return;
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
				GSTRegistered.CertItem.Ticked = false;
				return;
			}
			else if (IsTickGSTRegistered == false)
			{
				IsTickGSTRegistered = true;
				GSTRegistered.CertItem.Ticked = true;
				return;
			}
		}
		#endregion

		#region TickPublicLiabilityCommand         private MvxCommand mTickPublicLiabilityCommand;  		public MvxCommand TickPublicLiabilityCommand
		{ 			get
			{ 				if (mTickPublicLiabilityCommand == null)
				{ 					mTickPublicLiabilityCommand = new MvxCommand(this.TickPublicLiability); 				} 				return mTickPublicLiabilityCommand; 			} 		}  		private void TickPublicLiability()
		{
			if (IsTickPublicLiability == true)
			{
				IsTickPublicLiability = false;
				PublicLiablityCert.CertItem.Ticked = false;
				View.HidenPublicLiability();
				return;
			}
			else if (IsTickPublicLiability == false)
			{
				IsTickPublicLiability = true;
				PublicLiablityCert.CertItem.Ticked = true;
				View.ShowPublicLiability();
				return;
			} 		}
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
				UserProfile.PersonalProfile.StatementAgree = DateTime.Now;
				return;
			}
			if (IsVerify == true)
			{
				IsVerify = false;
				UserProfile.PersonalProfile.StatementAgree = null;
				return;
			}
		}
		#endregion

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

		private async void ShowNext()
		{
			try
			{
				View.HideKeyboard();
				if (CurrentPage < 5)
				{
					CurrentPage += 1;

				}
				var personalProfileTemp = DataHelper.RetrieveFromUserPref<PersonalProfile>("PERSONAL_STEP");

				if (personalProfileTemp != null)
				{
					mPersonalProfileLocal = personalProfileTemp;
				}
				if (CurrentPage == 2)
				{
					mPersonalProfileLocal.FirstName = FirstName;
					mPersonalProfileLocal.LastName = LastName;
					mPersonalProfileLocal.DOB = DOB;
					mPersonalProfileLocal.Mobile = MobileNumber;
					mPersonalProfileLocal.BulidingNo = UnitNumber;
					mPersonalProfileLocal.Street = StreetName;
					mPersonalProfileLocal.PostCode = PostCode;
					mPersonalProfileLocal.State = State;
					mPersonalProfileLocal.NOK = NOK;
					mPersonalProfileLocal.NOKPhone = NOKPhone;

					PersonalProfileValidator validatorPersonalProfile = new PersonalProfileValidator();
					var result = validatorPersonalProfile.Validate(mPersonalProfileLocal);
					if (!result.IsValid)
					{
						CurrentPage -= 1;
						var firstError = result.Errors.First();
						mMessageboxService.ShowToast(firstError.ErrorMessage);
						return;
					}
					View.PanToPage(CurrentPage);
					HightlightStep(true, true, false, false);

					if (personalProfileTemp != null)
					{
						mPersonalProfileLocal.ProfileCompletionStep = personalProfileTemp.ProfileCompletionStep;
					}
					else
					{
						mPersonalProfileLocal.ProfileCompletionStep = ProfileCompletionStep.PPStep1;
					}

				}
				else if (CurrentPage == 3)
				{
					CertValidator validatorConstructionCard = new CertValidator();
					var result = validatorConstructionCard.Validate(ConstructionCardItem);
					if (!result.IsValid)
					{
						CurrentPage -= 1;
						var firstError = result.Errors.First();
						mMessageboxService.ShowToast(firstError.ErrorMessage);
						return;
					}

					View.PanToPage(CurrentPage);
					HightlightStep(true, true, true, false);

					if (personalProfileTemp != null)
					{
						if (personalProfileTemp.Certs.Count > 0)
						{
							mPersonalProfileLocal.ProfileCompletionStep = personalProfileTemp.ProfileCompletionStep;
							mPersonalProfileLocal.Certs[0] = ConstructionCardItem.CertItem;
						}
						else
						{
							mPersonalProfileLocal.ProfileCompletionStep = ProfileCompletionStep.PPStep2;

							mPersonalProfileLocal.Certs.Add(ConstructionCardItem.CertItem);
						}
					}
					else
					{
						mPersonalProfileLocal.ProfileCompletionStep = ProfileCompletionStep.PPStep2;
						mPersonalProfileLocal.Certs.Add(ConstructionCardItem.CertItem);
					}
				}
				else if (CurrentPage == 4)
				{
					CertValidator validatorTraceLicense = new CertValidator();
					var result = validatorTraceLicense.Validate(TraceLicenseItem);
					if (!result.IsValid)
					{
						CurrentPage -= 1;
						var firstError = result.Errors.First();
						mMessageboxService.ShowToast(firstError.ErrorMessage);
						return;
					}

					View.PanToPage(CurrentPage);
					HightlightStep(true, true, true, true);
					if (personalProfileTemp != null)
					{
						if (personalProfileTemp.Certs.Count > 1)
						{
							mPersonalProfileLocal.ProfileCompletionStep = personalProfileTemp.ProfileCompletionStep;
							mPersonalProfileLocal.Certs[1] = TraceLicenseItem.CertItem;
						}
						else
						{
							mPersonalProfileLocal.ProfileCompletionStep = ProfileCompletionStep.PPStep3;
							mPersonalProfileLocal.Certs.Add(TraceLicenseItem.CertItem);
						}
					}
					else
					{
						mPersonalProfileLocal.ProfileCompletionStep = ProfileCompletionStep.PPStep3;
						mPersonalProfileLocal.Certs.Add(TraceLicenseItem.CertItem);
					}
				}

				DataHelper.SaveToUserPref(mPersonalProfileLocal, "PERSONAL_STEP");


				if (CurrentPage == 5) // Done button
				{
					CertValidator validatorIncomeInsuredCert = new CertValidator();
					var result4 = validatorIncomeInsuredCert.Validate(IncomeInsured);

					if (!result4.IsValid)
					{
						var firstError = result4.Errors.First();
						mMessageboxService.ShowToast(firstError.ErrorMessage);
						return;
					}

					CertValidator validatorFirstAidCert = new CertValidator();
					var result5 = validatorFirstAidCert.Validate(FirstAidCertified);

					if (!result5.IsValid)
					{
						var firstError = result5.Errors.First();
						mMessageboxService.ShowToast(firstError.ErrorMessage);
						return;
					}

					for (int i = 0; i < OperationalCerts.Count; i++)
					{
						CertValidator validatorOperationalCert = new CertValidator();
						var result3 = validatorOperationalCert.Validate(OperationalCerts[i]);

						if (!result3.IsValid)
						{
							var firstError = result3.Errors.First();
							mMessageboxService.ShowToast(firstError.ErrorMessage);
							return;
						}
					}

					if (IsVerify == false)
					{
						mMessageboxService.ShowToast("Please verify this information is true");
						View.ScrollToBottom();
						return;
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

					Debug.WriteLine("upload all image is done");

					if (Mvx.Resolve<ITaskManagementService>().IsAllTaskDone())
					{
						UserProfile.PersonalProfile.FirstName = FirstName;
						UserProfile.PersonalProfile.LastName = LastName;
						UserProfile.PersonalProfile.DOB = DOB;
						UserProfile.PersonalProfile.Mobile = MobileNumber;
						UserProfile.PersonalProfile.BulidingNo = UnitNumber;
						UserProfile.PersonalProfile.Street = StreetName;
						UserProfile.PersonalProfile.State = State;
						UserProfile.PersonalProfile.NOK = NOK;
						UserProfile.PersonalProfile.NOKPhone = NOKPhone;
						UserProfile.PersonalProfile.PostCode = PostCode;

						if (IsTickFirstAid != true)
						{
							FirstAidCertified.CertItem.Document = null;
							FirstAidCertified.CertItem.Expiry = null;
						}
						if (IsTickIncomeInsured != true)
						{
							IncomeInsured.CertItem.Document = null;
							IncomeInsured.CertItem.Expiry = null;
						}
						UserProfile.PersonalProfile.Certs[0] = ConstructionCardItem.CertItem;
						UserProfile.PersonalProfile.Certs[1] = TraceLicenseItem.CertItem;
						UserProfile.PersonalProfile.Certs[2] = IncomeInsured.CertItem;
						UserProfile.PersonalProfile.Certs[3] = FirstAidCertified.CertItem;
						UserProfile.PersonalProfile.Certs[4] = GSTRegistered.CertItem;

						UserProfile.PersonalProfile.Certs[6] = PublicLiablityCert.CertItem;

						for (int k = 0; k < OperationalCerts.Count; k++)
						{
							UserProfile.PersonalProfile.Certs.Add(OperationalCerts[k].CertItem);
						}



						UserProfile.PersonalProfile.StatementAgree = DateTime.Now;
						UserProfile.PersonalProfile.ProfileCompletionStep = ProfileCompletionStep.PPComplete;

						var response = await mApiService.CreatePersonalProfile(UserProfile.PersonalProfile);

						mPlatformService.HideNetworkIndicator();
						mProgressDialogService.HideProgressDialog();

						if (response.StatusCode == System.Net.HttpStatusCode.OK)
						{
							mCacheService.IsShowProfileView = true;
							Mvx.Resolve<ITrackingService>().SendEvent("Personal Profile completed");
							View.ShowPopupFinish();
						}
						else
						{
							RefreshListOperationalCert();
							if (!string.IsNullOrEmpty(response.ErrorData))
							{
								string errorData = response.ErrorData;
								ErrorLoginResponse err = JsonConvert.DeserializeObject<ErrorLoginResponse>(errorData);
								mMessageboxService.ShowToast(err.Description);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				RefreshListOperationalCert();
				mMessageboxService.Show("Error", ex.Message);
#endif
			}
		}
		#endregion

		#endregion

		#region Methods

		#region ShowCompletedStep

		public void ShowCompetedStep()
		{
			var personalProfile = DataHelper.RetrieveFromUserPref<PersonalProfile>("PERSONAL_STEP");

			if (personalProfile != null)
			{
				if (personalProfile.ProfileCompletionStep == ProfileCompletionStep.PPStep1)
				{
					ShowCompletedStep1(personalProfile);
				}

				else if (personalProfile.ProfileCompletionStep == ProfileCompletionStep.PPStep2)
				{
					ShowCompletedStep1(personalProfile);
					ShowCompletedStep2(personalProfile);
				}
				else if (personalProfile.ProfileCompletionStep == ProfileCompletionStep.PPStep3)
				{
					ShowCompletedStep1(personalProfile);
					ShowCompletedStep2(personalProfile);
					ShowCompletedStep3(personalProfile);

				}
				else if (personalProfile.ProfileCompletionStep == ProfileCompletionStep.PPStep4)
				{
					ShowCompletedStep1(personalProfile);
					//For Step 2
					ConstructionCardItem.LocalPathImage = personalProfile.Certs[0].Document.Media;
					//For step 3
					TraceLicenseItem.LocalPathImage = personalProfile.Certs[1].Document.Media;
					//For step 4
					IncomeInsured.LocalPathImage = personalProfile.Certs[2].Document.Media;
				}
			}
		}

		public void ShowCompletedStep1(PersonalProfile personalProfile)
		{
			FirstName = personalProfile.FirstName;
			LastName = personalProfile.LastName;
			DOB = personalProfile.DOB;
			MobileNumber = personalProfile.Mobile;
			UnitNumber = personalProfile.BulidingNo;
			StreetName = personalProfile.Street;
			PostCode = personalProfile.PostCode;
			State = personalProfile.State;
			NOK = personalProfile.NOK;
			NOKPhone = personalProfile.NOKPhone;
			DOBText = personalProfile.DOB.ToString().Split(' ')[0];
		}

		private bool IsUploadingConstructorPhoto = false;

		public void ShowCompletedStep2(PersonalProfile personalProfile)
		{

			ConstructionCardItem.CertItem = personalProfile.Certs[0];
			if (ConstructionCardItem.CertItem.Document.Media == null && IsUploadingConstructorPhoto == false && ConstructionCardItem.CertItem.Document.LocalMedia != null)
			{
				var cancelToken = new CancellationTokenSource();

				var bitmapImage = View.GetByteImage(ConstructionCardItem.CertItem.Document.LocalMedia);
				var imageName = Guid.NewGuid().ToString() + ".jpeg";

				var taskUpload = System.Threading.Tasks.Task.Run(async () =>
				{
					var urlResponse = await Mvx.Resolve<IAzureService>().Upload(bitmapImage, imageName);
					ConstructionCardItem.CertItem.Document.Media = urlResponse.Replace("/ShopItstore", string.Empty);
					ConstructionCardItem.CertItem.Document.MediaCreated = DateTime.Now;
					IsUploadingConstructorPhoto = false;

				}, cancelToken.Token);

				IsUploadingConstructorPhoto = true;
				Mvx.Resolve<ITaskManagementService>().AddTask(taskUpload, cancelToken);
			}

		}

		private bool IsUploadingTradePhoto = false;
		public void ShowCompletedStep3(PersonalProfile personalProfile)
		{
			TraceLicenseItem.CertItem = personalProfile.Certs[1];
			if (TraceLicenseItem.CertItem.Document != null)
			{
				if (TraceLicenseItem.CertItem.Document.Media == null && IsUploadingTradePhoto == false && TraceLicenseItem.CertItem.Document.LocalMedia != null)
				{
					var cancelToken = new CancellationTokenSource();

					var bitmapImage = View.GetByteImage(TraceLicenseItem.CertItem.Document.LocalMedia);
					var imageName = Guid.NewGuid().ToString() + ".jpeg";

					var taskUpload = System.Threading.Tasks.Task.Run(async () =>
					{
						var urlResponse = await Mvx.Resolve<IAzureService>().Upload(bitmapImage, imageName);
						TraceLicenseItem.CertItem.Document.Media = urlResponse.Replace("/ShopItstore", string.Empty);;
						TraceLicenseItem.CertItem.Document.MediaCreated = DateTime.Now;
						IsUploadingTradePhoto = false;

					}, cancelToken.Token);

					IsUploadingTradePhoto = true;
					Mvx.Resolve<ITaskManagementService>().AddTask(taskUpload, cancelToken);
				}
			}
		}

		#endregion

		#region HightlightStep
		public void HightlightStep(bool isStep1, bool isStep2, bool isStep3, bool isStep4)
		{
			this.IsStep1 = isStep1;
			this.IsStep2 = isStep2;
			this.IsStep3 = isStep3;
			this.IsStep4 = isStep4;
		}
		#endregion

		#region RefreshOperationalCert
		public void RefreshListOperationalCert()
		{
			for (int i = 5; i < UserProfile.PersonalProfile.Certs.Count; i++)
			{
				UserProfile.PersonalProfile.Certs.Remove(UserProfile.PersonalProfile.Certs[i]);
			}
		}
		#endregion

		#endregion  
	}

}

