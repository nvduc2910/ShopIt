using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using ShopIt.iOS;
using UIKit;
using ViewModels;
using ShopIt.iOS.Views.TableSources;

namespace ShopIt.iOS.Views
{
	public partial class ViewProjectView : BaseView, IViewProject
	{
		public ViewProjectView() : base("ViewProjectView", null)
		{
		}

		#region Variables

		private IntPtr handle;

		#endregion

		public new ViewProjectViewModel ViewModel
		{

			get
			{
				return base.ViewModel as ViewProjectViewModel;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			InitView();
			ViewModel.View = this;
			var set = this.CreateBindingSet<ViewProjectView, ViewProjectViewModel>();
			set.Bind(lbTitleProject).To(vm => vm.ProjectName);
			set.Bind(lbDescription).To(vm => vm.Description);

			set.Bind(lbMobile).To(vm => vm.Project.Owner.Mobile);
			set.Bind(lbEmail).To(vm => vm.Project.Owner.Email);
			set.Bind(vLoading).For(s => s.Hidden).To(vm => vm.IsLoading).WithConversion("TrueFalse");
			set.Bind(lbAddress).To(vm => vm.Project.Owner.Address);
			set.Bind(lbSearchText).To(vm => vm.TextSearch);
			set.Bind(btnShowMore).To(vm => vm.ShowMoreInvitesCommand);
			set.Bind(btnShowMore).For(s => s.Hidden).To(vm => vm.IsShowMoreButton).WithConversion("TrueFalse");
			set.Bind(btnClearSearchText).To(vm => vm.ClearSearchTextCommand);
			set.Bind(btnEdit).To(vm => vm.EditProjectCommand);
			set.Bind(btnEdit).For(v=>v.Hidden).To(vm=>vm.CanEdit).WithConversion("TrueFalse");

			//set.Bind(lbCompanyName).To(vm => vm.OwnerName);
			//set.Bind(lbOwnerType).To(vm => vm.OwnerType);
			set.Bind(lbOwnerType).To(vm => vm.Owner);

			var inviteTableSource = new InviteViewProjectTableViewSource(tbInvites, this);
			set.Bind(inviteTableSource).For(s => s.ItemsSource).To(vm => vm.Invites);
			tbInvites.Source = inviteTableSource;

			set.Bind(btnBack).To(vm => vm.GoBackCommand);

			set.Apply();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
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

		void TableInviteChangedSize()
		{
			InvokeOnMainThread(() =>
			{
				
				consHeightVSearchInvitees.Constant = tbInvites.ContentSize.Height + 277;
				consHeigtVInvitee.Constant = tbInvites.ContentSize.Height;
				tbInvites.LayoutIfNeeded();

			});
		}

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);
			if (parent == null)
			{
				tbInvites.RemoveObserver(this, "contentSize", handle);
			}
		}

		#region Initview

		public void InitView()
		{
			vProjectCreator.Layer.CornerRadius = 10f;
			vProjectCreator.Layer.MasksToBounds = true;
			vSearch.Layer.CornerRadius = 20f;
			vSearch.Layer.MasksToBounds = true;
			vInvitee.Layer.CornerRadius = 10f;
			vInvitee.Layer.MasksToBounds = true;
			vInvitee.ClipsToBounds = true;

			handle = IntPtr.Zero;
			tbInvites.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handle);
		}

		public void ShowInvitee()
		{
			vSeachInvitee.Hidden = false;
			consHeightVSearchInvitees.Constant = 457.667f;
		}

		public void HidenInvitee()
		{
			vSeachInvitee.Hidden = true;
			consHeightVSearchInvitees.Constant = 0f;
		}


		#endregion



	}
}

