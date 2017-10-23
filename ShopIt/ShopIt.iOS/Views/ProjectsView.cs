using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using UIKit;
using ShopIt.iOS.Views.TableSources;

namespace ShopIt.iOS.Views
{
	public partial class ProjectsView : BaseView
	{
		#region Variables

		private IntPtr handleProjectCreated;
		private IntPtr handleProjectJoined;
		private UIRefreshControl mProjectRefresh;

		#endregion

		public ProjectsView() : base("ProjectsView", null)
		{
		}

		#region ViewModel

		public new ProjectsViewModel ViewModel
		{
			get
			{
				return base.ViewModel as ProjectsViewModel;
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

			InitView();

			var set = this.CreateBindingSet<ProjectsView, ProjectsViewModel>();
			set.Bind(btnAddProject).To(vm => vm.AddProjectCommand);
			set.Bind(btnMenu).To(vm => vm.ShowMenuCommand);
			var projectCreated = new ProjectCreatedTableViewSource(tbProjectCreated, this);
			set.Bind(projectCreated).For(s => s.ItemsSource).To(vm => vm.ProjectsCreated);
			tbProjectCreated.Source = projectCreated;
			var projectJoined = new ProjectJoinedTableViewSource(tbProjectJoined, this);
			set.Bind(projectJoined).For(s => s.ItemsSource).To(vm => vm.ProjectsJoined);
			tbProjectJoined.Source = projectJoined;

			set.Bind(lbEmptyProjectJoined).For(v => v.Hidden).To(vm => vm.HaveProjectsJoined);
			set.Bind(lbEmptyProjectCreated).For(v => v.Hidden).To(vm => vm.HaveProjectsCreated);

			set.Apply();
		}

		public override void DidMoveToParentViewController(UIViewController parent)
		{
			base.DidMoveToParentViewController(parent);
			if (parent == null)
			{
				Console.Write("Destroy project");

			}
		}

		#region InitView

		public void InitView()
		{
			tbProjectCreated.BackgroundColor = UIColor.Clear;
			tbProjectJoined.BackgroundColor = UIColor.Clear;

			mProjectRefresh = new UIRefreshControl();
			mProjectRefresh.BackgroundColor = UIColor.Clear;
			mProjectRefresh.TintColor = UIColor.Black;
			mProjectRefresh.AddTarget(RefreshProjects, UIControlEvent.ValueChanged);

			sclMain.AlwaysBounceVertical = true;
			sclMain.AddSubview(mProjectRefresh);

			handleProjectCreated = new IntPtr(1);
			tbProjectCreated.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handleProjectCreated);

			handleProjectJoined = new IntPtr(2);
			tbProjectJoined.AddObserver(this, "contentSize", Foundation.NSKeyValueObservingOptions.New, handleProjectJoined);
		}

		private void RefreshProjects(object sender, EventArgs e)
		{
			ViewModel.LoadData();

			if (mProjectRefresh != null)
			{
				string title = string.Format("Last update : {0}", DateTime.Now.ToString());
				NSDictionary attrsDictionary = NSDictionary.FromObjectAndKey(UIColor.Black, UIStringAttributeKey.ForegroundColor);
				mProjectRefresh.AttributedTitle = new NSAttributedString(title, new UIStringAttributes(attrsDictionary));
				mProjectRefresh.EndRefreshing();
			}
		}
		#endregion

		#region ViewWillAppear

		public override void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
		{
			if (keyPath == "contentSize" && context == handleProjectCreated)
			{
				this.TableProjectCreatedChangedSize();
			}
			else if (keyPath == "contentSize" && context == handleProjectJoined)
			{
				this.TableProjectJoinedChangedSize();
			}
			else
			{
				base.ObserveValue(keyPath, ofObject, change, context);
			}
		}

		public override void WillMoveToParentViewController(UIViewController parent)
		{
			base.WillMoveToParentViewController(parent);
			if (parent == null)
			{
				tbProjectCreated.RemoveObserver(this, "contentSize", handleProjectCreated);
				tbProjectJoined.RemoveObserver(this, "contentSize", handleProjectJoined);
			}
		}

		void TableProjectCreatedChangedSize()
		{
			InvokeOnMainThread(() =>
			{
				consHeightVProjectCreated.Constant = (tbProjectCreated.ContentSize.Height) + 50;
				tbProjectCreated.LayoutIfNeeded();
			});
		}

		void TableProjectJoinedChangedSize()
		{
			InvokeOnMainThread(() =>
			{
				conHeightVProjectJoined.Constant = (tbProjectJoined.ContentSize.Height) + 50;
				tbProjectJoined.LayoutIfNeeded();
			});
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		#endregion
	}
}

