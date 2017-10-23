	using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class TourView : BaseView
	{
		public TourView() : base("TourView", null)
		{
		}



		#region ViewModel

		public new TourViewModel ViewModel
		{
			get
			{
				return base.ViewModel as TourViewModel;
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
			this.AdjustFontSize(this.View);

			var set = this.CreateBindingSet<TourView, TourViewModel>();

			set.Bind(btnSkip).To(vm => vm.SkipCommand);
			set.Bind(btnSkip).For("Title").To(vm => vm.ButtonTitle);
			set.Bind(pageControl).For(s => s.CurrentPage).To(vm => vm.CurrentPage).WithConversion("IntToNInterger");
			set.Apply();


			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			sclTour.DecelerationEnded -= sclTour_DecelerationEnded;
		}


		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			sclTour.DecelerationEnded += sclTour_DecelerationEnded;
		}

		void sclTour_DecelerationEnded(object sender, EventArgs e)
		{
			int page = (int)(sclTour.ContentOffset.X / sclTour.Frame.Width);
			ViewModel.CurrentPage = page;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

