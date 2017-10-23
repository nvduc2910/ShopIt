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
    [Register ("ProjectJoinedTableViewCell")]
    partial class ProjectJoinedTableViewCell
    {
        [Outlet]
        UIKit.UIImageView ivStatus { get; set; }


        [Outlet]
        UIKit.UILabel lbInvitedBy { get; set; }


        [Outlet]
        UIKit.UILabel lbJoinedOn { get; set; }


        [Outlet]
        UIKit.UILabel lbProjectName { get; set; }


        [Outlet]
        UIKit.UILabel lbStage { get; set; }


        [Outlet]
        UIKit.UIView vBound { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ivStatus != null) {
                ivStatus.Dispose ();
                ivStatus = null;
            }

            if (lbInvitedBy != null) {
                lbInvitedBy.Dispose ();
                lbInvitedBy = null;
            }

            if (lbJoinedOn != null) {
                lbJoinedOn.Dispose ();
                lbJoinedOn = null;
            }

            if (lbProjectName != null) {
                lbProjectName.Dispose ();
                lbProjectName = null;
            }

            if (lbStage != null) {
                lbStage.Dispose ();
                lbStage = null;
            }

            if (vBound != null) {
                vBound.Dispose ();
                vBound = null;
            }
        }
    }
}