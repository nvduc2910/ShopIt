using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ViewModels.Items;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views.TableSources
{
	public class OperationalCertViewTableViewSource : MvxTableViewSource
	{
		private bool mIsEdit;

		private EditProfileView mEditProfileView { get; set; }

		public OperationalCertViewTableViewSource(UITableView tableView, bool isEdit, EditProfileView editProfileView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("OperationalCertViewTableViewCell", NSBundle.MainBundle), OperationalCertViewTableViewCell.Key);
			mIsEdit = isEdit;
			mEditProfileView = editProfileView;
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			OperationalCertViewTableViewCell cell = (OperationalCertViewTableViewCell)tableView.DequeueReusableCell(OperationalCertViewTableViewCell.Key, indexPath);
			cell.Model = item as CertTypeItemViewModel;

			if (mIsEdit == true)
			{
				cell.BtnExpiry.UserInteractionEnabled = true;
				cell.TfType.UserInteractionEnabled = true;
				cell.BtnDelete.Hidden = false;
				cell.TfType.ShouldReturn += (textField) =>
				{
					if (mEditProfileView != null)
					{
						mEditProfileView.HidenKeyboard();
					}
					return false;
				};
			}
			else
			{
				cell.BtnExpiry.UserInteractionEnabled = false;
				cell.TfType.UserInteractionEnabled = false;
				cell.BtnDelete.Hidden = true;
			}
			return cell;
		}
	}
}
