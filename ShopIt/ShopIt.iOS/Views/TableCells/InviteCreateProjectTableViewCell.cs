using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.Models;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class InviteCreateProjectTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("InviteCreateProjectTableViewCell");
		public static readonly UINib Nib;


		public InviteCreateProjectItemViewModel Model { get; set; }

		static InviteCreateProjectTableViewCell()
		{
			Nib = UINib.FromName("InviteCreateProjectTableViewCell", NSBundle.MainBundle);

		}

		protected InviteCreateProjectTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<InviteCreateProjectTableViewCell, InviteCreateProjectItemViewModel>();
				set.Bind(lbEmailName).To(vm => vm.Invite.InviteeEmail);
				set.Bind(btnRemoveInvite).To(vm => vm.RemoveInviteCommand);

				set.Apply();
			});

			// Note: this .ctor should not contain any initialization logic.
		}

		public static InviteCreateProjectTableViewCell Create()
		{

			return (InviteCreateProjectTableViewCell)Nib.Instantiate(null, null)[0];
		}

	}
}
