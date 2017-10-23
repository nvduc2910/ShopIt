using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using ShopIt.iOS;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views
{
	public partial class AddDescriptionView : BaseView
	{
		public AddDescriptionView() : base("AddDescriptionView", null)
		{
		}

		public new DescriptionProjectViewModel ViewModel
		{
			get
			{
				return base.ViewModel as DescriptionProjectViewModel;
			}
		}

		#region Observer

		NSObject mShowKeyBoardObserver, mHideKeyBoardObserver;

		#endregion

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			tvDescription.Placeholder = "Add description...";
			var set = this.CreateBindingSet<AddDescriptionView, DescriptionProjectViewModel>();
			set.Bind(tvDescription).To(vm => vm.Description);
			set.Bind(btnBack).To(vm => vm.GoBackCommand);
			set.Bind(btnSave).To(vm => vm.SaveCommand);
			set.Bind(lbCharacterNumber).To(vm => vm.LenghtCharactor);
			set.Apply();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			mShowKeyBoardObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardWillShow);
			mHideKeyBoardObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHide);
			tvDescription.BecomeFirstResponder();
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			if (mShowKeyBoardObserver != null)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(mShowKeyBoardObserver);
				mShowKeyBoardObserver = null;
			}

			if (mHideKeyBoardObserver != null)
			{
				NSNotificationCenter.DefaultCenter.RemoveObserver(mHideKeyBoardObserver);
				mHideKeyBoardObserver = null;
			}
		}

		#region Implement View

		void KeyboardWillShow(NSNotification obj)
		{
			var keyBoardSize = UIKeyboard.FrameEndFromNotification(obj);

			UIView.Animate(UIKeyboard.AnimationDurationFromNotification(obj), () =>
			 {
				consBottomVCharaterNumber.Constant = keyBoardSize.Height;
			});
		}

		void KeyboardWillHide(NSNotification obj)
		{
			UIView.Animate(UIKeyboard.AnimationDurationFromNotification(obj), () =>
			 {
				 consBottomVCharaterNumber.Constant = 0;
			 });
		}
		#endregion




	}
}

