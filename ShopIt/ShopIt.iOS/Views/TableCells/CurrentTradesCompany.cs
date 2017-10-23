using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.Models;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class CurrentTradesCompany : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("CurrentTradesCompany");
		public static readonly UINib Nib;

		public CurrentTradesItemViewModel Model { get; set; }

		static CurrentTradesCompany()
		{
			Nib = UINib.FromName("CurrentTradesCompany", NSBundle.MainBundle);
		}

		protected CurrentTradesCompany(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<CurrentTradesCompany, CurrentTradesItemViewModel>();

				set.Bind(lbInviteeName).To(vm => vm.CurrentTrades.DisplayName);
				set.Bind(btnDeleteInvitee).To(vm => vm.DeleteCurrentTradesCommand);

				set.Apply();
			});
		}
	}
}
