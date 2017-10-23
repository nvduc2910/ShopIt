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
    [Register ("AboutView")]
    partial class AboutView
    {
        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UIButton btnPP { get; set; }


        [Outlet]
        UIKit.UIButton btnToS { get; set; }


        [Outlet]
        UIKit.UITextView tvAbout { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnPP != null) {
                btnPP.Dispose ();
                btnPP = null;
            }

            if (btnToS != null) {
                btnToS.Dispose ();
                btnToS = null;
            }

            if (tvAbout != null) {
                tvAbout.Dispose ();
                tvAbout = null;
            }
        }
    }
}