using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using ShopIt.iOS;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class AlertInviteView : BaseView
	{
		public AlertInviteView() : base("AlertInviteView", null)
		{
		}

		#region ViewModel

		public new AlertInviteViewModel ViewModel
		{
			get
			{
				return base.ViewModel as AlertInviteViewModel;
			}
			set
			{
				base.ViewModel = value;
			}
		}

		#endregion

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var set = this.CreateBindingSet<AlertInviteView, AlertInviteViewModel>();

			set.Bind(btnBack).To(vm => vm.GoBackCommand);

			set.Bind(lbProjectTitle).To(vm => vm.AlertItem.Alert.AlertProjectHeading.Title);
			set.Bind(lbProjectDescription).To(vm => vm.AlertItem.Alert.AlertProjectHeading.Description);
			set.Bind(lbStage).To(vm => vm.AlertItem.Alert.AlertProjectHeading.Stage);

			set.Bind(lbCompany).To(vm => vm.AlertItem.Alert.AlertProjectHeading.ProjectOwner.CompanyName);
			set.Bind(lbAddress).To(vm => vm.AlertItem.Alert.AlertProjectHeading.ProjectOwner.Address);
			set.Bind(lbEmail).To(vm => vm.AlertItem.Alert.AlertProjectHeading.ProjectOwner.Email);
			set.Bind(lbMobile).To(vm => vm.AlertItem.Alert.AlertProjectHeading.ProjectOwner.Mobile);

			set.Bind(btnJoinProject).To(vm => vm.JoinProjectCommand);

			set.Apply();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

