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
    [Register ("AlertTableViewCell")]
    partial class AlertTableViewCell
    {
        [Outlet]
        UIKit.UIButton btnDelete { get; set; }


        [Outlet]
        UIKit.UIButton btnFlag { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint contentViewLeftConstraint { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint contentViewRightConstraint { get; set; }


        [Outlet]
        UIKit.UIImageView ivRead { get; set; }


        [Outlet]
        UIKit.UILabel lbDescription { get; set; }


        [Outlet]
        UIKit.UILabel lbText { get; set; }


        [Outlet]
        UIKit.UILabel lbTime { get; set; }


        [Outlet]
        UIKit.UIView vMyContent { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnDelete != null) {
                btnDelete.Dispose ();
                btnDelete = null;
            }

            if (btnFlag != null) {
                btnFlag.Dispose ();
                btnFlag = null;
            }

            if (contentViewLeftConstraint != null) {
                contentViewLeftConstraint.Dispose ();
                contentViewLeftConstraint = null;
            }

            if (contentViewRightConstraint != null) {
                contentViewRightConstraint.Dispose ();
                contentViewRightConstraint = null;
            }

            if (ivRead != null) {
                ivRead.Dispose ();
                ivRead = null;
            }

            if (lbDescription != null) {
                lbDescription.Dispose ();
                lbDescription = null;
            }

            if (lbText != null) {
                lbText.Dispose ();
                lbText = null;
            }

            if (lbTime != null) {
                lbTime.Dispose ();
                lbTime = null;
            }

            if (vMyContent != null) {
                vMyContent.Dispose ();
                vMyContent = null;
            }
        }
    }
}