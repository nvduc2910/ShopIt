using System;
using ShopIt.iOS;
using ViewModels;
using ShopIt.iOS.Views.TableSources;
using UIKit;
using MvvmCross.Binding.BindingContext;
using Foundation;

namespace ShopIt.iOS.Views
{
	public partial class EditProfileView : BaseView, IEditProfileView
	{
		#region Variables

		private IntPtr handleSizeOperation;
		private IntPtr handleSizeLicence;

		#endregion

		public EditProfileView() : base("EditProfileView", null)
		{
		}

		public new EditProfileViewModel ViewModel
		{
			get
			{
				return base.ViewModel as EditProfileViewModel;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.View = this;
			var set = this.CreateBindingSet<EditProfileView, EditProfileViewModel>();

			//binding for personal profile

			set.Bind(tfFirstName).To(vm => vm.FirstName);
			set.Bind(tfLastName).To(vm => vm.LastName);
			set.Bind(tfEmailAddress).To(vm => vm.Email);
			set.Bind(tfDOB).To(vm => vm.DOBText);
			set.Bind(tfMobileNumber).To(vm => vm.MobileNumber);
			set.Bind(tfUnit).To(vm => vm.UnitNumber);
			set.Bind(tfStreetNumber).To(vm => vm.StreetName);
			set.Bind(tfPostCode).To(vm => vm.PostCode);
			set.Bind(tfState).To(vm => vm.State);
			set.Bind(tfFullName).To(vm => vm.NOK);
			set.Bind(tfContactNumber).To(vm => vm.NOKPhone);
			set.Bind(btnIncomeInsured).For(s => s.Selected).To(vm => vm.IsTickIncomeInsured);
			set.Bind(btnFirstAid).For(s => s.Selected).To(vm => vm.IsTickFirstAid);
			set.Bind(btnGSTRegistered).For(s => s.Selected).To(vm => vm.IsTickGSTRegistered);
			set.Bind(btnTickPerPublicLiability).For(s => s.Selected).To(vm => vm.IsTickPerPublicLiability);

			set.Bind(btnIncomeInsured).To(vm => vm.CheckIncomeInsuredCommand);
			set.Bind(btnFirstAid).To(vm => vm.CheckFirstAidCommand);
			set.Bind(btnGSTRegistered).To(vm => vm.CheckGSTRegisteredCommand);
			set.Bind(btnTickPerPublicLiability).To(vm => vm.CheckPerPublicLiablityCommand);

			set.Bind(tfCardNumber).To(vm => vm.ConstructionCardItem.CertItem.Identifier);
			set.Bind(tfCardExpiry).To(vm => vm.ConstructionCardItem.ExpiryText);
			set.Bind(btnDOB).To(vm => vm.DOBCommand);
			set.Bind(tfTypeOfTrade).To(vm => vm.TraceLicenseItem.CertItem.Descriptor);
			set.Bind(tfTradeLicenceNumber).To(vm => vm.TraceLicenseItem.CertItem.Identifier);
			set.Bind(tfTradeLicenceExpiry).To(vm => vm.TraceLicenseItem.ExpiryText);
			set.Bind(btnExpiryConstructionCard).To(vm => vm.ConstructionCardItem.EditExpiryCommand);
			set.Bind(btnExpiryTradeLicence).To(vm => vm.TraceLicenseItem.EditExpiryCommand);
			set.Bind(btnTradeContractor).To(vm => vm.TraceLicenseItem.ViewDocumentCommand);
			set.Bind(btnContractorCard).To(vm => vm.ConstructionCardItem.ViewDocumentCommand);
			set.Bind(btnCheckVerify).For(s => s.Selected).To(vm => vm.IsVerify);
			set.Bind(btnCheckVerify).To(vm => vm.VerifyCommand);
			set.Bind(btnTapAdd).To(vm => vm.TapToAddOperationalCertCommand);
			//set.Bind(btnTradeContractor).For("Title").To(vm => vm.AddTradeContructorText);


			//IncomeInsured
			set.Bind(lbIncomeInsuredExpiry).To(vm => vm.IncomeInsured.ExpiryText);
			set.Bind(btnIncomeInsuredExpiry).To(vm => vm.IncomeInsured.EditExpiryCommand);
			set.Bind(btnViewIncomeInsuredPhoto).To(vm => vm.IncomeInsured.ViewDocumentCommand);
			set.Bind(ivIncomeInsuredStatus).For(s => s.Highlighted).To(vm => vm.IncomeInsured.IsOverdue);

			//FirstAid
			set.Bind(lbFirstAidExpiry).To(vm => vm.FirstAidCertified.ExpiryText);
			set.Bind(btnViewFirstAidPhoto).To(vm => vm.FirstAidCertified.ViewDocumentCommand);
			set.Bind(btnFirstAIdExpiry).To(vm => vm.FirstAidCertified.EditExpiryCommand);
			set.Bind(ivFirstAidStatus).For(s => s.Highlighted).To(vm => vm.FirstAidCertified.IsOverdue);
			//Per Public Liability

			set.Bind(lbPerPublicLiability).To(vm => vm.PerPublicLiability.ExpiryText);
			set.Bind(btnViewPerPublicLiability).To(vm => vm.PerPublicLiability.ViewDocumentCommand);
			set.Bind(btnPerPublicLiablityExpiry).To(vm => vm.PerPublicLiability.EditExpiryCommand);
			set.Bind(ivPerPublicLiabilityStatus).For(s => s.Highlighted).To(vm => vm.PerPublicLiability.IsOverdue);
			//Add Public Liablity

			set.Bind(tfAddPerPublicLiability).To(vm => vm.PerPublicLiability.ExpiryText);
			set.Bind(btnAddPerPublicLiabilityExpiry).To(vm => vm.PerPublicLiability.EditExpiryCommand);
			set.Bind(ivAddPerPublicLiability).For(s => s.LocalPathImage).To(vm => vm.PerPublicLiability.CertItem.Document.LocalMedia);
			set.Bind(btnAddPerPublicLiabilityPhoto).To(vm => vm.PerPublicLiability.ChooseCertPhotoCommand);

			//Add IncomeInsured
			set.Bind(tfAddIncomeInsuredExpiry).To(vm => vm.IncomeInsured.ExpiryText);
			set.Bind(btnAddIncomeInsuredExpiry).To(vm => vm.IncomeInsured.EditExpiryCommand);
			set.Bind(ivAddIncomeInsuredPhoto).For(s => s.LocalPathImage).To(vm => vm.IncomeInsured.CertItem.Document.LocalMedia);
			set.Bind(btnAddIncomeInsuredPhoto).To(vm => vm.IncomeInsured.ChooseCertPhotoCommand);

			//Add First Aid
			set.Bind(tfAddFirstAidExpiry).To(vm => vm.FirstAidCertified.ExpiryText);
			set.Bind(btnAddFirstAidExpiry).To(vm => vm.FirstAidCertified.EditExpiryCommand);
			set.Bind(ivAddFirstAidPhoto).For(s => s.LocalPathImage).To(vm => vm.FirstAidCertified.CertItem.Document.LocalMedia);
			set.Bind(btnAddFirstAidPhoto).To(vm => vm.FirstAidCertified.ChooseCertPhotoCommand);

			var operationalCertTableSource = new OperationalCertViewTableViewSource(tbOperationalCert, true, this);
			set.Bind(operationalCertTableSource).For(s => s.ItemsSource).To(vm => vm.OperationalCerts);
			tbOperationalCert.Source = operationalCertTableSource;

			var newoperationalCertTableSource = new OperationalCertTableViewSource(tbNewOperationalCert, null, this);
			set.Bind(newoperationalCertTableSource).For(s => s.ItemsSource).To(vm => vm.NewOperationalCerts);
			tbNewOperationalCert.Source = newoperationalCertTableSource;

			//binding for company profile 



			set.Bind(tfCPbusinessName).To(vm => vm.CPBusinessName);
			set.Bind(tfCPABN).To(vm => vm.CPABN);
			set.Bind(tfCPPosition).To(vm => vm.CPPositionJobTitle);
			set.Bind(tfCPUnit).To(vm => vm.CPUnitNumber);
			set.Bind(tfCPStreet).To(vm => vm.CPStreetName);
			set.Bind(tfCPPostCode).To(vm => vm.CPPostCode);
			set.Bind(tfCPState).To(vm => vm.CPState);

			set.Bind(btnCPGSTRegistered).For(s => s.Selected).To(vm => vm.IsTickGSTRegistedCompany);
			set.Bind(btnCPCompensation).For(s => s.Selected).To(vm => vm.IsTickCompensation);
			set.Bind(btnCPPublicLiability).For(s => s.Selected).To(vm => vm.IsTickPublicLiability);
			set.Bind(btnCPProductLiability).For(s => s.Selected).To(vm => vm.IsTickProductLiability);
			set.Bind(btnCPSameAsPer).For(s => s.Selected).To(vm => vm.IsShowSameAsPersonal);
			set.Bind(btnCPVerify).For(s => s.Selected).To(vm => vm.IsCPVerify);
			set.Bind(btnCPDisplay).For(s => s.Selected).To(vm => vm.IsShowDisplayMyBusiness);

			set.Bind(btnCPGSTRegistered).To(vm => vm.CheckGSTRegisteredCompanyCommand);
			set.Bind(btnCPCompensation).To(vm => vm.CheckCompensationCommand);
			set.Bind(btnCPPublicLiability).To(vm => vm.CheckPublicLiabilityCommand);
			set.Bind(btnCPProductLiability).To(vm => vm.CheckProductLiabilityCommand);
			set.Bind(btnCPSameAsPer).To(vm => vm.CheckSameAsAddressCommand);
			set.Bind(btnCPDisplay).To(vm => vm.CheckDisplayMyBusinessCommand);
			set.Bind(btnCPVerify).To(vm => vm.CheckVerifyCompanyCommand);
			set.Bind(btnCPTapAddMore).To(vm => vm.TapToAddLicencesCommand);

			set.Bind(tfWorkeCompenstationExpiry).To(vm => vm.CompensationCert.ExpiryText);
			set.Bind(btnViewWorksCompenPhoto).To(vm => vm.CompensationCert.ViewDocumentCommand);
			set.Bind(ivWorksCompenstationPhoto).For(s => s.Highlighted).To(vm => vm.CompensationCert.IsOverdue);
			set.Bind(btnWorkCompenstationExpiry).To(vm => vm.CompensationCert.EditExpiryCommand);


			set.Bind(tfPublicLiabilityExpiry).To(vm => vm.PublicLiabilityCert.ExpiryText);
			set.Bind(tfCPPublicLiabilityDollar).To(vm => vm.PublicLiabilityCert.AmountText);
			set.Bind(btnViewPublicLiabilityPhoto).To(vm => vm.PublicLiabilityCert.ViewDocumentCommand);
			set.Bind(ivPublicLiabilityStatus).For(s => s.Highlighted).To(vm => vm.PublicLiabilityCert.IsOverdue);
			set.Bind(btnPublicLiabilityExpiry).To(vm => vm.PublicLiabilityCert.EditExpiryCommand);

			set.Bind(tfProductLiabilityExpiry).To(vm => vm.ProductLiabilityCert.ExpiryText);
			set.Bind(tfCPProductLiability).To(vm => vm.ProductLiabilityCert.AmountText);
			set.Bind(btnViewProductLiabilityPhoto).To(vm => vm.ProductLiabilityCert.ViewDocumentCommand);
			set.Bind(ivProductLiabilityStatus).For(s => s.Highlighted).To(vm => vm.ProductLiabilityCert.IsOverdue);
			set.Bind(btnProductLiabilityExpiry).To(vm => vm.ProductLiabilityCert.EditExpiryCommand);


			set.Bind(ivContructionStatus).For(s => s.Highlighted).To(vm => vm.ConstructionCardItem.IsOverdue);
			set.Bind(ivTradeLicencesStatus).For(s => s.Highlighted).To(vm => vm.TraceLicenseItem.IsOverdue);

			set.Bind(tfTypeOfTradeAdd).To(vm => vm.TraceLicenseItem.CertItem.Descriptor);
			set.Bind(tfTradeLicenceAdd).To(vm => vm.TraceLicenseItem.CertItem.Identifier);
			set.Bind(tfLicenceExpiryDateAdd).To(vm => vm.TraceLicenseItem.ExpiryText);

			//Add new trade
			set.Bind(btnLicenceExpiryDateAdd).To(vm => vm.TraceLicenseItem.EditExpiryCommand);
			set.Bind(btnLicencePhotoAdd).To(vm => vm.TraceLicenseItem.ChooseCertPhotoCommand);
			set.Bind(ivLicencePhotoAdd).For(s => s.LocalPathImage).To(vm => vm.TraceLicenseItem.LocalPathImage);

			//Add new Compenstation
			set.Bind(tfAddWorkCompenstationExpiry).To(vm => vm.CompensationCert.ExpiryText);
			set.Bind(btnAddWorkCompenstationExpiry).To(vm => vm.CompensationCert.EditExpiryCommand);
			set.Bind(ivAddPhotoWorkCompenstation).For(s => s.LocalPathImage).To(vm => vm.CompensationCert.CertItem.Document.LocalMedia);
			set.Bind(btnAddPhotoWorkCompenstation).To(vm => vm.CompensationCert.ChooseCertPhotoCommand);

			//Add new PublicLiability

			set.Bind(tfAddPublicLiabilityExpiry).To(vm => vm.PublicLiabilityCert.ExpiryText);
			set.Bind(btnAddPubicliablityExpiry).To(vm => vm.PublicLiabilityCert.EditExpiryCommand);
			set.Bind(tfAddPublicLiabilityDollar).To(vm => vm.PublicLiabilityCert.AmountText);
			set.Bind(ivAddPublicLiabilityPhoto).For(s => s.LocalPathImage).To(vm => vm.PublicLiabilityCert.CertItem.Document.LocalMedia);
			set.Bind(btnAddPublicLiabilityPhoto).To(vm => vm.PublicLiabilityCert.ChooseCertPhotoCommand);

			//Add Product

			set.Bind(tfAddProductLiabilityExpiry).To(vm => vm.ProductLiabilityCert.ExpiryText);
			set.Bind(btnAddProductLiabilityExpiry).To(vm => vm.ProductLiabilityCert.EditExpiryCommand);
			set.Bind(tfAddProducLiabilityDollar).To(vm => vm.ProductLiabilityCert.AmountText);
			set.Bind(ivAddProductLiabilityPhoto).For(s => s.LocalPathImage).To(vm => vm.ProductLiabilityCert.CertItem.Document.LocalMedia);
			set.Bind(btnAddProductLiabilityPhoto).To(vm => vm.ProductLiabilityCert.ChooseCertPhotoCommand);

			var licencesTableSource = new LicenceCertViewTableSource(tbCPLicences, true, this);
			set.Bind(licencesTableSource).For(s => s.ItemsSource).To(vm => vm.LicencesCert);
			tbCPLicences.Source = licencesTableSource;

			var newLicencesTableSource = new LicencesCertTableViewSource(tbCPNewLicences, null, this);
			set.Bind(newLicencesTableSource).For(s => s.ItemsSource).To(vm => vm.NewLicences);
			tbCPNewLicences.Source = newLicencesTableSource;

			set.Bind(btnSave).To(vm => vm.SaveCommand);
			btnSaveTime.TouchUpInside += BtnSaveTime_TouchUpInside;
			btnCancel.TouchUpInside += BtnCancel_TouchUpInside;
			set.Bind(btnBack).To(vm => vm.GoBackCommand);
			InitView();
			ImplementKeyboard();
			set.Apply();

		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		#region InitView

		public void InitView()
		{
			vPopupDateTime.Layer.CornerRadius = 20f;
			vPopupDateTime.Layer.MasksToBounds = true;
			vPopupDateTime.ClipsToBounds = true;

			handleSizeOperation = new IntPtr(1);
			handleSizeLicence = new IntPtr(2);
			tbOperationalCert.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New | NSKeyValueObservingOptions.Initial, handleSizeOperation);
			tbCPLicences.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New | NSKeyValueObservingOptions.Initial, handleSizeLicence);
		}

		public bool TextFieldDissmiss(UITextField textField)
		{
			this.View.EndEditing(true);
			return false; // We do not want UITextField to insert line-breaks.
		}

		public void ImplementKeyboard()
		{
			tfFirstName.ShouldReturn += TextFieldDissmiss;
			tfLastName.ShouldReturn += TextFieldDissmiss;
			tfMobileNumber.ShouldReturn += TextFieldDissmiss;
			tfUnit.ShouldReturn += TextFieldDissmiss;
			tfStreetNumber.ShouldReturn += TextFieldDissmiss;
			tfPostCode.ShouldReturn += TextFieldDissmiss;
			tfState.ShouldReturn += TextFieldDissmiss;

			tfFullName.ShouldReturn += TextFieldDissmiss;
			tfContactNumber.ShouldReturn += TextFieldDissmiss;
			tfCardNumber.ShouldReturn += TextFieldDissmiss;
			tfCardExpiry.ShouldEndEditing += TextFieldDissmiss;
			tfTypeOfTrade.ShouldReturn += TextFieldDissmiss;
			tfTradeLicenceNumber.ShouldReturn += TextFieldDissmiss;
			tfTradeLicenceExpiry.ShouldEndEditing += TextFieldDissmiss;

			tfCPbusinessName.ShouldReturn += TextFieldDissmiss;
			tfCPABN.ShouldReturn += TextFieldDissmiss;
			tfCPPosition.ShouldReturn += TextFieldDissmiss;

			tfCPUnit.ShouldReturn += TextFieldDissmiss;
			tfState.ShouldReturn += TextFieldDissmiss;
			tfCPState.ShouldReturn += TextFieldDissmiss;
			tfCPPostCode.ShouldReturn += TextFieldDissmiss;
			tfCPStreet.ShouldReturn += TextFieldDissmiss;

			tfCPPublicLiabilityDollar.ShouldReturn += TextFieldDissmiss;
			tfCPProductLiability.ShouldReturn += TextFieldDissmiss;

			tfTradeLicenceAdd.ShouldReturn += TextFieldDissmiss;

		}

		#endregion

		#region ViewWillApear

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);
			if (parent == null)
			{
				tbOperationalCert.RemoveObserver(this, "contentSize", handleSizeOperation);
				tbCPLicences.RemoveObserver(this, "contentSize", handleSizeLicence);
			}
		}

		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (keyPath == "contentSize" && context == handleSizeOperation)
			{
				Console.WriteLine("handleSizeOperation");
				if (tbOperationalCert.ContentSize.Height == 0)
				{
					consHeightVOtherCert.Constant = tbOperationalCert.ContentSize.Height;
				}
				else
				{
					consHeightVOtherCert.Constant = tbOperationalCert.ContentSize.Height;
				}
			}
			else if (keyPath == "contentSize" && context == handleSizeLicence)
			{
				Console.WriteLine("handleSizeLicence");
				if (tbCPLicences.ContentSize.Height == 0)
				{
					consCPHeightLicences.Constant = tbCPLicences.ContentSize.Height;
				}
				else
				{
					consCPHeightLicences.Constant = tbCPLicences.ContentSize.Height + 35;
				}
			}
			else
			{
				base.ObserveValue(keyPath, ofObject, change, context);
			}
		}

		#endregion

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		#region HandleChooseTime

		void BtnSaveTime_TouchUpInside(object sender, EventArgs e)
		{
			ViewModel.SelectedTimeCommand.Execute(dpkTime.Date.NSDateToDateTime());
		}

		void BtnCancel_TouchUpInside(object sender, EventArgs e)
		{
			setAnimationHidden(vPopupDateTime);
			setAnimationHidden(vChooseTime);

		}

		public void setAnimationHidden(UIView viewHidden, bool hidden = true)
		{

			UIView.Animate(0.3, 0, UIViewAnimationOptions.LayoutSubviews,
			() =>
			{
				UIView.SetAnimationCurve(UIViewAnimationCurve.EaseInOut);
				if (hidden)
				{
					viewHidden.Alpha = 0;
				}
				else
				{
					viewHidden.Hidden = false;
					viewHidden.Alpha = 1;
				}
			},
			() =>
			{
				if (hidden)
				{
					viewHidden.Hidden = true;
				}
			});
		}

		#endregion

		#region Implement IViewProfileView

		public void HidenKeyboard()
		{
			View.EndEditing(true);
		}

		public void ShowTradeContractorCert()
		{
			InvokeOnMainThread(() =>
			{
				consHeightVTradeContructor.Constant = 241f;
				consHeightVTradeContractorAdd.Constant = 0f;
				vAddTradeContructor.Hidden = true;
				vTradeContrator.Hidden = false;
				UIView.Animate(0.5, () =>
				{
					View.LayoutIfNeeded();
				});
			});

		}
		public void HidenTradeContractorCert()
		{
			InvokeOnMainThread(() =>
			{
				vTradeContrator.Hidden = true;
				consHeightVTradeContructor.Constant = 0f;
				consHeightVTradeContractorAdd.Constant = 415f;
				vAddTradeContructor.Hidden = false;
				UIView.Animate(0.5, () =>
				{
					View.LayoutIfNeeded();
				});
			});
		}

		public void ShowOperationCert()
		{
			InvokeOnMainThread(() =>
			{
				consHeightVOtherCert.Constant = 185f;
				UIView.Animate(0.5, () =>
				{
					View.LayoutIfNeeded();
				});
			});
		}

		public void HidenOperationCert()
		{
			InvokeOnMainThread(() =>
			{
				consHeightVOtherCert.Constant = 0f;
				UIView.Animate(0.5, () =>
				{
					View.LayoutIfNeeded();
				});
			});
		}

		public void ShowCompanyProfile()
		{

			InvokeOnMainThread(() =>
			{
				consHeightVCompany.Constant = 1435f;
				UIView.Animate(0.5, () =>
				{
					View.LayoutIfNeeded();
				});
			});

			vCompanyDetails.Hidden = false;
			SetHightFirstTime();

		}

		public void HideCompanyProfile()
		{

			InvokeOnMainThread(() =>
			{
				consHeightVCompany.Constant = 0f;
				UIView.Animate(0.5, () =>
				{
					View.LayoutIfNeeded();
				});
			});
			vCompanyDetails.Hidden = true;
		}

		public void ShowSetTimeDialog()
		{
			vChooseTime.Hidden = false;
			vPopupDateTime.Hidden = false;

			UIView.Animate(0.25, () =>
			{
				vPopupDateTime.Alpha = 1;
				vChooseTime.Alpha = 1;
			});
		}

		public void HiddenTimeDialog()
		{
			setAnimationHidden(vPopupDateTime);
			setAnimationHidden(vChooseTime);
		}
		public void AddMoreOperationalCert()
		{
			consHeightVAddOperationalCert.Constant += 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
			tbNewOperationalCert.ReloadData();
		}
		public void RemoveOperationalCert()
		{
			consHeightVAddOperationalCert.Constant -= 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
			tbNewOperationalCert.ReloadData();
		}

		public void ShowLicences()
		{
			consHeightVCompany.Constant = consHeightVCompany.Constant + tbCPLicences.ContentSize.Height + 35;
		}

		public void HidenLicences()
		{
			consCPHeightLicences.Constant = 35f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 35f;
		}

		public void ShowWorksCompenstation()
		{
			consHeightVWorkCompenstation.Constant = 106f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 106f;
			vCompensation.Hidden = false;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenWordsCompenstation()
		{
			consHeightVWorkCompenstation.Constant = 0f;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 106f;
			vCompensation.Hidden = true;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowPublicLiability()
		{
			consHeightVPublicLiability.Constant = 156f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 156f;
			vPublicLiability.Hidden = false;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenPublicLiability()
		{
			consHeightVPublicLiability.Constant = 0;
			vPublicLiability.Hidden = true;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 156f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});

		}

		public void ShowProductLiability()
		{
			consHeightVProductLiablity.Constant = 156f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 156f;
			vProductLiability.Hidden = false;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenProductLiability()
		{
			consHeightVProductLiablity.Constant = 0;
			vProductLiability.Hidden = true;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 156f;

			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddress()
		{

			consHeightVAddress.Constant = 200f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 200f;

			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
			vCPSameAddress.Hidden = false;
		}

		public void HidenAddress()
		{

			consHeightVAddress.Constant = 0f;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 200f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
			vCPSameAddress.Hidden = true;
		}

		public void AddMoreLicences()
		{
			consCPHeightNewLicences.Constant += 345f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
			tbCPNewLicences.ReloadData();
		}

		public void RemoveLicences()
		{
			consCPHeightNewLicences.Constant -= 345f;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
			tbCPNewLicences.ReloadData();
		}

		public void SetHightFirstTime()
		{
			nfloat totalHeight = 0;
			if (ViewModel.IsShowSameAsPersonal == false)
			{
				consHeightVCompany.Constant = 1435f;
			}
			else
			{

				totalHeight += 200f;
			}
			if (ViewModel.IsTickCompensation == true)
			{
				consHeightVCompany.Constant = 1435f;
			}
			else
			{
				//HidenWordsCompenstation();
				totalHeight += 106f;
			}
			if (ViewModel.IsTickPublicLiability == true)
			{
				consHeightVCompany.Constant = 1435f;
			}
			else
			{
				//HidenPublicLiability();
				totalHeight += 156f;
			}
			if (ViewModel.IsTickProductLiability == true)
			{
				consHeightVCompany.Constant = 1435f;
			}
			else
			{
				//HidenProductLiability();

				totalHeight += 156f;
			}
			consHeightVCompany.Constant = 1435f - totalHeight;
		}

		public void ShowAddWordCompenstation()
		{
			vAddWorksCompenstation.Hidden = false;
			consHeightVAddCompenstation.Constant = 295f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 295f;


			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HiddenAddWordCompenstation()
		{
			vAddWorksCompenstation.Hidden = true;
			consHeightVAddCompenstation.Constant = 0;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 295f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddPublicLiability()
		{
			vAddPublicLiability.Hidden = false;
			consHeighVAddPublicLiability.Constant = 345f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});

		}

		public void HiddenAddPublicLiabiliy()
		{
			vAddPublicLiability.Hidden = true;
			consHeighVAddPublicLiability.Constant = 0;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddProductLiability()
		{
			vAddProductLiability.Hidden = false;
			consHeightVAddProductLiability.Constant = 345f;
			consHeightVCompany.Constant = consHeightVCompany.Constant + 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HiddenAddProductLiability()
		{
			vAddProductLiability.Hidden = true;
			consHeightVAddProductLiability.Constant = 0f;
			consHeightVCompany.Constant = consHeightVCompany.Constant - 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowIncomeInsured()
		{
			vIncomeInsuredDetails.Hidden = false;
			consHeightVIncomeInsured.Constant = 106f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenIncomeInsured()
		{
			vIncomeInsuredDetails.Hidden = true;
			consHeightVIncomeInsured.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowFirstAid()
		{
			vFirstAidDetails.Hidden = false;
			consHeightVFirstAid.Constant = 106f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenFirstAid()
		{
			vFirstAidDetails.Hidden = true;
			consHeightVFirstAid.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddIncomeInsured()
		{
			vAddIncomeInsured.Hidden = false;
			consHeightVAddIncomeInsured.Constant = 295f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenAddIncomeInsured()
		{
			vAddIncomeInsured.Hidden = true;
			consHeightVAddIncomeInsured.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddFirstAid()
		{
			vAddFirstAid.Hidden = false;
			consHeightVAddFirstAid.Constant = 295f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenAddFirstAid()
		{
			vAddFirstAid.Hidden = true;
			consHeightVAddFirstAid.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowPerPublicLiablity()
		{
			vPerPublicLiabilityDetails.Hidden = false;
			consVPerPublicLiabilityDetails.Constant = 106f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenPerPublicLiability()
		{
			vPerPublicLiabilityDetails.Hidden = true;
			consVPerPublicLiabilityDetails.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddPerPublicLiability()
		{
			vAddPerPublicLiability.Hidden = false;
			consAddVPerPublicLiablity.Constant = 295f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenAddPerPublicLiability()
		{
			vAddPerPublicLiability.Hidden = true;
			consAddVPerPublicLiablity.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}
		#endregion
	}
}

