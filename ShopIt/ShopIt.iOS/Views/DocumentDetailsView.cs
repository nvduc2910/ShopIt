using System;
using ShopIt.Core.ViewModels;
using UIKit;
using MvvmCross.Binding.BindingContext;
using System.Threading.Tasks;
using Foundation;

namespace ShopIt.iOS.Views
{
	public partial class DocumentDetailsView : BaseView, IUIScrollViewDelegate
	{
		public DocumentDetailsView() : base("DocumentDetailsView", null)
		{
		}

		#region ViewModel

		public new DocumentDetailsViewModel ViewModel
		{
			get
			{
				return base.ViewModel as DocumentDetailsViewModel;
			}
			set
			{
				base.ViewModel = value;
			}
		}
		UITapGestureRecognizer singleTap;
		#endregion

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			sclMain.Delegate = this;
			sclMain.MinimumZoomScale = 1.0f;
			sclMain.MaximumZoomScale = 3.0f;

			singleTap = new UITapGestureRecognizer(this.TapToChooseImage);

			ivPhotoDocument.UserInteractionEnabled = true;
			sclMain.CanCancelContentTouches = true;

			ivPhotoDocument.AddGestureRecognizer(singleTap);

			var set = this.CreateBindingSet<DocumentDetailsView, DocumentDetailsViewModel>();

			set.Bind(btnClose).To(vm => vm.GoBackCommand);
			set.Bind(btnDelete).To(vm => vm.DeleteCommand);

			set.Bind(btnDelete).For(s => s.Hidden).To(vm => vm.CanEdit).WithConversion("TrueFalse");

			set.Bind(ivPhotoDocument).For(s => s.LocalImage).To(vm => vm.LocalMedia).WithConversion("BytesToUIImage");
			set.Bind(ivPhotoDocument).For(s => s.ImageUrl).To(vm => vm.Media);

			set.Bind(lbTitile).To(vm => vm.Titile);
			set.Apply();
		}


		public override void ViewWillAppear(bool animated)
		{
			if (ViewModel.CanEdit == true)
			{
				if (ViewModel.LocalMedia != null)
				{
					ivPhotoDocument.Image = ExtensionsImage.ToImage(ViewModel.LocalMedia);
				}
			}
			base.ViewWillAppear(animated);
		}
			

		public void TapToChooseImage(UITapGestureRecognizer tap)
		{
			ViewModel.EditCommand.Execute();
		}

		[Export("viewForZoomingInScrollView:")]
		public UIView ViewForZoomingInScrollView(UIScrollView scrollView)
		{
			return ivPhotoDocument;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

