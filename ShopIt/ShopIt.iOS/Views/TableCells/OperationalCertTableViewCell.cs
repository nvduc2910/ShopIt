using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using ShopIt.iOS.Controls;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class OperationalCertTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("OperationalCertTableViewCell");
		public static readonly UINib Nib;

		public CertTypeItemViewModel Model { get; set; }

		public BindableUrlUIImageView IvPhoto
		{
			get
			{
				return ivOperationalCer;
			}
		}

		public UITextField TfCertificateExpiry
		{
			get
			{
				return tfCertificateExpiry;
			}
		}

		public UITextField TfTypeOfCert
		{
			get
			{
				return tfTypeOfCert;
			}
		}

		static OperationalCertTableViewCell()
		{
			Nib = UINib.FromName("OperationalCertTableViewCell", NSBundle.MainBundle);

		}

		protected OperationalCertTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<OperationalCertTableViewCell, CertTypeItemViewModel>();
				set.Bind(btnCerExpiry).To(vm => vm.SetPersonalExpiryCommand);
				set.Bind(tfCertificateExpiry).To(vm => vm.ExpiryText);
				set.Bind(tfTypeOfCert).To(vm => vm.CertItem.Descriptor);
				set.Bind(ivOperationalCer).For(s => s.Highlighted).To(vm => vm.IsShowOrignalImage);
				set.Bind(ivOperationalCer).For(s => s.LocalPathImage).To(vm => vm.CertItem.Document.LocalMedia);
				set.Bind(btnTakePhotoOperationalCer).To(vm => vm.ChooseCertPhotoCommand);
				set.Bind(btnRemove).To(vm => vm.RemoveOperationalCertCommand);
				set.Apply();
			});

		}
		public static OperationalCertTableViewCell Create()
		{
			return (OperationalCertTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
