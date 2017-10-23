using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using ShopIt.iOS.Views.TableCells;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableSources
{
	public class LicenceCertViewTableSource : MvxTableViewSource
	{

		private EditProfileView mEditProfileView { get; set; }
		private bool mIsEdit;

		public LicenceCertViewTableSource(UITableView tableView, bool isEdit, EditProfileView editProfile) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("LicenceCertTableViewCell", NSBundle.MainBundle), LicenceCertTableViewCell.Key);
			mIsEdit = isEdit;
			mEditProfileView = editProfile;
		}
		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			LicenceCertTableViewCell cell = (LicenceCertTableViewCell)tableView.DequeueReusableCell(LicenceCertTableViewCell.Key, indexPath);

			cell.Model = item as CertTypeItemViewModel;

			if (cell.Model.IsCanedit == true)
			{
				cell.BtnExpiryDate.UserInteractionEnabled = true;
				cell.TfTypeOfLicences.UserInteractionEnabled = true;
				cell.TfTypeOfLicences.ShouldReturn += (textField) =>
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
				cell.BtnExpiryDate.UserInteractionEnabled = false;
				cell.TfTypeOfLicences.UserInteractionEnabled = false;
			}

			return cell;
		}
	}
}
