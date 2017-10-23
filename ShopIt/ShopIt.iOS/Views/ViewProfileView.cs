using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using ShopIt.iOS;
using UIKit;
using ViewModels;
using ShopIt.iOS.Views.TableSources;

namespace ShopIt.iOS.Views
{
	public partial class ViewProfileView : BaseView, IViewProfileView
	{

		#region Variables

		private IntPtr handleSizeOperation;
		private IntPtr handleSizeLicence;
		#endregion

		public ViewProfileView() : base("ViewProfileView", null)
		{
		}

		public new ViewProfileViewModel ViewModel
		{
			get

			{
				return base.ViewModel as ViewProfileViewModel;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.View = this;
			var set = this.CreateBindingSet<ViewProfileView, ViewProfileViewModel>();
			set.Bind(lbFirstName).To(vm => vm.UserProfile.PersonalProfile.FirstName);
			set.Bind(lbLastName).To(vm => vm.UserProfile.PersonalProfile.LastName);
			set.Bind(lbEmailAddress).To(vm => vm.UserProfile.PersonalProfile.Email);
			set.Bind(lbDOB).To(vm => vm.DOBText);

			set.Bind(lbMobileNumber).To(vm => vm.UserProfile.PersonalProfile.Mobile);
			set.Bind(lbUnit).To(vm => vm.UserProfile.PersonalProfile.BulidingNo);
			set.Bind(lbStreetName).To(vm => vm.UserProfile.PersonalProfile.Street);
			set.Bind(lbPostcode).To(vm => vm.UserProfile.PersonalProfile.PostCode);
			set.Bind(lbState).To(vm => vm.UserProfile.PersonalProfile.State);
			set.Bind(lbFullName).To(vm => vm.UserProfile.PersonalProfile.NOK);
			set.Bind(lbContactNumber).To(vm => vm.UserProfile.PersonalProfile.NOKPhone);

			set.Bind(lbCardNumber).To(vm => vm.UserProfile.PersonalProfile.Certs[0].Identifier);
			set.Bind(lbCardExpiry).To(vm => vm.UserProfile.PersonalProfile.Certs[0].ExpiryText);

			set.Bind(lbTypeOfTrade).To(vm => vm.UserProfile.PersonalProfile.Certs[1].Descriptor);
			set.Bind(lbTradeLicenceNumber).To(vm => vm.UserProfile.PersonalProfile.Certs[1].Identifier);
			set.Bind(lbTradeLicenceExpiry).To(vm => vm.UserProfile.PersonalProfile.Certs[1].ExpiryText);

			//set.Bind(lbIncomeInsuredExpiry).To(vm => vm.UserProfile.PersonalProfile.Certs[2].Expiry);

			set.Bind(lbPostcodeCompany).To(vm => vm.UserProfile.CompanyProfile.PostCode);
			set.Bind(lbStateCompany).To(vm => vm.UserProfile.CompanyProfile.State);
			set.Bind(lbUnitCompany).To(vm => vm.UserProfile.CompanyProfile.BulidingNo);
			set.Bind(lbStreetNameCompany).To(vm => vm.UserProfile.CompanyProfile.Street);

			set.Bind(btnIncomeInsured).For(s => s.Selected).To(vm => vm.IsShowIncomeInsuredCert);
			set.Bind(btnFirstAidCert).For(s => s.Selected).To(vm => vm.IsShowFirstAidCert);
			set.Bind(btnGSTRegistered).For(s => s.Selected).To(vm => vm.IsShowGSTRegistered);


			set.Bind(lbFirstAidExpiry).To(vm => vm.UserProfile.PersonalProfile.Certs[3].ExpiryText);
			set.Bind(lbIncomeInsuredExpiry).To(vm => vm.UserProfile.PersonalProfile.Certs[2].ExpiryText);
			set.Bind(ivInComeInsuredStatus).For(s => s.Highlighted).To(vm => vm.IncomeInsured.IsOverdue);
			set.Bind(ivFirstAidStatus).For(s => s.Highlighted).To(vm => vm.FirstAidCertified.IsOverdue);


			set.Bind(btnViewIncomeInsuredPhoto).To(vm => vm.IncomeInsured.ViewDocumentCommand);
			set.Bind(btnViewLicenceTrade).To(vm => vm.TraceLicenseItem.ViewDocumentCommand);
			set.Bind(btnViewContruction).To(vm => vm.ConstructionCardItem.ViewDocumentCommand);
			set.Bind(btnViewFirstAidPhoto).To(vm => vm.FirstAidCertified.ViewDocumentCommand);


			set.Bind(lbPerPublicLiabilityExpiry).To(vm => vm.PerPublicLiabilityCert.CertItem.ExpiryText);
			set.Bind(btnViewPerPublicLiabilityPhoto).To(vm => vm.PerPublicLiabilityCert.ViewDocumentCommand);
			set.Bind(btnPerPublicLiability).For(s => s.Selected).To(vm => vm.IsShowPerPublicLiability);
			set.Bind(ivPerPublicLiabilityStatus).For(s => s.Highlighted).To(vm => vm.PerPublicLiabilityCert.IsOverdue);

			set.Bind(btnAddCompanyDetails).To(vm => vm.AddCompanyDetailsCommand);

			set.Bind(lbBusinessName).To(vm => vm.UserProfile.CompanyProfile.BusinessName);
			set.Bind(lbABN).To(vm => vm.UserProfile.CompanyProfile.ABN);
			set.Bind(lbPositionJob).To(vm => vm.UserProfile.CompanyProfile.JobTitle);

			set.Bind(ivStatusContructorCard).For(s => s.Highlighted).To(vm => vm.ConstructionCardItem.IsOverdue);
			set.Bind(ivStatusTradeLicence).For(s => s.Highlighted).To(vm => vm.TraceLicenseItem.IsOverdue);
			//set.Bind()

			set.Bind(btnSameAddress).For(s => s.Selected).To(vm => vm.IsShowSameAsPersonal);
			set.Bind(btnCompensation).For(s => s.Selected).To(vm => vm.IsShowCompensation);
			set.Bind(btnProductLiability).For(s => s.Selected).To(vm => vm.IsShowProductLiability);
			set.Bind(btnPublicLiability).For(s => s.Selected).To(vm => vm.IsShowPublicLiability);
			set.Bind(btnRegisteredCompany).For(s => s.Selected).To(vm => vm.IsShowGSTRegisterCompany);
			set.Bind(btnDisplayMy).For(s => s.Selected).To(vm => vm.IsShowDisplayMyBusiness);

			set.Bind(lbCompenstationExpiry).To(vm => vm.CompensationCert.CertItem.ExpiryText);
			set.Bind(ivCompenstationStatus).For(s => s.Highlighted).To(vm => vm.CompensationCert.IsOverdue);
			set.Bind(btnViewCompensation).To(vm => vm.CompensationCert.ViewDocumentCommand);

			set.Bind(lbPublicDollar).To(vm => vm.PublicLiabilityCert.CertItem.AmoutText);
			set.Bind(lbPublicLiabilityExpiry).To(vm => vm.PublicLiabilityCert.CertItem.ExpiryText);
			set.Bind(ivPublicLiabilityStatus).For(s => s.Highlighted).To(vm => vm.PublicLiabilityCert.IsOverdue);
			set.Bind(btnViewPublicLiability).To(vm => vm.PublicLiabilityCert.ViewDocumentCommand);

			set.Bind(lbProductDollar).To(vm => vm.ProductLiabilityCert.CertItem.AmoutText);
			set.Bind(lbProductLiabilityExpiry).To(vm => vm.ProductLiabilityCert.CertItem.ExpiryText);
			set.Bind(ivProductLiabilityStatus).For(s => s.Highlighted).To(vm => vm.ProductLiabilityCert.IsOverdue);
			set.Bind(btnViewProductLiability).To(vm => vm.ProductLiabilityCert.ViewDocumentCommand);

			set.Bind(btnEditProfile).For(s => s.Hidden).To(vm => vm.IsShowEditButton).WithConversion("TrueFalse");
			//set.Bind(vDisplay).For(s => s.Hidden).To(vm => vm.IsShowEditButton).WithConversion("TrueFalse");;

			set.Bind(btnEditProfile).To(vm => vm.EditProfileCommand);
			var operationalTableSource = new OperationalCertViewTableViewSource(tbOperationalCert, false, null);
			set.Bind(operationalTableSource).For(s => s.ItemsSource).To(vm => vm.OperationalCert);
			tbOperationalCert.Source = operationalTableSource;

			var licenceTableSource = new LicenceCertViewTableSource(tbLicences, false, null);
			set.Bind(licenceTableSource).For(s => s.ItemsSource).To(vm => vm.LicencesCert);
			tbLicences.Source = licenceTableSource;

			set.Bind(btnBack).To(vm => vm.GoBackCommand);
			set.Apply();

			InitView();
		}

		public void InitView()
		{

			handleSizeOperation = new IntPtr(1);
			handleSizeLicence = new IntPtr(2);
			tbOperationalCert.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handleSizeOperation);
			tbLicences.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handleSizeLicence);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		#region ViewWillAppear

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);
			if (parent == null)
			{
				tbOperationalCert.RemoveObserver(this, "contentSize", handleSizeOperation);
				tbLicences.RemoveObserver(this, "contentSize", handleSizeLicence);
			}
		}

		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (keyPath == "contentSize" && context == handleSizeOperation)
			{
				if (tbOperationalCert.ContentSize.Height == 0)
				{
					consHeightVOtherCert.Constant = tbOperationalCert.ContentSize.Height;

				}
				else
				{
					consHeightVOtherCert.Constant = tbOperationalCert.ContentSize.Height + 35;
				}
			}
			else if (keyPath == "contentSize" && context == handleSizeLicence)
			{
				if (tbLicences.ContentSize.Height == 0)
				{
					consHeightVLicence.Constant = tbLicences.ContentSize.Height;
				}
				else
				{
					consHeightVLicence.Constant = tbLicences.ContentSize.Height + 35;
				}
			}
			else
			{
				base.ObserveValue(keyPath, ofObject, change, context);
			}
		}
		#endregion

		#region Implement IViewProfileView

		public void HidenKeyboard()
		{
			//throw new NotImplementedException();
		}

		public void ShowTradeContractorCert()
		{
			consHeightVTradeContructor.Constant = 241f;
			vTradeContractor.Hidden = false;
		}
		public void HidenTradeContractorCert()
		{
			consHeightVTradeContructor.Constant = 0f;
			vTradeContractor.Hidden = true;
		}

		public void ShowOperationCert()
		{

			consHeightVOtherCert.Constant = 187f;
			vOtherCert.Hidden = false;
		}

		public void HidenOperationCert()
		{
			consHeightVOtherCert.Constant = 0f;
			vOtherCert.Hidden = true;
		}

		public void ShowCompanyProfile()
		{
			vCompanyDetails.Hidden = false;
		}

		public void HideCompanyProfile()
		{
			consHeightVCompany.Constant = 0f;
			vCompanyDetails.Hidden = true;
		}

		public void ShowAddCompanyDetails()
		{
			consHeightAddCompany.Constant = 72f;
			//consHeightVCompany.Constant = 
			btnAddCompanyDetails.Hidden = false;
		}

		public void HidenAddCompanyDetails()
		{
			consHeightAddCompany.Constant = 0f;
			btnAddCompanyDetails.Hidden = true;
		}

		public void ShowCompensation()
		{
			consHeightVCompensation.Constant = 106;
		}

		public void HidenCompensation()
		{
			consHeightVCompensation.Constant = 0;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 106f;
		}

		public void ShowPublicLiability()
		{
			vPublicLiability.Hidden = false;
			consHeightVPublicLiablility.Constant = 156f;
		}

		public void HidenPublicLiability()
		{
			vPublicLiability.Hidden = true;
			consHeightVPublicLiablility.Constant = 0;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 156f;
		}

		public void ShowProductLiability()
		{
			consVHeightProductLiability.Constant = 156f;
			vProductLiability.Hidden = false;
		}

		public void HidenProductLiability()
		{
			vProductLiability.Hidden = true;
			consVHeightProductLiability.Constant = 0;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 156f;
		}
		public void ShowLicences()
		{
			consHeightVCompany.Constant = consHeightVCompany.Constant + tbLicences.ContentSize.Height + 35;
		}
		public void HidenLicences()
		{

		}
		public void ShowAddressCompany()
		{
			consHeightVAddressCompany.Constant = 200f;
			vSameAddress.Hidden = false;
		}

		public void HidenAddressCompany()
		{
			consHeightVAddressCompany.Constant = 0f;
			vSameAddress.Hidden = true;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 200f;
		}

		public void ShowIncomeInsured()
		{
			consHeightVIncomeInsured.Constant = 50f;
			vIncomInsured.Hidden = false;

			consHeightVIncomeInsuredDetails.Constant = 106f;
			vIncomeInsuredDetails.Hidden = false;
		}

		public void HidenIncomeInsured()
		{
			consHeightVIncomeInsured.Constant = 0f;
			vIncomInsured.Hidden = true;

			consHeightVIncomeInsuredDetails.Constant = 0f;
			vIncomeInsuredDetails.Hidden = true;
		}

		public void ShowFirstAid()
		{
			consHeightVFirstAid.Constant = 50f;
			vFirstAidCert.Hidden = false;

			consHeightVFirstAidDetails.Constant = 106f;
			vFirstAidDetails.Hidden = false;
		}

		public void HidenFirstAid()
		{
			consHeightVFirstAid.Constant = 0f;
			vFirstAidCert.Hidden = true;

			consHeightVFirstAidDetails.Constant = 0f;
			vFirstAidDetails.Hidden = true;
		}

		public void ShowGSTRegistered()
		{
			consHeightVGST.Constant = 50f;
			vGST.Hidden = false;
		}

		public void HidenGSTRegistered()
		{
			consHeightVGST.Constant = 0f;
			vGST.Hidden = true;
		}

		public void ShowOtherInformation()
		{
			consHeightVOtherInfo.Constant = 35f;
			vOtherInfo.Hidden = false;
		}

		public void HidenOtherInformation()
		{
			consHeightVOtherInfo.Constant = 0f;
			vOtherInfo.Hidden = true;
		}

		public void ShowContructorCardExpiryDate()
		{
			vConstructorCardExpiry.Hidden = false;
			vHeightConstructorCardExpiry.Constant = 50f;
		}

		public void HidenContructorCardExpiryDate()
		{
			vConstructorCardExpiry.Hidden = true;
			vHeightConstructorCardExpiry.Constant = 0f;


		}

		public void ShowPerPublicLiability()
		{
			consHeightVPerPublicLiability.Constant = 50f;
			vPerPublicLiability.Hidden = false;
			consHeightVPerPublicLiabilityDetails.Constant = 106f;
			vPerPublicLiabilityDetails.Hidden = false;
		}

		public void HidenPerPublicLiability()
		{
			consHeightVPerPublicLiability.Constant = 0f;
			vPerPublicLiability.Hidden = true;
			consHeightVPerPublicLiabilityDetails.Constant = 0f;
			vPerPublicLiabilityDetails.Hidden = true;
		}
		#endregion
	}
}

