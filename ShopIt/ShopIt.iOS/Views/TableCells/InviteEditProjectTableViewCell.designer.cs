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
	[Register ("InviteEditProjectTableViewCell")]
	partial class InviteEditProjectTableViewCell
	{
		[Outlet]
		UIKit.UIButton btnDeletePendingInvite { get; set; }

		[Outlet]
		UIKit.UILabel lbInviteName { get; set; }

		[Outlet]
		UIKit.UILabel lbStatus { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lbInviteName != null) {
				lbInviteName.Dispose ();
				lbInviteName = null;
			}

			if (lbStatus != null) {
				lbStatus.Dispose ();
				lbStatus = null;
			}

			if (btnDeletePendingInvite != null) {
				btnDeletePendingInvite.Dispose ();
				btnDeletePendingInvite = null;
			}
		}
	}
}
