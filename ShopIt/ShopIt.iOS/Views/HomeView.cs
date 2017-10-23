using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class HomeView : BaseView, IHomeView
	{
		#region TabBar

		BaseTabBarView tabBar;
		nfloat heightContent;

		void InitTabBar()
		{
			//nfloat heightTabBar = UIScreen.MainScreen.Bounds.Width / 414f * 54f;
			nfloat heightTabBar = 54f;
			heightContent = UIScreen.MainScreen.Bounds.Height - heightTabBar;

			tabBar = new BaseTabBarView();

			tabBar.ViewControllers = new UIViewController[]
			{
				tabBar.CreateTabFor(0, "", "", "", ViewModel.ProfileVM),
				tabBar.CreateTabFor(1, "", "", "", ViewModel.ProjectsVM),
				tabBar.CreateTabFor(2, "", "", "", ViewModel.AlertsVM)
			};

			tabBar.View.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, heightContent);
			tabBar.TabBar.Hidden = true;

			this.AddChildViewController(tabBar);
			this.View.InsertSubview(tabBar.View, 0);
		}

		#endregion

		public HomeView() : base("HomeView", null)
		{
		}

		#region ViewModel

		public new HomeViewModel ViewModel
		{
			get
			{
				return base.ViewModel as HomeViewModel;
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
			InitTabBar();

			ViewModel.View = this;

			var set = this.CreateBindingSet<HomeView, HomeViewModel>();
			set.Bind(btnTabProfile).To(vm => vm.SelectTabProfileCommand);
			set.Bind(btnTabProfile).For(b => b.Selected).To(vm => vm.IsTabProfileSelected);
			set.Bind(btnTabProjects).To(vm => vm.SelectTabProjectsCommand);
			set.Bind(btnTabProjects).For(b => b.Selected).To(vm => vm.IsTabProjectSelected);
			set.Bind(btnTabAlerts).To(vm => vm.SelectTabAlertsCommand);
			set.Bind(btnTabAlerts).For(b => b.Selected).To(vm => vm.IsTabAlertsSelected);
			set.Bind(lbAlertIndicator).To(vm => vm.AlertsVM.AlertIndicator);




			vBgAlertIndicator.Layer.CornerRadius = vBgAlertIndicator.Frame.Size.Height / 2;
			vBgAlertIndicator.Layer.MasksToBounds = true;

			set.Apply();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();
			tabBar.View.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, heightContent);
		}

		#region IHomeView

		public void SwitchToTab(HomeTab tab)
		{
			tabBar.SelectedIndex = (int)tab;
		}

		#endregion
	}
}

