using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ShopIt.iOS.Views.TableCells;
using ShopIt.Core.Models;

namespace ShopIt.iOS.Views.TableSources
{
	public class InviteViewProjectTableViewSource : MvxTableViewSource
	{
		public ViewProjectView View { get; set; }

		public InviteViewProjectTableViewSource(UITableView tableView, ViewProjectView view) : base(tableView)
		{
			this.View = view;
			tableView.RegisterNibForCellReuse(UINib.FromName("InviteViewProjectTableViewCell", NSBundle.MainBundle), InviteViewProjectTableViewCell.Key);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			InviteViewProjectTableViewCell cell = (InviteViewProjectTableViewCell)tableView.DequeueReusableCell(InviteViewProjectTableViewCell.Key, indexPath);

			cell.Model = item as Invite;
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			cell.IvStatus.Highlighted = cell.Model.Alert ?? false;

			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
			var cell = tableView.CellAt(indexPath) as InviteViewProjectTableViewCell;
			if (cell != null)
				View.ViewModel.SelectInviteCommand.Execute(cell.Model);
		}
	}
}
