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
    [Register ("ProjectsView")]
    partial class ProjectsView
    {
        [Outlet]
        UIKit.UIButton btnAddDescription { get; set; }


        [Outlet]
        UIKit.UIButton btnAddProject { get; set; }


        [Outlet]
        UIKit.UIButton btnClear { get; set; }


        [Outlet]
        UIKit.UIButton btnClose { get; set; }


        [Outlet]
        UIKit.UIButton btnMenu { get; set; }


        [Outlet]
        UIKit.UIButton btnSave { get; set; }


        [Outlet]
        UIKit.UIButton btnSite { get; set; }


        [Outlet]
        UIKit.UIButton btnTender { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint conHeightVProjectJoined { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVProjectCreated { get; set; }


        [Outlet]
        UIKit.UILabel lbEmptyProjectCreated { get; set; }


        [Outlet]
        UIKit.UILabel lbEmptyProjectJoined { get; set; }


        [Outlet]
        UIKit.UIScrollView sclMain { get; set; }


        [Outlet]
        UIKit.UITableView tbProjectCreated { get; set; }


        [Outlet]
        UIKit.UITableView tbProjectJoined { get; set; }


        [Outlet]
        UIKit.UITextField tfEmailInvite { get; set; }


        [Outlet]
        UIKit.UITextField tfProjectName { get; set; }


        [Outlet]
        UIKit.UIView vAddProject { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnAddProject != null) {
                btnAddProject.Dispose ();
                btnAddProject = null;
            }

            if (btnMenu != null) {
                btnMenu.Dispose ();
                btnMenu = null;
            }

            if (conHeightVProjectJoined != null) {
                conHeightVProjectJoined.Dispose ();
                conHeightVProjectJoined = null;
            }

            if (consHeightVProjectCreated != null) {
                consHeightVProjectCreated.Dispose ();
                consHeightVProjectCreated = null;
            }

            if (lbEmptyProjectCreated != null) {
                lbEmptyProjectCreated.Dispose ();
                lbEmptyProjectCreated = null;
            }

            if (lbEmptyProjectJoined != null) {
                lbEmptyProjectJoined.Dispose ();
                lbEmptyProjectJoined = null;
            }

            if (sclMain != null) {
                sclMain.Dispose ();
                sclMain = null;
            }

            if (tbProjectCreated != null) {
                tbProjectCreated.Dispose ();
                tbProjectCreated = null;
            }

            if (tbProjectJoined != null) {
                tbProjectJoined.Dispose ();
                tbProjectJoined = null;
            }
        }
    }
}