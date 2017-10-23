using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views.TableSources
{
	public class CurrentTradesTableViewSource : MvxTableViewSource
	{

		public EditProjectView View { get; set; }

		public CurrentTradesTableViewSource(UITableView tableView, EditProjectView view) : base(tableView)
		{
			this.View = view;
			tableView.RegisterNibForCellReuse(UINib.FromName("CurrentTradesCompany", NSBundle.MainBundle), CurrentTradesCompany.Key);

		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			CurrentTradesCompany cell = (CurrentTradesCompany)tableView.DequeueReusableCell(CurrentTradesCompany.Key, indexPath);
			return cell;
		}
	}
}
