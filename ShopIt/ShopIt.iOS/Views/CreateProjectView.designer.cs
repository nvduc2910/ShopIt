// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ShopIt.iOS.Views
{
	[Register ("CreateProjectView")]
	partial class CreateProjectView
	{
		[Outlet]
		UIKit.UIButton btnAddDescription { get; set; }

		[Outlet]
		UIKit.UIButton btnClear { get; set; }

		[Outlet]
		UIKit.UIButton btnClose { get; set; }

		[Outlet]
		UIKit.UIButton btnSaveProject { get; set; }

		[Outlet]
		UIKit.UIButton btnSite { get; set; }

		[Outlet]
		UIKit.UIButton btnTapAddInvitee { get; set; }

		[Outlet]
		UIKit.UIButton btnTender { get; set; }

		[Outlet]
		UIKit.UILabel lbDescription { get; set; }

		[Outlet]
		UIKit.UILabel lbLeghtTitle { get; set; }

		[Outlet]
		UIKit.UITableView tbInvite { get; set; }

		[Outlet]
		UIKit.UITableView tbSuggestedContacts { get; set; }

		[Outlet]
		UIKit.UITextField tfEmailInvite { get; set; }

		[Outlet]
		UIKit.UITextField tfProjectName { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnAddDescription != null) {
				btnAddDescription.Dispose ();
				btnAddDescription = null;
			}

			if (btnClear != null) {
				btnClear.Dispose ();
				btnClear = null;
			}

			if (btnClose != null) {
				btnClose.Dispose ();
				btnClose = null;
			}

			if (btnSaveProject != null) {
				btnSaveProject.Dispose ();
				btnSaveProject = null;
			}

			if (btnSite != null) {
				btnSite.Dispose ();
				btnSite = null;
			}

			if (btnTender != null) {
				btnTender.Dispose ();
				btnTender = null;
			}

			if (lbDescription != null) {
				lbDescription.Dispose ();
				lbDescription = null;
			}

			if (lbLeghtTitle != null) {
				lbLeghtTitle.Dispose ();
				lbLeghtTitle = null;
			}

			if (tbInvite != null) {
				tbInvite.Dispose ();
				tbInvite = null;
			}

			if (tbSuggestedContacts != null) {
				tbSuggestedContacts.Dispose ();
				tbSuggestedContacts = null;
			}

			if (tfEmailInvite != null) {
				tfEmailInvite.Dispose ();
				tfEmailInvite = null;
			}

			if (tfProjectName != null) {
				tfProjectName.Dispose ();
				tfProjectName = null;
			}

			if (btnTapAddInvitee != null) {
				btnTapAddInvitee.Dispose ();
				btnTapAddInvitee = null;
			}
		}
	}
}
