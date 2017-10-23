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
	[Register ("OperationalCertViewTableViewCell")]
	partial class OperationalCertViewTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnDeleteCert { get; set; }

		[Outlet]
		UIKit.UIButton btnExpiryDate { get; set; }

		[Outlet]
		UIKit.UIButton btnViewCertificate { get; set; }

		[Outlet]
		UIKit.UIImageView ivStatus { get; set; }

		[Outlet]
		UIKit.UILabel lbExpiry { get; set; }

		[Outlet]
		UIKit.UITextField tfType { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnExpiryDate != null) {
				btnExpiryDate.Dispose ();
				btnExpiryDate = null;
			}

			if (btnViewCertificate != null) {
				btnViewCertificate.Dispose ();
				btnViewCertificate = null;
			}

			if (ivStatus != null) {
				ivStatus.Dispose ();
				ivStatus = null;
			}

			if (lbExpiry != null) {
				lbExpiry.Dispose ();
				lbExpiry = null;
			}

			if (tfType != null) {
				tfType.Dispose ();
				tfType = null;
			}

			if (btnDeleteCert != null) {
				btnDeleteCert.Dispose ();
				btnDeleteCert = null;
			}
		}
	}
}
