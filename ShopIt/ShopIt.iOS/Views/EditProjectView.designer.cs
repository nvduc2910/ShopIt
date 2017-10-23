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
    [Register ("EditProjectView")]
    partial class EditProjectView
    {
        [Outlet]
        UIKit.NSLayoutConstraint bottomSpace { get; set; }


        [Outlet]
        UIKit.UIButton btnClearTitle { get; set; }


        [Outlet]
        UIKit.UIButton btnClose { get; set; }


        [Outlet]
        UIKit.UIButton btnDeleteProject { get; set; }


        [Outlet]
        UIKit.UIButton btnEditDescription { get; set; }


        [Outlet]
        UIKit.UIButton btnSaveProject { get; set; }


        [Outlet]
        UIKit.UIButton btnSendInvite { get; set; }


        [Outlet]
        UIKit.UIButton btnShowMore { get; set; }


        [Outlet]
        UIKit.UIButton btnSite { get; set; }


        [Outlet]
        UIKit.UIButton btnTender { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVCurrentTraces { get; set; }


        [Outlet]
        UIKit.UILabel lbDescription { get; set; }


        [Outlet]
        UIKit.UITableView tbCurrentTraces { get; set; }


        [Outlet]
        UIKit.UITableView tbInviteTraces { get; set; }


        [Outlet]
        UIKit.UITableView tbSuggestedContacts { get; set; }


        [Outlet]
        UIKit.UITextField tfEmailAddress { get; set; }


        [Outlet]
        UIKit.UITextField tfProjectTitle { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (bottomSpace != null) {
                bottomSpace.Dispose ();
                bottomSpace = null;
            }

            if (btnClearTitle != null) {
                btnClearTitle.Dispose ();
                btnClearTitle = null;
            }

            if (btnClose != null) {
                btnClose.Dispose ();
                btnClose = null;
            }

            if (btnDeleteProject != null) {
                btnDeleteProject.Dispose ();
                btnDeleteProject = null;
            }

            if (btnEditDescription != null) {
                btnEditDescription.Dispose ();
                btnEditDescription = null;
            }

            if (btnSaveProject != null) {
                btnSaveProject.Dispose ();
                btnSaveProject = null;
            }

            if (btnSendInvite != null) {
                btnSendInvite.Dispose ();
                btnSendInvite = null;
            }

            if (btnShowMore != null) {
                btnShowMore.Dispose ();
                btnShowMore = null;
            }

            if (btnSite != null) {
                btnSite.Dispose ();
                btnSite = null;
            }

            if (btnTender != null) {
                btnTender.Dispose ();
                btnTender = null;
            }

            if (consHeightVCurrentTraces != null) {
                consHeightVCurrentTraces.Dispose ();
                consHeightVCurrentTraces = null;
            }

            if (lbDescription != null) {
                lbDescription.Dispose ();
                lbDescription = null;
            }

            if (tbCurrentTraces != null) {
                tbCurrentTraces.Dispose ();
                tbCurrentTraces = null;
            }

            if (tbInviteTraces != null) {
                tbInviteTraces.Dispose ();
                tbInviteTraces = null;
            }

            if (tbSuggestedContacts != null) {
                tbSuggestedContacts.Dispose ();
                tbSuggestedContacts = null;
            }

            if (tfEmailAddress != null) {
                tfEmailAddress.Dispose ();
                tfEmailAddress = null;
            }

            if (tfProjectTitle != null) {
                tfProjectTitle.Dispose ();
                tfProjectTitle = null;
            }
        }
    }
}