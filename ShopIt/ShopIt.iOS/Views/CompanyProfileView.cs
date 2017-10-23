using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using ShopIt.iOS;
using UIKit;
using ViewModels;
using ShopIt.iOS.Views.TableSources;
using Foundation;

namespace ShopIt.iOS.Views
{
	public partial class CompanyProfileView : BaseView, ICompanyProfileView
	{

		#region Variables

		private IntPtr handle;

		#endregion

		public CompanyProfileView() : base("CompanyProfileView", null)
		{
			
		}
		public new CreateCompanyProfileViewModel ViewModel

		{
			get

			{
				return base.ViewModel as CreateCompanyProfileViewModel;
			}
		}

		public override void ViewDidLoad()
		{

			base.ViewDidLoad();

			ViewModel.View = this;

			InitView();
			var set = this.CreateBindingSet<CompanyProfileView, CreateCompanyProfileViewModel>();

			set.Bind(btnNext).To(vm => vm.ShowNextCommand);
			set.Bind(lbTitileStep).To(vm => vm.TitleStep);
			set.Bind(btnNext).For("Title").To(vm => vm.TitleButtonNext);
			set.Bind(btnBack).To(vm => vm.ShowBackStepCommand);


			set.Bind(tfBusinessName).To(vm => vm.BusinessName);
			set.Bind(tfABN).To(vm => vm.ABN);
			set.Bind(tfPosition).To(vm => vm.JobPosition);
			set.Bind(tfUnit).To(vm => vm.Unit);
			set.Bind(tfStreetName).To(vm => vm.StreetName);
			set.Bind(tfState).To(vm => vm.State);
			set.Bind(tfPostcode).To(vm => vm.PostCode);
			set.Bind(btnCheckSameAs).For(s => s.Selected).To(vm => vm.IsTickSameAsPersonal);
			set.Bind(btnGST).To(vm => vm.TickGSTRegisteredCommand);
			set.Bind(btnCheckSameAs).To(vm => vm.TickAddressSameAsCommand);


			set.Bind(btnCompensation).For(s => s.Selected).To(vm => vm.IsTickCompensation);
			set.Bind(btnPublicLiability).For(s => s.Selected).To(vm => vm.IsTickPublicLiability);
			set.Bind(btnProductLiability).For(s => s.Selected).To(vm => vm.IsTickProductLiability);
			set.Bind(btnGST).For(s => s.Selected).To(vm => vm.IsTickGSTRegistered);


			set.Bind(btnCompensation).To(vm => vm.TickCompensationCommand);
			set.Bind(btnPublicLiability).To(vm => vm.TickPublicLiabilityCommand);
			set.Bind(btnProductLiability).To(vm => vm.TickProductLiabilityCommand);

			set.Bind(btnCompensationExpiry).To(vm => vm.CompensationCert.SetCompanyExpiryCommand);
			set.Bind(btnPublicLiabilityExpiry).To(vm => vm.PublicLiabilityCert.SetCompanyExpiryCommand);
			set.Bind(btnProductLiabilityExpiry).To(vm => vm.ProductLiabilityCert.SetCompanyExpiryCommand);
			set.Bind(btnGSTExpiry).To(vm => vm.GSTRegisteredCert.SetCompanyExpiryCommand);

			set.Bind(btnCompensationPhoto).To(vm => vm.CompensationCert.ChooseCertPhotoCommand);
			set.Bind(btnPublicLiabilityPhoto).To(vm => vm.PublicLiabilityCert.ChooseCertPhotoCommand);
			set.Bind(btnProductLiabilityPhoto).To(vm => vm.ProductLiabilityCert.ChooseCertPhotoCommand);
			set.Bind(btnTakeGSTPhoto).To(vm => vm.GSTRegisteredCert.ChooseCertPhotoCommand);

			set.Bind(tfCompensationExpiry).To(vm => vm.CompensationCert.ExpiryText);
			set.Bind(tfPublicLiabilityExpiry).To(vm => vm.PublicLiabilityCert.ExpiryText);
			set.Bind(tfProductLiabilityExpiry).To(vm => vm.ProductLiabilityCert.ExpiryText);

			set.Bind(tfDollarProduct).To(vm => vm.ProductLiabilityCert.AmountText);
			set.Bind(tfDollarPublic).To(vm => vm.PublicLiabilityCert.AmountText);


			set.Bind(ivCompensationPhoto).For(s => s.LocalPathImage).To(vm => vm.CompensationCert.CertItem.Document.LocalMedia);
			set.Bind(ivPublicLiabilityPhoto).For(s => s.LocalPathImage).To(vm => vm.PublicLiabilityCert.CertItem.Document.LocalMedia);
			set.Bind(ivProductLiabilityPhoto).For(s => s.LocalPathImage).To(vm => vm.ProductLiabilityCert.CertItem.Document.LocalMedia);

			set.Bind(btnCheckDisplay).To(vm => vm.TickDisplayCommand);
			set.Bind(btnVerify).To(vm => vm.TickVerifyCommand);

			set.Bind(btnCheckDisplay).For(s => s.Selected).To(vm => vm.IsCheckDisplay);
			set.Bind(btnVerify).For(s => s.Selected).To(vm => vm.IsCheckVerify);

			set.Bind(btnTapAdd).To(vm => vm.TapAddMoreLicenceCommand);

			var licenceTableViewSource = new LicencesCertTableViewSource(tbLicenceCompany, this, null);
			set.Bind(licenceTableViewSource).For(s => s.ItemsSource).To(vm => vm.LicenceCerts);
			tbLicenceCompany.Source = licenceTableViewSource;

			btnSetTime.TouchUpInside += BtnSaveTime_TouchUpInside;
			btnCancel.TouchUpInside += BtnCancel_TouchUpInside;

			set.Apply();

			// Perform any additional setup after loading the view, typically from a nib.
			handle = new IntPtr(1);
			tbLicenceCompany.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handle);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.//
		}

		public override void ViewWillAppear(bool animated)
		{
			sclMain.DecelerationEnded += SclMain_DecelerationEnded;
			base.ViewWillAppear(animated);
		}

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);
			if (parent == null)
			{
				tbLicenceCompany.RemoveObserver(this, "contentSize", handle);
			}
		}

		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (keyPath == "contentSize" && context == handle)
			{
				if (tbLicenceCompany.ContentSize.Height == 0)
				{
					consHeightVLicenceCompany.Constant = tbLicenceCompany.ContentSize.Height + 85;
					UIView.Animate(0.5, () =>
					{
						View.LayoutIfNeeded();
					});
				}
				else
				{
					consHeightVLicenceCompany.Constant = tbLicenceCompany.ContentSize.Height + 85;
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
			UpdateHightLight(page);
		}

		void BtnSaveTime_TouchUpInside(object sender, EventArgs e)
		{
			ViewModel.SelectedTimeCommand.Execute(dpkTime.Date.NSDateToDateTime());
		}

		void BtnCancel_TouchUpInside(object sender, EventArgs e)
		{
			setAnimationHidden(vChooseTime);
			setAnimationHidden(vPopupDateTime);
		}

		public void UpdateHightLight(int page)
		{
			vHighlightStep1.Layer.Opacity = 0.5f;
			vHightlightDone.Layer.Opacity = 0.5f;
			
			if (page == 0)
			{
				vHighlightStep1.Layer.Opacity = 1f;
			}
			else if (page == 1)
			{
				vHighlightStep1.Layer.Opacity = 1f;
				vHightlightDone.Layer.Opacity = 1f;
			}
		}

		#region Implement View

		public void InitView()
		{
			vAddGST.Hidden = true;
			vPopupDateTime.Layer.CornerRadius = 20f;
			vPopupDateTime.Layer.MasksToBounds = true;
			vPopupDateTime.ClipsToBounds = true;

			vHighlightStep1.Layer.CornerRadius = 3;
			vHighlightStep1.Layer.MasksToBounds = true;

			vHightlightDone.Layer.CornerRadius = 3;
			vHightlightDone.Layer.MasksToBounds = true;
			               
		}


		public void ShowAddGSTRegistered()
		{
			consVHeightGSTRegistered.Constant = 260f;
			vAddGST.Hidden = false;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenAddGSTRegisterd()
		{
			consVHeightGSTRegistered.Constant = 0f;
			vAddGST.Hidden = true;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddCompensationCert()
		{

			consHeightVWorkCompensation.Constant = 295f;
			vWorkCompensation.Hidden = false;
			UIView.Animate(0.5, () => {
				View.LayoutIfNeeded();
			});
		}

		public void HiddenAddCompensationCert()
		{
			consHeightVWorkCompensation.Constant = 0f;
			vWorkCompensation.Hidden = true;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddPublicLiabilityCert()
		{
			consHeightVPublicLiability.Constant = 345f;
			vPublicLiability.Hidden = false;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenAddPublicLiabilityCert()
		{
			consHeightVPublicLiability.Constant = 0f;
			vPublicLiability.Hidden = true;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddProductLiabilityCert()
		{
			consHeightVProductLiability.Constant = 345f;
			vProductLiability.Hidden = false;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenAddProductLiabilityCert()
		{
			consHeightVProductLiability.Constant = 0f;
			vProductLiability.Hidden = true;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void ShowAddMoreLicencesCert()
		{
			consHeightVLicenceCompany.Constant = consHeightVLicenceCompany.Constant + 345f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
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
		public void PanToPage(int page)
		{
			nfloat xOffet = sclMain.Frame.Width * (page - 1);
			sclMain.SetContentOffset(new CGPoint(xOffet, 0), true);

			UpdateHightLight(page - 1);
		}

		public void ShowAddBusinessAddress()
		{
			vBusinessAddress.Hidden = false;
			consHeightVBusinessAddress.Constant = 200f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void HidenBusinessAddress()
		{
			vBusinessAddress.Hidden = true;
			consHeightVBusinessAddress.Constant = 0f;
			UIView.Animate(0.5, () =>
			{
				View.LayoutIfNeeded();
			});
			
		}

		public void HideKeyboard()
		{
			View.EndEditing(true);
		}

		public void ScrollToBottom()
		{
			if (sclMainStep2.ContentSize.Height > sclMainStep2.Frame.Height)
			{
				nfloat yOffet = sclMainStep2.ContentSize.Height - sclMainStep2.Frame.Height;
				sclMainStep2.SetContentOffset(new CGPoint(0, yOffet), true);
			}
		}

		#endregion


	}
}

