using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.iOS;
using UIKit;
using ViewModels;
using ShopIt.iOS.Views.TableSources;
using Foundation;
using MvvmCross.Platform;
using ShopIt.Core.Services;
using System.Linq;
using CoreGraphics;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views
{
	public partial class CreateProjectView : BaseView, ICreateProjectView, IUIGestureRecognizerDelegate
	{
		public CreateProjectView() : base("CreateProjectView", null)
		{
		}

		public new CreateProjectViewModel ViewModel

		{
			get
			{
				return base.ViewModel as CreateProjectViewModel;
			}
		}

		#region Observer

		NSObject mShowKeyBoardObserver, mHideKeyBoardObserver;

		#endregion


		UITapGestureRecognizer tapScreen;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.View = this;

			tapScreen = new UITapGestureRecognizer(InviteCommand);
			tapScreen.Delegate = this;
			tapScreen.CancelsTouchesInView = false;

			var set = this.CreateBindingSet<CreateProjectView, CreateProjectViewModel>();

			set.Bind(btnAddDescription).To(vm => vm.AddDescriptionCommand);
			set.Bind(tfProjectName).To(vm => vm.Title);

			set.Bind(btnClear).To(vm => vm.ClearProjectNameCommand);
			set.Bind(btnClose).To(vm => vm.CloseCommand);
			set.Bind(tfEmailInvite).To(vm => vm.EmailInvite);

			set.Bind(lbDescription).To(vm => vm.Description);
			set.Bind(btnTender).To(vm => vm.TenderCommand);
			set.Bind(btnSite).To(vm => vm.SiteCommand);

			set.Bind(btnTender).For(v=>v.Selected).To(vm => vm.IsSelecteTender);
			set.Bind(btnSite).For(v => v.Selected).To(vm => vm.IsSelecteTender).WithConversion("TrueFalse");

			tfEmailInvite.ShouldReturn += TfEmailInvite_ShouldReturn;

			var inviteSource = new InviteCreateProjectTableViewSource(tbInvite);
			set.Bind(inviteSource).For(s => s.ItemsSource).To(vm => vm.Invites);
			tbInvite.Source = inviteSource;

			var contactSource = new SuggestedContactsTableViewSource(tbSuggestedContacts, this);
			set.Bind(contactSource).For(s => s.ItemsSource).To(vm => vm.SuggestedContacts);
			tbSuggestedContacts.Source = contactSource;

			set.Bind(tbSuggestedContacts).For(v => v.Hidden).To(vm => vm.ShowSuggestedContacts).WithConversion("TrueFalse");

			//set.Bind(ivTender).For(s => s.Highlighted).To(vm => vm.IsSelecteTender);
			//set.Bind(ivSite).For(s => s.Highlighted).To(vm => vm.IsSelecteTender).WithConversion("TrueFalse");

			set.Bind(btnSaveProject).To(vm => vm.SaveProjectCommand);

			set.Bind(lbLeghtTitle).To(vm => vm.LeghtTitle);

			tfProjectName.ShouldChangeCharacters += (textField, range, replacementString) =>
			{
				var newLength = textField.Text.Length + replacementString.Length - range.Length;
				return newLength <= 20;	
			};

			set.Apply();
		}

		void InviteCommand()
		{
			
			ViewModel.InviteCommand.Execute();

		}

		bool TfEmailInvite_ShouldReturn(UITextField textField)
		{
			ViewModel.InviteCommand.Execute();
			return true;
		}

		public override void ViewWillAppear(bool animated)
		{
			mShowKeyBoardObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, KeyboardWillShow);
			mHideKeyBoardObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHide);
			base.ViewWillAppear(animated);
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

		[Export("gestureRecognizer:shouldReceiveTouch:")]
		public bool ShouldReceiveTouch(UIGestureRecognizer recognizer, UITouch touch)
		{
			return !(touch.View.Superview is SuggestedContactTableViewCell);
		}

		void KeyboardWillShow(NSNotification obj)
		{
			View.AddGestureRecognizer(tapScreen);
		}

		void KeyboardWillHide(NSNotification obj)
		{
			View.RemoveGestureRecognizer(tapScreen);
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public void HidenKeyboard()
		{
			View.EndEditing(true);
		}
	}
}

