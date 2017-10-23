using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views.TableSources
{
	public class InviteCreateProjectTableViewSource : MvxTableViewSource
	{
		

		public InviteCreateProjectTableViewSource(UITableView tableView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("InviteCreateProjectTableViewCell", NSBundle.MainBundle), InviteCreateProjectTableViewCell.Key);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			InviteCreateProjectTableViewCell cell = (InviteCreateProjectTableViewCell)tableView.DequeueReusableCell(InviteCreateProjectTableViewCell.Key, indexPath);
			return cell;
		}
	}
}
