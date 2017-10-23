// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace ShopIt.iOS.Views.TableCells
{
    [Register ("OperationalCertTableViewCell")]
    partial class OperationalCertTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnCerExpiry { get; set; }


        [Outlet]
        UIKit.UIButton btnRemove { get; set; }


        [Outlet]
        UIKit.UIButton btnTakePhotoOperationalCer { get; set; }


        [Outlet]
        ShopIt.iOS.Controls.BindableUrlUIImageView ivOperationalCer { get; set; }


        [Outlet]
        UIKit.UITextField tfCertificateExpiry { get; set; }


        [Outlet]
        UIKit.UITextField tfTypeOfCert { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnCerExpiry != null) {
                btnCerExpiry.Dispose ();
                btnCerExpiry = null;
            }

            if (btnRemove != null) {
                btnRemove.Dispose ();
                btnRemove = null;
            }

            if (btnTakePhotoOperationalCer != null) {
                btnTakePhotoOperationalCer.Dispose ();
                btnTakePhotoOperationalCer = null;
            }

            if (ivOperationalCer != null) {
                ivOperationalCer.Dispose ();
                ivOperationalCer = null;
            }

            if (tfCertificateExpiry != null) {
                tfCertificateExpiry.Dispose ();
                tfCertificateExpiry = null;
            }

            if (tfTypeOfCert != null) {
                tfTypeOfCert.Dispose ();
                tfTypeOfCert = null;
            }
        }
    }
}