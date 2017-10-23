using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class LicenceCertTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("LicenceCertTableViewCell");
		public static readonly UINib Nib;

		public CertTypeItemViewModel Model { get; set; }

		public UIButton BtnExpiryDate
		{
			get
			{
				return btnExpiryDate;
			}
		}

		public UITextField TfTypeOfLicences
		{
			get
			{
				return tfTypeOfLicence;
			}
		}

		static LicenceCertTableViewCell()
		{
			Nib = UINib.FromName("LicenceCertTableViewCell", NSBundle.MainBundle);
		}

		protected LicenceCertTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<LicenceCertTableViewCell, CertTypeItemViewModel>();

				set.Bind(btnViewDocument).To(vm => vm.ViewDocumentCommand);
				set.Bind(tfTypeOfLicence).To(vm => vm.CertItem.Descriptor);
				set.Bind(lbExpiryDate).To(vm => vm.ExpiryText);
				set.Bind(btnExpiryDate).To(vm => vm.EditExpiryCommand);
				set.Bind(ivStatus).For(s => s.Highlighted).To(vm => vm.IsOverdue);

				set.Bind(btnDeleteCert).To(vm => vm.DeleteCompanyLicenceCertCommand);
				set.Bind(btnDeleteCert).For(v => v.Hidden).To(vm => vm.IsCanedit).WithConversion("Visibility");

				set.Apply();

			});
		}

		public static LicenceCertTableViewCell Create()
		{
			return (LicenceCertTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
