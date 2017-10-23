using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;
using ViewModels;
using ShopIt.iOS.Views.TableSources;

namespace ShopIt.iOS.Views
{
	public partial class PersonalProfileView : BaseView, IPersonalProfileView
	{
		public PersonalProfileView() : base("PersonalProfileView", null)
		{
		}

		#region Variables

		private IntPtr handle;

		#endregion

		public new CreatePersonalProfileViewModel ViewModel
		{
			get
			{
				return base.ViewModel as CreatePersonalProfileViewModel;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.View = this;
			var set = this.CreateBindingSet<PersonalProfileView, CreatePersonalProfileViewModel>();

			//Hightlight
			set.Bind(ivHightlightStep1).For(s => s.Highlighted).To(vm => vm.IsStep1);
			set.Bind(ivHightlightStep2).For(s => s.Highlighted).To(vm => vm.IsStep2);
			set.Bind(ivHightlightStep3).For(s => s.Highlighted).To(vm => vm.IsStep3);
			set.Bind(ivHightlightStep4).For(s => s.Highlighted).To(vm => vm.IsStep4);

			//STEP 1
			set.Bind(lbStep).To(vm => vm.TitleStep);
			set.Bind(btnNextStep).To(vm => vm.ShowNextCommand);
			set.Bind(btnNextStep).For("Title").To(vm => vm.TitleButtonNext);
			set.Bind(tfFristName).To(vm => vm.FirstName);
			set.Bind(tfLastName).To(vm => vm.LastName);
			set.Bind(lbDOB).To(vm => vm.DOBText);
			set.Bind(lbDOBTemp).To(vm => vm.DOBTextTemp);
			set.Bind(tfMobileNumber).To(vm => vm.MobileNumber);
			set.Bind(tfUnit).To(vm => vm.UnitNumber);
			set.Bind(tfStreetName).To(vm => vm.StreetName);
			set.Bind(tfPostCode).To(vm => vm.PostCode);
			set.Bind(tfState).To(vm => vm.State);
			set.Bind(btnChooseDOB).To(vm => vm.DOBCommand);
			set.Bind(tfFullName).To(vm => vm.NOK);
			set.Bind(tfContactNumber).To(vm => vm.NOKPhone);

			//STEP 2
			set.Bind(tfCardExpriry).To(vm => vm.ConstructionCardItem.ExpiryText);
			set.Bind(tfCardNumber).To(vm => vm.ConstructionCardItem.CertItem.Identifier);
			set.Bind(btnCardExpiry).To(vm => vm.ConstructionCardItem.SetPersonalExpiryCommand);
			set.Bind(btnTakePhotoOfCard).To(vm => vm.ConstructionCardItem.ChooseCertPhotoCommand);

			//for Image Step 2 3 4

			set.Bind(ivCard).For(s => s.LocalPathImage).To(vm => vm.ConstructionCardItem.CertItem.Document.LocalMedia);
			set.Bind(ivLicense).For(s => s.LocalPathImage).To(vm => vm.TraceLicenseItem.CertItem.Document.LocalMedia);


			set.Bind(ivCard).For(s => s.Highlighted).To(vm => vm.ConstructionCardItem.IsShowOrignalImage);
			set.Bind(ivLicense).For(s => s.Highlighted).To(vm => vm.TraceLicenseItem.IsShowOrignalImage);

			//STEP 3
			set.Bind(btnTakePhotoOfLicence).To(vm => vm.TraceLicenseItem.ChooseCertPhotoCommand);
			set.Bind(tfTradeLicenceExpriry).To(vm => vm.TraceLicenseItem.ExpiryText);
			set.Bind(btnTradeLicence).To(vm => vm.TraceLicenseItem.SetPersonalExpiryCommand);
			set.Bind(tfTypeofTrade).To(vm => vm.TraceLicenseItem.CertItem.Descriptor);
			set.Bind(tfTradeLicenceNumber).To(vm => vm.TraceLicenseItem.CertItem.Identifier);

			//STEP 4
			set.Bind(btnExpiryInsured).To(vm => vm.IncomeInsured.SetPersonalExpiryCommand);
			set.Bind(tfExpiryIncomeInsured).To(vm => vm.IncomeInsured.ExpiryText);
			set.Bind(btnTakePhotoOfDocument).To(vm => vm.IncomeInsured.ChooseCertPhotoCommand);
			set.Bind(ivIncomeInsuredPhoto).For(s => s.LocalPathImage).To(vm => vm.IncomeInsured.CertItem.Document.LocalMedia);

			set.Bind(tfPublicLiabilityExpiry).To(vm => vm.PublicLiablityCert.ExpiryText);
			set.Bind(btnPublicLiabilityExpiry).To(vm => vm.PublicLiablityCert.SetPersonalExpiryCommand);
			set.Bind(ivPublicLiabilityPhoto).For(s => s.LocalPathImage).To(vm => vm.PublicLiablityCert.CertItem.Document.LocalMedia);
			set.Bind(btnTickPublicLiability).To(vm => vm.TickPublicLiabilityCommand);
			set.Bind(btnPublicLiablityExpiry).To(vm => vm.PublicLiablityCert.ChooseCertPhotoCommand);

			set.Bind(btnFirstAidExpiry).To(vm => vm.FirstAidCertified.SetPersonalExpiryCommand);
			set.Bind(tfFirstAidExpiry).To(vm => vm.FirstAidCertified.ExpiryText);
			set.Bind(btnFirstAidPhoto).To(vm => vm.FirstAidCertified.ChooseCertPhotoCommand);
			set.Bind(ivFirstAidPhoto).For(s => s.LocalPathImage).To(vm => vm.FirstAidCertified.CertItem.Document.LocalMedia);
			   
			//set.Bind(btnGSTExpiry).To(vm => vm.GSTRegistered.SetPersonalExpiryCommand);
			//set.Bind(tfGSTExpiry).To(vm => vm.GSTRegistered.ExpiryText);
			//set.Bind(btnTakePhotoGSTCert).To(vm => vm.GSTRegistered.ChooseCertPhotoCommand);


			set.Bind(btnVerify).To(vm => vm.VerifyCommand);
			set.Bind(btnTickIncomeInsured).To(vm => vm.TickIncomeInsuredCommand);
			set.Bind(btnCheckGSTRegistered).To(vm => vm.TickGSTRegisteredCommand);
			set.Bind(btnCheckFirstAidCertified).To(vm => vm.TickFirstAidCertCommand);
			set.Bind(btnAddMoreOperaCert).To(vm => vm.TapToAddOperationalCertCommand);
			set.Bind(btnBack).To(vm => vm.ShowBackStepCommand);
			set.Bind(btnCreateCompanyProfile).To(vm => vm.CreateCompanyProfileCommand);

			set.Bind(btnClose).To(vm => vm.CloseCommand);


			set.Bind(ivCheckIncomeInsured).For(s => s.Highlighted).To(vm => vm.IsTickIncomeInsured);
			set.Bind(ivTickFirstCert).For(s => s.Highlighted).To(vm => vm.IsTickFirstAid);
			set.Bind(ivGSTRegistered).For(s => s.Highlighted).To(vm => vm.IsTickGSTRegistered);
			set.Bind(ivTickPublicLiablity).For(s => s.Highlighted).To(vm => vm.IsTickPublicLiability);

			set.Bind(ivCheckVerify).For(s => s.Highlighted).To(vm => vm.IsVerify);

			var operationcalCertTableSource = new OperationalCertTableViewSource(tbOperationalCert, this, null);
			set.Bind(operationcalCertTableSource).For(s => s.ItemsSource).To(vm => vm.OperationalCerts);
			tbOperationalCert.Source = operationcalCertTableSource;

			btnTimerOutside.TouchUpInside += BtnTimerOutside_TouchUpInside;
			btnSaveTime.TouchUpInside += BtnSaveTime_TouchUpInside;

			btnCancel.TouchUpInside += BtnCancel_TouchUpInside;

			set.Apply();
			InitView();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		#region ViewWillAppear

		public override void ViewWillAppear(bool animated)
		{
			sclMain.DecelerationEnded += SclMain_DecelerationEnded;
			base.ViewWillAppear(animated);
		}

		public override void ViewWillDisappear(bool animated)
		{
			sclMain.DecelerationEnded -= SclMain_DecelerationEnded;
			base.ViewWillDisappear(animated);
		}

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);
			if (parent == null)
			{
				tbOperationalCert.RemoveObserver(this, "contentSize", handle);
			}
		}

		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (keyPath == "contentSize" && context == handle)
			{
				if (tbOperationalCert.ContentSize.Height == 0)
				{
					consHeightVOperationalCert.Constant = tbOperationalCert.ContentSize.Height + 85;
					UIView.Animate(0.5, () =>
					{
						View.LayoutIfNeeded();
					});
				}
				else
				{
					consHeightVOperationalCert.Constant = tbOperationalCert.ContentSize.Height + 85;
				}
			}
			else
			{
				base.ObserveValue(keyPath, ofObject, change, context);
			}
		}

		void SclMain_DecelerationEnded(object sender, EventArgs e)
		{
			int page = (int)(sclMain.ContentOffset.X / sclMain.Frame.Width);
			ViewModel.CurrentPage = page;
		}

		#endregion

		#region Init UI 

		public void InitView()
		{
			vFinish.Hidden = true;

			vPopupDateTime.Layer.CornerRadius = 20f;
			vPopupDateTime.Layer.MasksToBounds = true;
			vPopupDateTime.ClipsToBounds = true;

			handle = new IntPtr(1);
			tbOperationalCert.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handle);
		}

		#endregion

		#region HandleSetTimeEvent

		void BtnTimerOutside_TouchUpInside(object sender, EventArgs e)
		{

			vPopupDateTime.Hidden = true;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			}, () =>
			{
				vChooseTime.Hidden = true;
			});
		}

		void BtnCancel_TouchUpInside(object sender, EventArgs e)
		{
			setAnimationHidden(vChooseTime);
			setAnimationHidden(vPopupDateTime);
		}
		void BtnSaveTime_TouchUpInside(object sender, EventArgs e)
		{
			
			ViewModel.SelectedTimeCommand.Execute(dpkTime.Date.NSDateToDateTime());
		}
		#endregion

		#region Implement IPersonalProfileView
		public void ShowPopupFinish()
		{
			vFinish.Hidden = false;

			UIView.Animate(0.25, () =>
			{
				vFinish.Alpha = 1;
			});
		}

		public void HiddenPopupFinish()
		{
			vFinish.Hidden = true;
		}

		public void AddMoreOperationalCert()
		{
			//consHeightVOperationalCert.Constant += 345f;
		}

		public void PanToPage(int page)
		{
			nfloat xOffet = sclMain.Frame.Width * (page - 1);
			sclMain.SetContentOffset(new CGPoint(xOffet, 0), true);
		}

		public void ShowSetTimeDialog()
		{
			dpkTime.Date = (Foundation.NSDate)DateTime.Now;
			
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
			setAnimationHidden(vChooseTime, true);
			setAnimationHidden(vPopupDateTime, true);

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

		public void HideKeyboard()
		{
			View.EndEditing(true);
		}

		public byte[] GetByteImage(string path)
		{
			var image = UIImage.FromFile(path);
			return ExtensionsImage.ToNSData(image);
		}

		public void ScrollToBottom()
		{
			if (sclMainStep4.ContentSize.Height > sclMainStep4.Frame.Height)
			{
				nfloat yOffet = sclMainStep4.ContentSize.Height - sclMainStep4.Frame.Height;
				sclMainStep4.SetContentOffset(new CGPoint(0, yOffet), true);
			}
		}

		public void ShowIncomeInsured()
		{
			vIncomeInsured.Hidden = false;
			consHeightIncomeCert.Constant = 295f;

			UIView.Animate(0.25, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenIncomeInsured()
		{
			vIncomeInsured.Hidden = true;
			consHeightIncomeCert.Constant = 0f;
			UIView.Animate(0.25, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowFirstAid()
		{
			vAddFirstAid.Hidden = false;
			consHeightVFirstAid.Constant = 295f;
			UIView.Animate(0.25, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenFirstAid()
		{
			vAddFirstAid.Hidden = true;
			consHeightVFirstAid.Constant = 0f;
			UIView.Animate(0.25, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowPublicLiability()
		{
			consHeightVPublicLiability.Constant = 295f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenPublicLiability()
		{
			consHeightVPublicLiability.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		#endregion
	}
}

