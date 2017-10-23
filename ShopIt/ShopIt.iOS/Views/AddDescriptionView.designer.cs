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
    [Register ("AddDescriptionView")]
    partial class AddDescriptionView
    {
        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UIButton btnSave { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consBottomVCharaterNumber { get; set; }


        [Outlet]
        UIKit.UILabel lbCharacterNumber { get; set; }


        [Outlet]
        SZTextViewBinding.SZTextView tvDescription { get; set; }


        [Outlet]
        UIKit.UIView vCharacterNumber { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnSave != null) {
                btnSave.Dispose ();
                btnSave = null;
            }

            if (consBottomVCharaterNumber != null) {
                consBottomVCharaterNumber.Dispose ();
                consBottomVCharaterNumber = null;
            }

            if (lbCharacterNumber != null) {
                lbCharacterNumber.Dispose ();
                lbCharacterNumber = null;
            }

            if (tvDescription != null) {
                tvDescription.Dispose ();
                tvDescription = null;
            }

            if (vCharacterNumber != null) {
                vCharacterNumber.Dispose ();
                vCharacterNumber = null;
            }
        }
    }
}