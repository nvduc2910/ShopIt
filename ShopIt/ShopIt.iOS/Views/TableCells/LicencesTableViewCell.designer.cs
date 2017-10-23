// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ShopIt.iOS
{
    [Register ("LicencesTableViewCell")]
    partial class LicencesTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnLicenceExpiry { get; set; }


        [Outlet]
        UIKit.UIButton btnRemove { get; set; }


        [Outlet]
        UIKit.UIButton btnTakePhoto { get; set; }


        [Outlet]
        ShopIt.iOS.Controls.BindableUrlUIImageView ivPhoto { get; set; }


        [Outlet]
        UIKit.UITextField tfLicenceExpiry { get; set; }


        [Outlet]
        UIKit.UITextField tfTypeOfLicence { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnLicenceExpiry != null) {
                btnLicenceExpiry.Dispose ();
                btnLicenceExpiry = null;
            }

            if (btnRemove != null) {
                btnRemove.Dispose ();
                btnRemove = null;
            }

            if (btnTakePhoto != null) {
                btnTakePhoto.Dispose ();
                btnTakePhoto = null;
            }

            if (ivPhoto != null) {
                ivPhoto.Dispose ();
                ivPhoto = null;
            }

            if (tfLicenceExpiry != null) {
                tfLicenceExpiry.Dispose ();
                tfLicenceExpiry = null;
            }

            if (tfTypeOfLicence != null) {
                tfTypeOfLicence.Dispose ();
                tfTypeOfLicence = null;
            }
        }
    }
}