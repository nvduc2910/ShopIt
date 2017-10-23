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
    [Register ("ProjectCreateTableViewCell")]
    partial class ProjectCreateTableViewCell
    {
        [Outlet]
        UIKit.UIImageView ivStatusProject { get; set; }


        [Outlet]
        UIKit.UILabel lbCreatedOn { get; set; }


        [Outlet]
        UIKit.UILabel lbProjectName { get; set; }


        [Outlet]
        UIKit.UILabel lbStatus { get; set; }


        [Outlet]
        UIKit.UIView vBound { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (ivStatusProject != null) {
                ivStatusProject.Dispose ();
                ivStatusProject = null;
            }

            if (lbCreatedOn != null) {
                lbCreatedOn.Dispose ();
                lbCreatedOn = null;
            }

            if (lbProjectName != null) {
                lbProjectName.Dispose ();
                lbProjectName = null;
            }

            if (lbStatus != null) {
                lbStatus.Dispose ();
                lbStatus = null;
            }

            if (vBound != null) {
                vBound.Dispose ();
                vBound = null;
            }
        }
    }
}