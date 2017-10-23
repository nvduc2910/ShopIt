using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using UIKit;
using CoreGraphics;

namespace ShopIt.iOS.Views
{
	public partial class AboutView : BaseView
	{
		public AboutView() : base("AboutView", null)
		{
		}

		public new AboutViewModel ViewModel
		{
			get
			{
				return base.ViewModel as AboutViewModel;
			}
		}

		private bool mIsFirstLoad = true;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var set = this.CreateBindingSet<AboutView, AboutViewModel>();

			set.Bind(btnBack).To(vm => vm.GoBackCommand);
			set.Bind(btnPP).To(vm => vm.PrivacyPolicyCommand);
			set.Bind(btnToS).To(vm => vm.TermsOfServiceCommand);

			set.Apply();
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			if (mIsFirstLoad)
			{
				mIsFirstLoad = false;
			}
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();
			if (mIsFirstLoad)
			{
				tvAbout.SetContentOffset(CGPoint.Empty, false);
			}
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.

		}
	}
}

