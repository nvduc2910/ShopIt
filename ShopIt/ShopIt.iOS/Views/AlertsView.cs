using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using UIKit;
using ShopIt.iOS.Views.TableSources;
using Foundation;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views
{
	public partial class AlertsView : BaseView, IAlertsView
	{
		public AlertsView() : base("AlertsView", null)
		{
		}

		#region Variables

		private UIRefreshControl mAlertRefresh;

		#endregion

		#region ViewModel

		public new AlertsViewModel ViewModel
		{
			get
			{
				return base.ViewModel as AlertsViewModel;
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

			ViewModel.View = this;

			var set = this.CreateBindingSet<AlertsView, AlertsViewModel>();

			var alertSource = new AlertTableSource(tbAlert);
			alertSource.View = this;

			set.Bind(alertSource).For(s => s.ItemsSource).To(vm => vm.AlertItems);
			set.Bind(btnMenu).To(vm => vm.ShowMenuCommand);

			set.Bind(lbTitle).To(vm => vm.Title);
			set.Bind(tfSearch).To(vm => vm.TextSearch);
			set.Bind(btnClearSearch).To(vm => vm.ClearSearchCommand);
			set.Bind(btnClearSearch).For(v => v.Hidden).To(vm => vm.HasClearSearch).WithConversion("Visibility");

			tfSearch.ShouldReturn += (textField) =>
			{
				View.EndEditing(true);
				return true;
			};

			tbAlert.Source = alertSource;

			set.Apply();
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();

			// Release any cached data, images, etc that aren't in use.
		}

		#region Init View

		public void InitView()
		{
			vSearch.Layer.CornerRadius = 20f;

			mAlertRefresh = new UIRefreshControl();
			mAlertRefresh.BackgroundColor = UIColor.Clear;
			mAlertRefresh.TintColor = UIColor.Black;
			mAlertRefresh.AddTarget(RefreshAlerts, UIControlEvent.ValueChanged);

			tbAlert.AddSubview(mAlertRefresh);
			tbAlert.AlwaysBounceVertical = true;

		}

		private void RefreshAlerts(object sender, EventArgs e)
		{
			ViewModel.LoadData();

			if (mAlertRefresh != null)
			{
				string title = string.Format("Last update : {0}", DateTime.Now.ToString());
				NSDictionary attrsDictionary = NSDictionary.FromObjectAndKey(UIColor.Black, UIStringAttributeKey.ForegroundColor);
				mAlertRefresh.AttributedTitle = new NSAttributedString(title, new UIStringAttributes(attrsDictionary));
				mAlertRefresh.EndRefreshing();
			}
		}

		public void CloseOptionsOnAlert(int idx)
		{
			var cell = tbAlert.CellAt(NSIndexPath.FromRowSection(idx, 0)) as AlertTableViewCell;
			cell.ResetConstraintsConstantsToZero(true);
		}
		#endregion
	}
}

