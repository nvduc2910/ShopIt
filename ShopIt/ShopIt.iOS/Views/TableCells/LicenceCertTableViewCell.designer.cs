// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ShopIt.iOS.Views.TableCells
{
	[Register ("LicenceCertTableViewCell")]
	partial class LicenceCertTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnDeleteCert { get; set; }

		[Outlet]
		UIKit.UIButton btnExpiryDate { get; set; }

		[Outlet]
		UIKit.UIButton btnViewDocument { get; set; }

		[Outlet]
		UIKit.UIImageView ivStatus { get; set; }

		[Outlet]
		UIKit.UILabel lbExpiryDate { get; set; }

		[Outlet]
		UIKit.UITextField tfTypeOfLicence { get; set; }

		[Outlet]
		UIKit.UIView vBound { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnExpiryDate != null) {
				btnExpiryDate.Dispose ();
				btnExpiryDate = null;
			}

			if (btnViewDocument != null) {
				btnViewDocument.Dispose ();
				btnViewDocument = null;
			}

			if (ivStatus != null) {
				ivStatus.Dispose ();
				ivStatus = null;
			}

			if (lbExpiryDate != null) {
				lbExpiryDate.Dispose ();
				lbExpiryDate = null;
			}

			if (tfTypeOfLicence != null) {
				tfTypeOfLicence.Dispose ();
				tfTypeOfLicence = null;
			}

			if (vBound != null) {
				vBound.Dispose ();
				vBound = null;
			}

			if (btnDeleteCert != null) {
				btnDeleteCert.Dispose ();
				btnDeleteCert = null;
			}
		}
	}
}
