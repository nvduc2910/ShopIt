using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.Models;
using UIKit;
using ShopIt.Core.ViewModels.Items;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class InviteEditProjectTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("InviteEditProjectTableViewCell");
		public static readonly UINib Nib;

		public PendingTradesItemViewModel Model { get; set; }

		static InviteEditProjectTableViewCell()
		{
			Nib = UINib.FromName("InviteEditProjectTableViewCell", NSBundle.MainBundle);
		}

		protected InviteEditProjectTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<InviteEditProjectTableViewCell, PendingTradesItemViewModel>();
				set.Bind(lbInviteName).To(vm => vm.PendingTrades.InviteeEmail);
				set.Bind(btnDeletePendingInvite).To(vm => vm.DeletePendingTradesCommand);
				set.Apply();
			});

		}

		public static InviteEditProjectTableViewCell Create()
		{
			return (InviteEditProjectTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
