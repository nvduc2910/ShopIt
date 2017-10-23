using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.ViewModels.Items;
using ShopIt.iOS.Views;
using UIKit;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views.TableSources
{
	public class ProjectJoinedTableViewSource : MvxTableViewSource
	{
		public ProjectsView View { get; set; }

		public ProjectJoinedTableViewSource(UITableView tableView, ProjectsView view) : base(tableView)
		{
			this.View = view;
			tableView.RegisterNibForCellReuse(UINib.FromName("ProjectJoinedTableViewCell", NSBundle.MainBundle), ProjectJoinedTableViewCell.Key);
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			ProjectJoinedTableViewCell cell = (ProjectJoinedTableViewCell)tableView.DequeueReusableCell(ProjectJoinedTableViewCell.Key, indexPath);
			if (cell == null)
			{
				cell = ProjectJoinedTableViewCell.Create();
			}

			cell.Model = item as ProjectItemViewModel;


			return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
			if (View != null)
			{
				var cell = tableView.CellAt(indexPath) as ProjectJoinedTableViewCell;
				View.ViewModel.SelectProjectItemCommand.Execute(cell.Model);
			}
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 196f;
		}
	}
}
