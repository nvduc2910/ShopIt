using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS
{
	public partial class LicencesTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("LicencesTableViewCell");
		public static readonly UINib Nib;

		public CertTypeItemViewModel Model { get; set; }


		public UIImageView IvPhotoLicences
		{
			get
			{
				return ivPhoto;
			}
		}

		public UITextField TfLicencesExpiry
		{
			get
			{
				return tfLicenceExpiry;
			}
		}

		public UITextField TfTypeOfLicences
		{
			get
			{
				return tfTypeOfLicence;
			}
		}

		static LicencesTableViewCell()
		{
			Nib = UINib.FromName("LicencesTableViewCell", NSBundle.MainBundle);
		}

		protected LicencesTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<LicencesTableViewCell, CertTypeItemViewModel>();

				set.Bind(btnLicenceExpiry).To(vm => vm.SetCompanyExpiryCommand);
				set.Bind(tfLicenceExpiry).To(vm => vm.ExpiryText);
				set.Bind(tfTypeOfLicence).To(vm => vm.CertItem.Descriptor);
				set.Bind(btnRemove).To(vm => vm.RemoveLicenceCertCommand);
				set.Bind(ivPhoto).For(s => s.Highlighted).To(vm => vm.IsShowOrignalImage);
				set.Bind(ivPhoto).For(s => s.LocalPathImage).To(vm => vm.CertItem.Document.LocalMedia);
				set.Bind(btnTakePhoto).To(vm => vm.ChooseCertPhotoCommand);
				set.Apply();

			});
			// Note: this .ctor should not contain any initialization logic.
		}

		public static LicencesTableViewCell Create()
		{
			return (LicencesTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
