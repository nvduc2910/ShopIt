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
    [Register ("SignUpView")]
    partial class SignUpView
    {
        [Outlet]
        UIKit.UIButton btnClose { get; set; }


        [Outlet]
        UIKit.UIButton btnPP { get; set; }


        [Outlet]
        UIKit.UIButton btnSignup { get; set; }


        [Outlet]
        UIKit.UIButton btnTermAndPrivacy { get; set; }


        [Outlet]
        UIKit.UIButton btnToS { get; set; }


        [Outlet]
        UIKit.UITextField tfConfirmPassword { get; set; }


        [Outlet]
        UIKit.UITextField tfEmail { get; set; }


        [Outlet]
        UIKit.UITextField tfPassword { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnClose != null) {
                btnClose.Dispose ();
                btnClose = null;
            }

            if (btnPP != null) {
                btnPP.Dispose ();
                btnPP = null;
            }

            if (btnSignup != null) {
                btnSignup.Dispose ();
                btnSignup = null;
            }

            if (btnTermAndPrivacy != null) {
                btnTermAndPrivacy.Dispose ();
                btnTermAndPrivacy = null;
            }

            if (btnToS != null) {
                btnToS.Dispose ();
                btnToS = null;
            }

            if (tfConfirmPassword != null) {
                tfConfirmPassword.Dispose ();
                tfConfirmPassword = null;
            }

            if (tfEmail != null) {
                tfEmail.Dispose ();
                tfEmail = null;
            }

            if (tfPassword != null) {
                tfPassword.Dispose ();
                tfPassword = null;
            }
        }
    }
}