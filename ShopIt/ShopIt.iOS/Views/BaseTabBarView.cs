using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Views;
using UIKit;

namespace ShopIt.iOS.Views
{
	public class BaseTabBarView : MvxTabBarViewController
	{
		public BaseTabBarView()
			: base()
		{
		}

		public UIViewController CreateTabFor(int index, string title, string image, string selectedImage, IMvxViewModel viewModel)
		{
			var viewController = this.CreateViewControllerFor(viewModel) as UIViewController;
			var tabbarItem = new UITabBarItem(title, UIImage.FromBundle(image), UIImage.FromFile(selectedImage));
			tabbarItem.Tag = index;
			viewController.TabBarItem = tabbarItem;
			return viewController;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			if (ViewModel == null)
				return;

			this.AutomaticallyAdjustsScrollViewInsets = false;
		}

		public override void DidMoveToParentViewController(UIViewController parent)
		{
			if (parent == null && this.ViewControllers != null)
			{
				foreach (var view in this.ViewControllers)
				{
					view.DidMoveToParentViewController(parent);
				}
			}
			base.DidMoveToParentViewController(parent);
		}

		public void AdjustFontSize(UIView view)
		{
			foreach (var subview in view.Subviews)
			{
				if (subview.Subviews != null && subview.Subviews.Length != 0)
				{
					AdjustFontSize(subview);
				}
				else
				{
					if (subview is UIButton)
					{
						(subview as UIButton).Font = FontHelper.AdjustFontSize((subview as UIButton).Font);
					}
					else if (subview is UILabel)
					{
						(subview as UILabel).Font = FontHelper.AdjustFontSize((subview as UILabel).Font);
					}
				}
			}
		}
	}
}
