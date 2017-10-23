using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ViewModels.Items;
using ShopIt.iOS.Views.TableCells;

namespace ShopIt.iOS.Views.TableSources
{
	public class OperationalCertTableViewSource :  MvxTableViewSource 
	{
		private PersonalProfileView mPersonalProfileView { get; set; }
		private EditProfileView mEditProfileView { get; set; }

		public OperationalCertTableViewSource(UITableView tableView, PersonalProfileView personalProfileView, EditProfileView editProfileView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("OperationalCertTableViewCell", NSBundle.MainBundle), OperationalCertTableViewCell.Key);
			mPersonalProfileView = personalProfileView;
			mEditProfileView = editProfileView;
		}
		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			OperationalCertTableViewCell cell = (OperationalCertTableViewCell)tableView.DequeueReusableCell(OperationalCertTableViewCell.Key, indexPath);
			cell.Model = item as CertTypeItemViewModel;

			cell.TfCertificateExpiry.ShouldBeginEditing += (sdf) =>
			{
				return false;
			};
			cell.TfTypeOfCert.ShouldReturn += (textField) =>
			{
				if (mPersonalProfileView != null)
				{
					mPersonalProfileView.HideKeyboard();
				}
				if (mEditProfileView != null)
				{
					mEditProfileView.HidenKeyboard();
				}
				return false;
			};
			return cell;
		}
	}
}
