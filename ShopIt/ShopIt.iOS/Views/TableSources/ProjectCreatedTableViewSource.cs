using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.Models;
using ShopIt.Core.ViewModels.Items;
using ShopIt.iOS.Views;
using UIKit;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views.TableSources
{
	public class ProjectCreatedTableViewSource : MvxTableViewSource
	{
		public ProjectsView View { get; set; } 
		
		public ProjectCreatedTableViewSource(UITableView tableView, ProjectsView view) : base(tableView)
		{
			this.View = view;
			tableView.RegisterNibForCellReuse(UINib.FromName("ProjectCreateTableViewCell", NSBundle.MainBundle), ProjectCreateTableViewCell.Key);
		}
		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			ProjectCreateTableViewCell cell = (ProjectCreateTableViewCell)tableView.DequeueReusableCell(ProjectCreateTableViewCell.Key, indexPath);
			if (cell == null)
			{
				cell = ProjectCreateTableViewCell.Create();
			}

			cell.Model = item as ProjectItemViewModel;

			return cell;
		}
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			base.RowSelected(tableView, indexPath);
			if (View != null)
			{
				var cell = tableView.CellAt(indexPath) as ProjectCreateTableViewCell;
				View.ViewModel.SelectProjectItemCommand.Execute(cell.Model);
			}
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 151f;
		}
	}
}
