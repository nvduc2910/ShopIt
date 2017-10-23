using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views.TableSources
{
	public class InviteEditProjectTableViewSource : MvxTableViewSource
	{
		public InviteEditProjectTableViewSource(UITableView tableView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("InviteEditProjectTableViewCell", NSBundle.MainBundle), InviteEditProjectTableViewCell.Key);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cell = (InviteEditProjectTableViewCell)tableView.DequeueReusableCell(InviteEditProjectTableViewCell.Key, indexPath);
			return cell;
		}
	}
}
