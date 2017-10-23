using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using ShopIt.iOS;
using ShopIt.iOS.Views.TableCells;
using UIKit;
using ViewModels.Items;

namespace ShopIt.iOS.Views.TableSources
{
	public class LicencesCertTableViewSource : MvxTableViewSource
	{

		private CompanyProfileView mCompanyView { get; set; }
		private EditProfileView mEditProfileView { get; set; }

		public LicencesCertTableViewSource(UITableView tableView, CompanyProfileView companyView, EditProfileView editProfileView) : base(tableView)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("LicencesTableViewCell", NSBundle.MainBundle), LicencesTableViewCell.Key);
			mCompanyView = companyView;
			mEditProfileView = editProfileView;
			
		}

		protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
		{
			LicencesTableViewCell cell = (LicencesTableViewCell)tableView.DequeueReusableCell(LicencesTableViewCell.Key, indexPath);
			cell.Model = item as CertTypeItemViewModel;
			cell.TfLicencesExpiry.ShouldBeginEditing += (textField) =>
			{
				return false;
			};
			cell.TfTypeOfLicences.ShouldReturn += (textField) =>
			{
				if (mCompanyView != null)
				{
					mCompanyView.HideKeyboard();
				}
				if (mEditProfileView != null)
				{
					mEditProfileView.HidenKeyboard();
				}
				return false;
			};
			//cell.IvPhotoLicences.Image = null;
			//if (cell.Model.CertItem.Document != null)
			//{
			//	cell.IvPhotoLicences.Image = UIImage.FromFile(cell.Model.CertItem.Document.LocalMedia);
			//}
			//else
			//{
			//	cell.IvPhotoLicences.Image = UIImage.FromBundle("ic_add_photo");
			//}
			return cell;
		}
	}
}
