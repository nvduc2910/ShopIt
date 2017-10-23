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
    [Register ("DocumentsView")]
    partial class DocumentsView
    {
        [Outlet]
        UIKit.UIButton btnAdd { get; set; }


        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UICollectionView colDocuments { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnAdd != null) {
                btnAdd.Dispose ();
                btnAdd = null;
            }

            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (colDocuments != null) {
                colDocuments.Dispose ();
                colDocuments = null;
            }
        }
    }
}