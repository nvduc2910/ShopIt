using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.Models;
using UIKit;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class InviteViewProjectTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("InviteViewProjectTableViewCell");
		public static readonly UINib Nib;
		public Invite Model { get; set; }

		public UIImageView IvStatus
		{
			get
			{
				return ivStatus;
			}
		}

		static InviteViewProjectTableViewCell()
		{
			Nib = UINib.FromName("InviteViewProjectTableViewCell", NSBundle.MainBundle);
		}

		protected InviteViewProjectTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<InviteViewProjectTableViewCell, Invite>();
				set.Bind(lbInviteName).To(vm => vm.DisplayName);
				set.Bind(ivStatus).For(v => v.Highlighted).To(vm => vm.Alert);
				set.Bind(ivViewProfile).For(s => s.Hidden).To(vm => vm.ProfileComplete).WithConversion("TrueFalse");

				set.Bind(ivStatus).For(v => v.Hidden).To(vm => vm.Pending);
				set.Bind(lbPending).For(v => v.Hidden).To(vm => vm.Pending).WithConversion("Visibility");

				set.Apply();

			});
		}
	}
}
