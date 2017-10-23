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
	[Register ("HomeView")]
	partial class HomeView
	{
		[Outlet]
		UIKit.UIButton btnTabAlerts { get; set; }

		[Outlet]
		UIKit.UIButton btnTabProfile { get; set; }

		[Outlet]
		UIKit.UIButton btnTabProjects { get; set; }

		[Outlet]
		UIKit.NSLayoutConstraint ConsBottomMenu { get; set; }

		[Outlet]
		UIKit.UILabel lbAlertIndicator { get; set; }

		[Outlet]
		UIKit.UIView vBgAlertIndicator { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnTabAlerts != null) {
				btnTabAlerts.Dispose ();
				btnTabAlerts = null;
			}

			if (btnTabProfile != null) {
				btnTabProfile.Dispose ();
				btnTabProfile = null;
			}

			if (btnTabProjects != null) {
				btnTabProjects.Dispose ();
				btnTabProjects = null;
			}

			if (ConsBottomMenu != null) {
				ConsBottomMenu.Dispose ();
				ConsBottomMenu = null;
			}

			if (lbAlertIndicator != null) {
				lbAlertIndicator.Dispose ();
				lbAlertIndicator = null;
			}

			if (vBgAlertIndicator != null) {
				vBgAlertIndicator.Dispose ();
				vBgAlertIndicator = null;
			}
		}
	}
}
