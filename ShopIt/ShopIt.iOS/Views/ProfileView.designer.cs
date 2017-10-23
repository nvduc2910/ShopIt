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
    [Register ("ProfileView")]
    partial class ProfileView
    {
        [Outlet]
        UIKit.UIButton btnCreateProfile { get; set; }


        [Outlet]
        UIKit.UIButton btnMenu { get; set; }


        [Outlet]
        UIKit.UIButton btnSendProfile { get; set; }


        [Outlet]
        UIKit.UIButton btnSendProfile1 { get; set; }


        [Outlet]
        UIKit.UIButton btnShareProfile { get; set; }


        [Outlet]
        UIKit.UIButton btnShareProfile1 { get; set; }


        [Outlet]
        UIKit.UIButton btnViewProfile { get; set; }


        [Outlet]
        UIKit.UIImageView ivStatusAlert { get; set; }


        [Outlet]
        UIKit.UIView vCreateProfile { get; set; }


        [Outlet]
        UIKit.UIView vViewProfile { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCreateProfile != null) {
                btnCreateProfile.Dispose ();
                btnCreateProfile = null;
            }

            if (btnMenu != null) {
                btnMenu.Dispose ();
                btnMenu = null;
            }

            if (btnSendProfile != null) {
                btnSendProfile.Dispose ();
                btnSendProfile = null;
            }

            if (btnSendProfile1 != null) {
                btnSendProfile1.Dispose ();
                btnSendProfile1 = null;
            }

            if (btnShareProfile != null) {
                btnShareProfile.Dispose ();
                btnShareProfile = null;
            }

            if (btnShareProfile1 != null) {
                btnShareProfile1.Dispose ();
                btnShareProfile1 = null;
            }

            if (btnViewProfile != null) {
                btnViewProfile.Dispose ();
                btnViewProfile = null;
            }

            if (ivStatusAlert != null) {
                ivStatusAlert.Dispose ();
                ivStatusAlert = null;
            }

            if (vCreateProfile != null) {
                vCreateProfile.Dispose ();
                vCreateProfile = null;
            }

            if (vViewProfile != null) {
                vViewProfile.Dispose ();
                vViewProfile = null;
            }
        }
    }
}