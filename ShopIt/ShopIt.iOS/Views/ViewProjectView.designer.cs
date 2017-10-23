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
    [Register ("ViewProjectView")]
    partial class ViewProjectView
    {
        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UIButton btnClearSearchText { get; set; }


        [Outlet]
        UIKit.UIButton btnEdit { get; set; }


        [Outlet]
        UIKit.UIButton btnShowMore { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVInvitee { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVSearchInvitees { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeigtVInvitee { get; set; }


        [Outlet]
        UIKit.UILabel lbAddress { get; set; }


        [Outlet]
        UIKit.UILabel lbDescription { get; set; }


        [Outlet]
        UIKit.UILabel lbEmail { get; set; }


        [Outlet]
        UIKit.UILabel lbMobile { get; set; }


        [Outlet]
        UIKit.UILabel lbOwnerType { get; set; }


        [Outlet]
        UIKit.UITextField lbSearchText { get; set; }


        [Outlet]
        UIKit.UILabel lbTitleProject { get; set; }


        [Outlet]
        UIKit.UITableView tbInvites { get; set; }


        [Outlet]
        UIKit.UIView vInvitee { get; set; }


        [Outlet]
        UIKit.UIView vLoading { get; set; }


        [Outlet]
        UIKit.UIView vProjectCreator { get; set; }


        [Outlet]
        UIKit.UIView vSeachInvitee { get; set; }


        [Outlet]
        UIKit.UIView vSearch { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnClearSearchText != null) {
                btnClearSearchText.Dispose ();
                btnClearSearchText = null;
            }

            if (btnEdit != null) {
                btnEdit.Dispose ();
                btnEdit = null;
            }

            if (btnShowMore != null) {
                btnShowMore.Dispose ();
                btnShowMore = null;
            }

            if (consHeightVSearchInvitees != null) {
                consHeightVSearchInvitees.Dispose ();
                consHeightVSearchInvitees = null;
            }

            if (consHeigtVInvitee != null) {
                consHeigtVInvitee.Dispose ();
                consHeigtVInvitee = null;
            }

            if (lbAddress != null) {
                lbAddress.Dispose ();
                lbAddress = null;
            }

            if (lbDescription != null) {
                lbDescription.Dispose ();
                lbDescription = null;
            }

            if (lbEmail != null) {
                lbEmail.Dispose ();
                lbEmail = null;
            }

            if (lbMobile != null) {
                lbMobile.Dispose ();
                lbMobile = null;
            }

            if (lbOwnerType != null) {
                lbOwnerType.Dispose ();
                lbOwnerType = null;
            }

            if (lbSearchText != null) {
                lbSearchText.Dispose ();
                lbSearchText = null;
            }

            if (lbTitleProject != null) {
                lbTitleProject.Dispose ();
                lbTitleProject = null;
            }

            if (tbInvites != null) {
                tbInvites.Dispose ();
                tbInvites = null;
            }

            if (vInvitee != null) {
                vInvitee.Dispose ();
                vInvitee = null;
            }

            if (vLoading != null) {
                vLoading.Dispose ();
                vLoading = null;
            }

            if (vProjectCreator != null) {
                vProjectCreator.Dispose ();
                vProjectCreator = null;
            }

            if (vSeachInvitee != null) {
                vSeachInvitee.Dispose ();
                vSeachInvitee = null;
            }

            if (vSearch != null) {
                vSearch.Dispose ();
                vSearch = null;
            }
        }
    }
}