// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ShopIt.iOS.Views
{
    [Register ("AlertsView")]
    partial class AlertsView
    {
        [Outlet]
        UIKit.UIButton btnClearSearch { get; set; }


        [Outlet]
        UIKit.UIButton btnMenu { get; set; }


        [Outlet]
        UIKit.UILabel lbTitle { get; set; }


        [Outlet]
        UIKit.UITableView tbAlert { get; set; }


        [Outlet]
        UIKit.UITextField tfSearch { get; set; }


        [Outlet]
        UIKit.UIView vSearch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnClearSearch != null) {
                btnClearSearch.Dispose ();
                btnClearSearch = null;
            }

            if (btnMenu != null) {
                btnMenu.Dispose ();
                btnMenu = null;
            }

            if (lbTitle != null) {
                lbTitle.Dispose ();
                lbTitle = null;
            }

            if (tbAlert != null) {
                tbAlert.Dispose ();
                tbAlert = null;
            }

            if (tfSearch != null) {
                tfSearch.Dispose ();
                tfSearch = null;
            }

            if (vSearch != null) {
                vSearch.Dispose ();
                vSearch = null;
            }
        }
    }
}