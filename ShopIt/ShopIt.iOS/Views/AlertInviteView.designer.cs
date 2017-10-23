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
    [Register ("AlertInviteView")]
    partial class AlertInviteView
    {
        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UIButton btnJoinProject { get; set; }


        [Outlet]
        UIKit.UILabel lbAddress { get; set; }


        [Outlet]
        UIKit.UILabel lbCompany { get; set; }


        [Outlet]
        UIKit.UILabel lbEmail { get; set; }


        [Outlet]
        UIKit.UILabel lbMobile { get; set; }


        [Outlet]
        UIKit.UILabel lbProjectDescription { get; set; }


        [Outlet]
        UIKit.UILabel lbProjectTitle { get; set; }


        [Outlet]
        UIKit.UILabel lbStage { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnJoinProject != null) {
                btnJoinProject.Dispose ();
                btnJoinProject = null;
            }

            if (lbAddress != null) {
                lbAddress.Dispose ();
                lbAddress = null;
            }

            if (lbCompany != null) {
                lbCompany.Dispose ();
                lbCompany = null;
            }

            if (lbEmail != null) {
                lbEmail.Dispose ();
                lbEmail = null;
            }

            if (lbMobile != null) {
                lbMobile.Dispose ();
                lbMobile = null;
            }

            if (lbProjectDescription != null) {
                lbProjectDescription.Dispose ();
                lbProjectDescription = null;
            }

            if (lbProjectTitle != null) {
                lbProjectTitle.Dispose ();
                lbProjectTitle = null;
            }

            if (lbStage != null) {
                lbStage.Dispose ();
                lbStage = null;
            }
        }
    }
}