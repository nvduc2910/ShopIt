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
	[Register ("InviteViewProjectTableViewCell")]
	partial class InviteViewProjectTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnSelectCell { get; set; }

		[Outlet]
		UIKit.UIImageView ivStatus { get; set; }

		[Outlet]
		UIKit.UIImageView ivViewProfile { get; set; }

		[Outlet]
		UIKit.UILabel lbInviteName { get; set; }

		[Outlet]
		UIKit.UILabel lbPending { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnSelectCell != null) {
				btnSelectCell.Dispose ();
				btnSelectCell = null;
			}

			if (ivStatus != null) {
				ivStatus.Dispose ();
				ivStatus = null;
			}

			if (ivViewProfile != null) {
				ivViewProfile.Dispose ();
				ivViewProfile = null;
			}

			if (lbInviteName != null) {
				lbInviteName.Dispose ();
				lbInviteName = null;
			}

			if (lbPending != null) {
				lbPending.Dispose ();
				lbPending = null;
			}
		}
	}
}
