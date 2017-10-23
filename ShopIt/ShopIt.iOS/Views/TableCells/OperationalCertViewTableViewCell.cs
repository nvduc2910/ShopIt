using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.Models;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class OperationalCertViewTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("OperationalCertViewTableViewCell");
		public static readonly UINib Nib;

		public CertTypeItemViewModel Model { get; set; }


		public UITextField TfType
		{
			get
			{
				return tfType;
			}
		}

		public UIButton BtnExpiry
		{
			get
			{
				return btnExpiryDate;
			}
		}

		public UIButton BtnDelete
		{
			get
			{
				return btnDeleteCert;	
			}
		}

		static OperationalCertViewTableViewCell()
		{
			Nib = UINib.FromName("OperationalCertViewTableViewCell", NSBundle.MainBundle);
		}

		protected OperationalCertViewTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<OperationalCertViewTableViewCell, CertTypeItemViewModel>();
				set.Bind(tfType).To(vm => vm.CertItem.Descriptor);
				set.Bind(lbExpiry).To(vm => vm.ExpiryText);
				set.Bind(btnExpiryDate).To(vm => vm.SetPersonalExpiryCommand);
				set.Bind(btnViewCertificate).To(vm => vm.ViewDocumentCommand);
				set.Bind(ivStatus).For(s => s.Highlighted).To(vm => vm.IsOverdue);
				set.Bind(btnDeleteCert).To(vm => vm.DeleteOperationalCertCommand);
				set.Apply();

			});
		}
		public static OperationalCertViewTableViewCell Create()
		{
			return (OperationalCertViewTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
