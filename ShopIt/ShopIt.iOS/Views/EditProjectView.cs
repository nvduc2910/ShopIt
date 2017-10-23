using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using ShopIt.iOS;
using UIKit;
using ViewModels;
using ShopIt.iOS.Views.TableSources;

namespace ShopIt.iOS.Views
{
	public partial class EditProjectView : BaseView, IEditProjectView
	{
		#region Variables

		private IntPtr handle;
		private NSObject mShowKeyBoardObserver, mHideKeyBoardObserver;

		#endregion

		public EditProjectView() : base("EditProjectView", null)
		{
		}
		public new EditProjectViewModel ViewModel
		{

			get
			{
				return base.ViewModel as EditProjectViewModel;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			ViewModel.View = this;

			InitView();

			var set = this.CreateBindingSet<EditProjectView, EditProjectViewModel>();
			set.Bind(tfProjectTitle).To(vm => vm.Title);
			set.Bind(btnClearTitle).To(vm => vm.ClearTitleCommand);

			set.Bind(lbDescription).To(vm => vm.Description);
			set.Bind(tfEmailAddress).To(vm => vm.EmailInvite);
			set.Bind(btnShowMore).To(vm => vm.ShowMoreCurrentTracesCommand);
			set.Bind(btnShowMore).For(s => s.Hidden).To(vm => vm.IsShowMoreButton).WithConversion("TrueFalse");

			set.Bind(btnTender).For(v => v.Selected).To(vm => vm.IsTenderStage);
			set.Bind(btnSite).For(v => v.Selected).To(vm => vm.IsSiteStage);
			set.Bind(btnTender).To(vm => vm.TenderCommand);
			set.Bind(btnSite).To(vm => vm.SiteCommand);

			var currentTracesTableSoure = new CurrentTradesTableViewSource(tbCurrentTraces, this);
			set.Bind(currentTracesTableSoure).For(s => s.ItemsSource).To(vm => vm.CurrentTrades);
			tbCurrentTraces.Source = currentTracesTableSoure;

			var inviteTableSource = new InviteEditProjectTableViewSource(tbInviteTraces);
			set.Bind(inviteTableSource).For(s => s.ItemsSource).To(vm => vm.InviteTrades);
			tbInviteTraces.Source = inviteTableSource;

			var contactSource = new SuggestedContactsTableViewSource(tbSuggestedContacts, this);
			set.Bind(contactSource).For(s => s.ItemsSource).To(vm => vm.SuggestedContacts);
			tbSuggestedContacts.Source = contactSource;

			set.Bind(tbSuggestedContacts).For(v => v.Hidden).To(vm => vm.ShowSuggestedContacts).WithConversion("TrueFalse");

			tfEmailAddress.ShouldReturn += tfEmailAddress_ShouldReturn;
			set.Bind(btnSendInvite).To(vm => vm.SendInviteCommand);

			set.Bind(btnEditDescription).To(vm => vm.EditDescriptionCommand);

			set.Bind(btnSaveProject).To(vm => vm.SaveAndUpdateCommand);

			set.Bind(btnDeleteProject).To(vm => vm.DeleteProjectCommand);

			set.Bind(btnClose).To(vm => vm.GoBackCommand);

			set.Apply();
		}

		void InitView()
		{
			handle = IntPtr.Zero;
			tbCurrentTraces.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handle);
		}

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);
			if (parent == null)
			{
				tbCurrentTraces.RemoveObserver(this, "contentSize", handle);
			}
		}

		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (keyPath == "contentSize" && context == handle)
			{
				this.TableInviteChangedSize();
			}
			else
			{
				base.ObserveValue(keyPath, ofObject, change, context);
			}
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			mShowKeyBoardObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, KeyboardWillShow);
			mHideKeyBoardObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, KeyboardWillHide);
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

		void KeyboardWillShow(NSNotification obj)
		{
			var keyBoardSize = UIKeyboard.FrameEndFromNotification(obj);

			UIView.Animate(UIKeyboard.AnimationDurationFromNotification(obj), () =>
			 {
				 bottomSpace.Constant = keyBoardSize.Height;
				 View.LayoutIfNeeded();
			 });
		}

		void KeyboardWillHide(NSNotification obj)
		{
			UIView.Animate(UIKeyboard.AnimationDurationFromNotification(obj), () =>
			 {
				 bottomSpace.Constant = 0;
				 View.LayoutIfNeeded();
			 });
		}

		void TableInviteChangedSize()
		{
			InvokeOnMainThread(() =>
			{
				consHeightVCurrentTraces.Constant = tbCurrentTraces.ContentSize.Height;
				tbCurrentTraces.LayoutIfNeeded();
			});
		}


		bool tfEmailAddress_ShouldReturn(UITextField textField)
		{
			ViewModel.SendInviteCommand.Execute();
			return true;
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		public void HideKeyboard()
		{
			View.EndEditing(true);
		}
	}
}

