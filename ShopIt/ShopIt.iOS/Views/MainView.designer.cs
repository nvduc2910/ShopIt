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
	[Register ("MainView")]
	partial class MainView
	{
		[Outlet]
		UIKit.UIButton btnAbout { get; set; }

		[Outlet]
		UIKit.UIButton btnCloseMenu { get; set; }

		[Outlet]
		UIKit.UIButton btnLicenseSearch { get; set; }

		[Outlet]
		UIKit.UIButton btnSignout { get; set; }

		[Outlet]
		UIKit.UIButton btnTour { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint consTopMenu { get; set; }

		[Outlet]
		UIKit.UIView vBackground { get; set; }

		[Outlet]
		UIKit.UIView vMenu { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLicenseSearch != null) {
				btnLicenseSearch.Dispose ();
				btnLicenseSearch = null;
			}

			if (btnAbout != null) {
				btnAbout.Dispose ();
				btnAbout = null;
			}

			if (btnCloseMenu != null) {
				btnCloseMenu.Dispose ();
				btnCloseMenu = null;
			}

			if (btnSignout != null) {
				btnSignout.Dispose ();
				btnSignout = null;
			}

			if (btnTour != null) {
				btnTour.Dispose ();
				btnTour = null;
			}

			if (consTopMenu != null) {
				consTopMenu.Dispose ();
				consTopMenu = null;
			}

			if (vBackground != null) {
				vBackground.Dispose ();
				vBackground = null;
			}

			if (vMenu != null) {
				vMenu.Dispose ();
				vMenu = null;
			}
		}
	}
}
