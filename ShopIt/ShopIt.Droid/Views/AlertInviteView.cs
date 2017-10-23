using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using ShopIt.Core.ViewModels;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustPan)]
    public class AlertInviteView : BaseView
    {
        #region UI Controls

        #endregion

        #region Overrides

        public AlertInviteViewModel ViewModel
        {
            get { return base.ViewModel as AlertInviteViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.AlertInviteView);
        }

        #endregion

        #region Implements

        #endregion

        #region Methods

        #endregion
    }
}