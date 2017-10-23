using System;

using UIKit;
using ShopIt.Core.ViewModels;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platform;
using ShopIt.Core.Services;

namespace ShopIt.iOS.Views
{
	public partial class MainView : BaseView, IMainView
	{
		#region Private

		private HomeView mHomeView;

		#endregion

		public MainView() : base("MainView", null)
		{
		}

		#region ViewModel

		public new MainViewModel ViewModel
		{
			get
			{
				return base.ViewModel as MainViewModel;
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

			#region Init View

			mHomeView = this.CreateViewControllerFor(ViewModel.HomeVM) as HomeView;
			mHomeView.View.Frame = UIScreen.MainScreen.Bounds;
			this.View.InsertSubview(mHomeView.View, 0);
			this.AddChildViewController(mHomeView);

			#endregion

			#region Binding
			var set = this.CreateBindingSet<MainView, MainViewModel>();
			set.Bind(btnCloseMenu).To(vm => vm.HideMenuCommand);

			set.Bind(btnTour).To(vm => vm.MenuVM.TourCommand);
			set.Bind(btnAbout).To(vm => vm.MenuVM.AboutCommand);
			set.Bind(btnSignout).To(vm => vm.MenuVM.SignOutCommand);
			set.Bind(btnLicenseSearch).To(vm => vm.MenuVM.LicenseSearchCommand);

			set.Apply();
			#endregion

			var a = TestConvert("1234");

		}

		public string TestConvert(string value)
		{
			int count = 0;
			var valueStr = value.Replace(".", string.Empty);
			string temp = valueStr;
			for (int i = valueStr.Length; i > 0; i--)
			{
				count++;
				if (count == 3)
				{
					temp = temp.Insert(i - 1, ".");
					count = 0;
				}
			}
			return temp;
			//string a = value.ToString().Insert(2, "sdf");
			//return a;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();
			mHomeView.View.Frame = UIScreen.MainScreen.Bounds;
		}

		public override void DidMoveToParentViewController(UIViewController parent)
		{
			base.DidMoveToParentViewController(parent);
			if (parent == null)
			{
				mHomeView.DidMoveToParentViewController(parent);
			}
		}
		#region RegisterNotification




		#endregion

		#region IMainView

		public void ShowMenu()
		{
			vBackground.Hidden = false;
			consTopMenu.Constant = 0;
			UIView.Animate(0.3, () =>
			{

				View.LayoutIfNeeded();

			});
		}

		public void HideMenu()
		{
			vBackground.Hidden = true;
			nfloat height = vMenu.Frame.Height;
			consTopMenu.Constant = -height;
			UIView.Animate(0.3, () =>
			{

				View.LayoutIfNeeded();

			});
		}

		#endregion
	}
}

