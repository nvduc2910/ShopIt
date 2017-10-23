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
    [Register ("LoginView")]
    partial class LoginView
    {
        [Outlet]
        UIKit.UIButton btnClosePopupForgotFW { get; set; }


        [Outlet]
        UIKit.UIButton btnForgotPassword { get; set; }


        [Outlet]
        UIKit.UIButton btnPP { get; set; }


        [Outlet]
        UIKit.UIButton btnRegister { get; set; }


        [Outlet]
        UIKit.UIButton btnSendMyPassword { get; set; }


        [Outlet]
        UIKit.UIButton btnSignin { get; set; }


        [Outlet]
        UIKit.UIButton btnTermAndPrivacy { get; set; }


        [Outlet]
        UIKit.UIButton btnToS { get; set; }


        [Outlet]
        UIKit.UITextField tfEmail { get; set; }


        [Outlet]
        UIKit.UITextField tfEmailForgot { get; set; }


        [Outlet]
        UIKit.UITextField tfPassword { get; set; }


        [Outlet]
        UIKit.UITextField tfUsernameForgot { get; set; }


        [Outlet]
        UIKit.UIView vPopupForgotPW { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnClosePopupForgotFW != null) {
                btnClosePopupForgotFW.Dispose ();
                btnClosePopupForgotFW = null;
            }

            if (btnForgotPassword != null) {
                btnForgotPassword.Dispose ();
                btnForgotPassword = null;
            }

            if (btnPP != null) {
                btnPP.Dispose ();
                btnPP = null;
            }

            if (btnRegister != null) {
                btnRegister.Dispose ();
                btnRegister = null;
            }

            if (btnSendMyPassword != null) {
                btnSendMyPassword.Dispose ();
                btnSendMyPassword = null;
            }

            if (btnSignin != null) {
                btnSignin.Dispose ();
                btnSignin = null;
            }

            if (btnTermAndPrivacy != null) {
                btnTermAndPrivacy.Dispose ();
                btnTermAndPrivacy = null;
            }

            if (btnToS != null) {
                btnToS.Dispose ();
                btnToS = null;
            }

            if (tfEmail != null) {
                tfEmail.Dispose ();
                tfEmail = null;
            }

            if (tfEmailForgot != null) {
                tfEmailForgot.Dispose ();
                tfEmailForgot = null;
            }

            if (tfPassword != null) {
                tfPassword.Dispose ();
                tfPassword = null;
            }

            if (vPopupForgotPW != null) {
                vPopupForgotPW.Dispose ();
                vPopupForgotPW = null;
            }
        }
    }
}