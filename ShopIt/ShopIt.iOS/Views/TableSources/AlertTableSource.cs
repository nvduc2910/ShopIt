using System;
using System.Drawing;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using ShopIt.iOS.Views;
using ShopIt.iOS.Views.TableCells;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableSources
{
	public class AlertTableSource : MvxTableViewSource, IAlertTableViewCellDelegate
	{
		public AlertsView View;

		public AlertTableSource(UITableView tableView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("AlertTableViewCell", NSBundle.MainBundle), AlertTableViewCell.Key);
			tableView.CanCancelContentTouches = false;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = (AlertTableViewCell)tableView.DequeueReusableCell(AlertTableViewCell.Key, indexPath);

			cell.CreatePangesture();

			cell.Layer.BackgroundColor = UIColor.Gray.CGColor;

			cell.Model = item as AlertItemViewModel;
			cell.Delegate = this;

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
			if (View != null)
			{
				var cell = tableView.CellAt(indexPath) as AlertTableViewCell;
				View.ViewModel.SelectAlertItemCommand.Execute(cell.Model);
			}
		}

		public void DidShowAllButtons(AlertTableViewCell cell)
		{
			
		}
	}
}
