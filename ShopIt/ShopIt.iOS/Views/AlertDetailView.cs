using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using ShopIt.iOS;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class AlertDetailView : BaseView, IAlertDetailsView
	{
		public AlertDetailView() : base("AlertDetailView", null)
		{
		}

		#region ViewModel

		public new AlertDetailViewModel ViewModel
		{
			get
			{
				return base.ViewModel as AlertDetailViewModel;
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

			ViewModel.View = this;
			var set = this.CreateBindingSet<AlertDetailView, AlertDetailViewModel>();

			set.Bind(btnBack).To(vm => vm.GoBackCommand);
			set.Bind(btnShare).To(vm => vm.ShareCommand);

			set.Bind(lbAlertTitle).To(vm => vm.AlertItem.Alert.Title);
			set.Bind(tvBody).To(vm => vm.AlertItem.Alert.Body);

			set.Apply();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		#region Implement IAlertDetailsView

		public void ShareAlert()
		{
			UIPasteboard.General.String = ViewModel.AlertItem.Alert.Body;
		}

		#endregion	
	}
}

