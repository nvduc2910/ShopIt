using System;
using System.IO;
using Foundation;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class LocalWebView : BaseView
	{
		public LocalWebView() : base("LocalWebView", null)
		{
		}

		public new LocalWebViewModel ViewModel
		{
			get
			{
				return base.ViewModel as LocalWebViewModel;
			}
			set
			{
				base.ViewModel = value;
			}
		}


		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var set = this.CreateBindingSet<LocalWebView, LocalWebViewModel>();

			set.Bind(btnBack).To(vm => vm.GoBackCommand);
			set.Bind(lbTitle).To(vm => vm.Title);

			set.Apply();

			string fileName = ViewModel.HTMLFile;// remember case-sensitive
			string localDocUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);

			NSUrlRequest request = new NSUrlRequest(new NSUrl(localDocUrl, false), NSUrlRequestCachePolicy.ReloadIgnoringLocalAndRemoteCacheData, 60);
			webView.LoadRequest(request);

			//			webView.LoadRequest(new NSUrlRequest(new NSUrl(localDocUrl, false)));
			webView.ScalesPageToFit = false;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillDisappear(bool animated)
		{
			MemoryUtils.ReleaseUIViewWithChildren(this.View);
			base.ViewWillDisappear(animated);
		}
	}
}

