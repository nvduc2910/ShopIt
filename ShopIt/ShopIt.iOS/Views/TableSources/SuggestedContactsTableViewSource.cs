using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ShopIt.iOS.Views.TableCells;
using ShopIt.Core.Models;

namespace ShopIt.iOS.Views.TableSources
{
	public class SuggestedContactsTableViewSource : MvxTableViewSource
	{
		public CreateProjectView CreateProjectView { get; set; }
		public EditProjectView EditProjectView { get; set; }

		public SuggestedContactsTableViewSource(UITableView tableView, CreateProjectView view) : base(tableView)
		{
			this.CreateProjectView = view;
			tableView.RegisterNibForCellReuse(UINib.FromName("SuggestedContactTableViewCell", NSBundle.MainBundle), SuggestedContactTableViewCell.Key);
		}

		public SuggestedContactsTableViewSource(UITableView tableView, EditProjectView view) : base(tableView)
		{
			this.EditProjectView = view;
			tableView.RegisterNibForCellReuse(UINib.FromName("SuggestedContactTableViewCell", NSBundle.MainBundle), SuggestedContactTableViewCell.Key);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			SuggestedContactTableViewCell cell = (SuggestedContactTableViewCell)tableView.DequeueReusableCell(SuggestedContactTableViewCell.Key, indexPath);

			cell.Model = item as Invite;
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			return cell;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 35f;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
			var cell = tableView.CellAt(indexPath) as SuggestedContactTableViewCell;
			if (cell != null)
			{
				if (CreateProjectView != null)
				{
					CreateProjectView.ViewModel.SelectSuggestedContactCommand.Execute(cell.Model);
					CreateProjectView.ViewModel.IsShowButtonTapInvite = false;
				}
				else if (EditProjectView != null)
					EditProjectView.ViewModel.SelectSuggestedContactCommand.Execute(cell.Model);
			}
		}
	}
}
