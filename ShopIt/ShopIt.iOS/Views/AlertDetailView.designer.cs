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
    [Register ("AlertDetailView")]
    partial class AlertDetailView
    {
        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UIButton btnShare { get; set; }


        [Outlet]
        UIKit.UILabel lbAlertTitle { get; set; }


        [Outlet]
        UIKit.UITextView tvBody { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnShare != null) {
                btnShare.Dispose ();
                btnShare = null;
            }

            if (lbAlertTitle != null) {
                lbAlertTitle.Dispose ();
                lbAlertTitle = null;
            }

            if (tvBody != null) {
                tvBody.Dispose ();
                tvBody = null;
            }
        }
    }
}