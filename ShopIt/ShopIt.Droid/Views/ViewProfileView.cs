using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Webkit;
using Android.Widget;
using Java.IO;
using Java.Util;
using MvvmCross.Platform;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using ShopIt.Droid.Controls;
using ShopIt.Droid.Extensions;
using ShopIt.Droid.Services;
using ViewModels;
using File = Java.IO.File;
using Uri = Android.Net.Uri;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.SensorPortrait,WindowSoftInputMode = SoftInput.AdjustResize)]
    public class ViewProfileView : BaseView, IViewProfileView
    {
        #region UI Controls

        private LinearLayout llOtherInformation;
        private RelativeLayout AddCompanyDetail;

        #endregion

        #region Overrides

        public ViewProfileViewModel ViewModel
        {
            get { return base.ViewModel as ViewProfileViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.ViewProfileView);
            ViewModel.View = this;
            Init();
        }

        #endregion

        #region Implements
        #region IViewProfileView
        public void HidenKeyboard()
        {
        }

        public void ShowTradeContractorCert()
        {
        }

        public void HidenTradeContractorCert()
        {
        }

        public void ShowIncomeInsured()
        {
            
        }

        public void HidenIncomeInsured()
        {
        }

        public void ShowFirstAid()
        {
        }

        public void HidenFirstAid()
        {
        }

        public void ShowGSTRegistered()
        {
        }

        public void HidenGSTRegistered()
        {
        }

        public void ShowOtherInformation()
        {
            llOtherInformation.Visibility = ViewStates.Visible;
        }

        public void HidenOtherInformation()
        {
            llOtherInformation.Visibility = ViewStates.Gone;
        }

        public void ShowOperationCert()
        {
        }

        public void HidenOperationCert()
        {
        }

        public void ShowCompanyProfile()
        {
        }

        public void HideCompanyProfile()
        {
        }

        public void ShowAddCompanyDetails()
        {
            AddCompanyDetail.Visibility = ViewStates.Visible;
        }

        public void HidenAddCompanyDetails()
        {
            AddCompanyDetail.Visibility = ViewStates.Gone;
        }

        public void ShowCompensation()
        {
        }

        public void HidenCompensation()
        {
        }

        public void ShowPublicLiability()
        {
        }

        public void HidenPublicLiability()
        {
        }

        public void ShowProductLiability()
        {
        }

        public void HidenProductLiability()
        {
        }

        public void ShowAddressCompany()
        {
        }

        public void HidenAddressCompany()
        {
        }

        public void ShowLicences()
        {
        }

        public void HidenLicences()
        {
        }

        public void ShowContructorCardExpiryDate()
        {
        }

        public void HidenContructorCardExpiryDate()
        {
        }

        public void ShowPerPublicLiability()
        {
        }

        public void HidenPerPublicLiability()
        {
        }

        #endregion

        #endregion

        #region Methods

        void Init()
        {
            llOtherInformation = this.FindViewById<LinearLayout>(Resource.Id.llOtherInformation);
            AddCompanyDetail = this.FindViewById<RelativeLayout>(Resource.Id.AddCompanyDetail);
        }

        #endregion

        
    }
}