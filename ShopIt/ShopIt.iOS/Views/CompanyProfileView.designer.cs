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
    [Register ("CompanyProfileView")]
    partial class CompanyProfileView
    {
        [Outlet]
        UIKit.UIButton btnBack { get; set; }


        [Outlet]
        UIKit.UIButton btnCancel { get; set; }


        [Outlet]
        UIKit.UIButton btnCheckDisplay { get; set; }


        [Outlet]
        UIKit.UIButton btnCheckSameAs { get; set; }


        [Outlet]
        UIKit.UIButton btnCompensation { get; set; }


        [Outlet]
        UIKit.UIButton btnCompensationExpiry { get; set; }


        [Outlet]
        UIKit.UIButton btnCompensationPhoto { get; set; }


        [Outlet]
        UIKit.UIButton btnGST { get; set; }


        [Outlet]
        UIKit.UIButton btnGSTExpiry { get; set; }


        [Outlet]
        UIKit.UIButton btnNext { get; set; }


        [Outlet]
        UIKit.UIButton btnProductLiability { get; set; }


        [Outlet]
        UIKit.UIButton btnProductLiabilityExpiry { get; set; }


        [Outlet]
        UIKit.UIButton btnProductLiabilityPhoto { get; set; }


        [Outlet]
        UIKit.UIButton btnPublicLiability { get; set; }


        [Outlet]
        UIKit.UIButton btnPublicLiabilityExpiry { get; set; }


        [Outlet]
        UIKit.UIButton btnPublicLiabilityPhoto { get; set; }


        [Outlet]
        UIKit.UIButton btnSetTime { get; set; }


        [Outlet]
        UIKit.UIButton btnTakeGSTPhoto { get; set; }


        [Outlet]
        UIKit.UIButton btnTapAdd { get; set; }


        [Outlet]
        UIKit.UIButton btnVerify { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVBusinessAddress { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVLicenceCompany { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVProductLiability { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVPublicLiability { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consHeightVWorkCompensation { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consVHeightCompensation { get; set; }


        [Outlet]
        UIKit.NSLayoutConstraint consVHeightGSTRegistered { get; set; }


        [Outlet]
        UIKit.UIDatePicker dpkTime { get; set; }


        [Outlet]
        ShopIt.iOS.Controls.BindableUrlUIImageView ivCompensationPhoto { get; set; }


        [Outlet]
        UIKit.UIImageView ivGSTPhoto { get; set; }


        [Outlet]
        ShopIt.iOS.Controls.BindableUrlUIImageView ivProductLiabilityPhoto { get; set; }


        [Outlet]
        ShopIt.iOS.Controls.BindableUrlUIImageView ivPublicLiabilityPhoto { get; set; }


        [Outlet]
        UIKit.UILabel lbTitileStep { get; set; }


        [Outlet]
        UIKit.UIScrollView sclMain { get; set; }


        [Outlet]
        ShopIt.iOS.Views.Controls.TPKeyboardAvoidingScrollView sclMainStep2 { get; set; }


        [Outlet]
        UIKit.UITableView tbLicenceCompany { get; set; }


        [Outlet]
        UIKit.UITextField tfABN { get; set; }


        [Outlet]
        UIKit.UITextField tfBusinessName { get; set; }


        [Outlet]
        UIKit.UITextField tfCompensationExpiry { get; set; }


        [Outlet]
        UIKit.UITextField tfDollarProduct { get; set; }


        [Outlet]
        UIKit.UITextField tfDollarPublic { get; set; }


        [Outlet]
        UIKit.UITextField tfPosition { get; set; }


        [Outlet]
        UIKit.UITextField tfPostcode { get; set; }


        [Outlet]
        UIKit.UITextField tfProductLiabilityExpiry { get; set; }


        [Outlet]
        UIKit.UITextField tfPublicLiabilityExpiry { get; set; }


        [Outlet]
        UIKit.UITextField tfState { get; set; }


        [Outlet]
        UIKit.UITextField tfStreetName { get; set; }


        [Outlet]
        UIKit.UITextField tfUnit { get; set; }


        [Outlet]
        UIKit.UIView vAddGST { get; set; }


        [Outlet]
        UIKit.UIView vBusinessAddress { get; set; }


        [Outlet]
        UIKit.UIView vChooseTime { get; set; }


        [Outlet]
        UIKit.UIView vHighlightStep1 { get; set; }


        [Outlet]
        UIKit.UIView vHightlightDone { get; set; }


        [Outlet]
        UIKit.UIView vPopupDateTime { get; set; }


        [Outlet]
        UIKit.UIView vProductLiability { get; set; }


        [Outlet]
        UIKit.UIView vPublicLiability { get; set; }


        [Outlet]
        UIKit.UIView vWorkCompensation { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (btnBack != null) {
                btnBack.Dispose ();
                btnBack = null;
            }

            if (btnCancel != null) {
                btnCancel.Dispose ();
                btnCancel = null;
            }

            if (btnCheckDisplay != null) {
                btnCheckDisplay.Dispose ();
                btnCheckDisplay = null;
            }

            if (btnCheckSameAs != null) {
                btnCheckSameAs.Dispose ();
                btnCheckSameAs = null;
            }

            if (btnCompensation != null) {
                btnCompensation.Dispose ();
                btnCompensation = null;
            }

            if (btnCompensationExpiry != null) {
                btnCompensationExpiry.Dispose ();
                btnCompensationExpiry = null;
            }

            if (btnCompensationPhoto != null) {
                btnCompensationPhoto.Dispose ();
                btnCompensationPhoto = null;
            }

            if (btnGST != null) {
                btnGST.Dispose ();
                btnGST = null;
            }

            if (btnGSTExpiry != null) {
                btnGSTExpiry.Dispose ();
                btnGSTExpiry = null;
            }

            if (btnNext != null) {
                btnNext.Dispose ();
                btnNext = null;
            }

            if (btnProductLiability != null) {
                btnProductLiability.Dispose ();
                btnProductLiability = null;
            }

            if (btnProductLiabilityExpiry != null) {
                btnProductLiabilityExpiry.Dispose ();
                btnProductLiabilityExpiry = null;
            }

            if (btnProductLiabilityPhoto != null) {
                btnProductLiabilityPhoto.Dispose ();
                btnProductLiabilityPhoto = null;
            }

            if (btnPublicLiability != null) {
                btnPublicLiability.Dispose ();
                btnPublicLiability = null;
            }

            if (btnPublicLiabilityExpiry != null) {
                btnPublicLiabilityExpiry.Dispose ();
                btnPublicLiabilityExpiry = null;
            }

            if (btnPublicLiabilityPhoto != null) {
                btnPublicLiabilityPhoto.Dispose ();
                btnPublicLiabilityPhoto = null;
            }

            if (btnSetTime != null) {
                btnSetTime.Dispose ();
                btnSetTime = null;
            }

            if (btnTakeGSTPhoto != null) {
                btnTakeGSTPhoto.Dispose ();
                btnTakeGSTPhoto = null;
            }

            if (btnTapAdd != null) {
                btnTapAdd.Dispose ();
                btnTapAdd = null;
            }

            if (btnVerify != null) {
                btnVerify.Dispose ();
                btnVerify = null;
            }

            if (consHeightVBusinessAddress != null) {
                consHeightVBusinessAddress.Dispose ();
                consHeightVBusinessAddress = null;
            }

            if (consHeightVLicenceCompany != null) {
                consHeightVLicenceCompany.Dispose ();
                consHeightVLicenceCompany = null;
            }

            if (consHeightVProductLiability != null) {
                consHeightVProductLiability.Dispose ();
                consHeightVProductLiability = null;
            }

            if (consHeightVPublicLiability != null) {
                consHeightVPublicLiability.Dispose ();
                consHeightVPublicLiability = null;
            }

            if (consHeightVWorkCompensation != null) {
                consHeightVWorkCompensation.Dispose ();
                consHeightVWorkCompensation = null;
            }

            if (consVHeightGSTRegistered != null) {
                consVHeightGSTRegistered.Dispose ();
                consVHeightGSTRegistered = null;
            }

            if (dpkTime != null) {
                dpkTime.Dispose ();
                dpkTime = null;
            }

            if (ivCompensationPhoto != null) {
                ivCompensationPhoto.Dispose ();
                ivCompensationPhoto = null;
            }

            if (ivGSTPhoto != null) {
                ivGSTPhoto.Dispose ();
                ivGSTPhoto = null;
            }

            if (ivProductLiabilityPhoto != null) {
                ivProductLiabilityPhoto.Dispose ();
                ivProductLiabilityPhoto = null;
            }

            if (ivPublicLiabilityPhoto != null) {
                ivPublicLiabilityPhoto.Dispose ();
                ivPublicLiabilityPhoto = null;
            }

            if (lbTitileStep != null) {
                lbTitileStep.Dispose ();
                lbTitileStep = null;
            }

            if (sclMain != null) {
                sclMain.Dispose ();
                sclMain = null;
            }

            if (sclMainStep2 != null) {
                sclMainStep2.Dispose ();
                sclMainStep2 = null;
            }

            if (tbLicenceCompany != null) {
                tbLicenceCompany.Dispose ();
                tbLicenceCompany = null;
            }

            if (tfABN != null) {
                tfABN.Dispose ();
                tfABN = null;
            }

            if (tfBusinessName != null) {
                tfBusinessName.Dispose ();
                tfBusinessName = null;
            }

            if (tfCompensationExpiry != null) {
                tfCompensationExpiry.Dispose ();
                tfCompensationExpiry = null;
            }

            if (tfDollarProduct != null) {
                tfDollarProduct.Dispose ();
                tfDollarProduct = null;
            }

            if (tfDollarPublic != null) {
                tfDollarPublic.Dispose ();
                tfDollarPublic = null;
            }

            if (tfPosition != null) {
                tfPosition.Dispose ();
                tfPosition = null;
            }

            if (tfPostcode != null) {
                tfPostcode.Dispose ();
                tfPostcode = null;
            }

            if (tfProductLiabilityExpiry != null) {
                tfProductLiabilityExpiry.Dispose ();
                tfProductLiabilityExpiry = null;
            }

            if (tfPublicLiabilityExpiry != null) {
                tfPublicLiabilityExpiry.Dispose ();
                tfPublicLiabilityExpiry = null;
            }

            if (tfState != null) {
                tfState.Dispose ();
                tfState = null;
            }

            if (tfStreetName != null) {
                tfStreetName.Dispose ();
                tfStreetName = null;
            }

            if (tfUnit != null) {
                tfUnit.Dispose ();
                tfUnit = null;
            }

            if (vAddGST != null) {
                vAddGST.Dispose ();
                vAddGST = null;
            }

            if (vBusinessAddress != null) {
                vBusinessAddress.Dispose ();
                vBusinessAddress = null;
            }

            if (vChooseTime != null) {
                vChooseTime.Dispose ();
                vChooseTime = null;
            }

            if (vHighlightStep1 != null) {
                vHighlightStep1.Dispose ();
                vHighlightStep1 = null;
            }

            if (vHightlightDone != null) {
                vHightlightDone.Dispose ();
                vHightlightDone = null;
            }

            if (vPopupDateTime != null) {
                vPopupDateTime.Dispose ();
                vPopupDateTime = null;
            }

            if (vProductLiability != null) {
                vProductLiability.Dispose ();
                vProductLiability = null;
            }

            if (vPublicLiability != null) {
                vPublicLiability.Dispose ();
                vPublicLiability = null;
            }

            if (vWorkCompensation != null) {
                vWorkCompensation.Dispose ();
                vWorkCompensation = null;
            }
        }
    }
}