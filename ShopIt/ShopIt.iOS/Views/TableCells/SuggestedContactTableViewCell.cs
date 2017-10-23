using System;

using Foundation;
using ShopIt.Core.Models;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class SuggestedContactTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("SuggestedContactTableViewCell");
		public static readonly UINib Nib;

		public Invite Model { get; set; }

		static SuggestedContactTableViewCell()
		{
			Nib = UINib.FromName("SuggestedContactTableViewCell", NSBundle.MainBundle);
		}

		protected SuggestedContactTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<SuggestedContactTableViewCell, Invite>();
				set.Bind(lbEmail).To(vm => vm.InviteeEmail);
				set.Apply();
			});
		}

		public static SuggestedContactTableViewCell Create()
		{
			return (SuggestedContactTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
