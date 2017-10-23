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
        ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class LocalWebView : BaseView
    {
        #region UI Controls

        #endregion

        #region Overrides

        public LocalWebViewModel ViewModel
        {
            get { return base.ViewModel as LocalWebViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.LocalWebView);
        }

        #endregion

        #region Implements

        #endregion

        #region Methods

        #endregion
    }
}