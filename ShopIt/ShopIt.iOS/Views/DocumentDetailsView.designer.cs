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
    [Register ("DocumentDetailsView")]
    partial class DocumentDetailsView
    {
        [Outlet]
        UIKit.UIButton btnClose { get; set; }


        [Outlet]
        UIKit.UIButton btnDelete { get; set; }


        [Outlet]
        ShopIt.iOS.Controls.BindableUrlUIImageView ivPhotoDocument { get; set; }


        [Outlet]
        UIKit.UILabel lbTitile { get; set; }


        [Outlet]
        UIKit.UIScrollView sclMain { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnClose != null) {
                btnClose.Dispose ();
                btnClose = null;
            }

            if (btnDelete != null) {
                btnDelete.Dispose ();
                btnDelete = null;
            }

            if (ivPhotoDocument != null) {
                ivPhotoDocument.Dispose ();
                ivPhotoDocument = null;
            }

            if (lbTitile != null) {
                lbTitile.Dispose ();
                lbTitile = null;
            }

            if (sclMain != null) {
                sclMain.Dispose ();
                sclMain = null;
            }
        }
    }
}