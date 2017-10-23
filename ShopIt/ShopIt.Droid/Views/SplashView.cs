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
using Android.Widget;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTop,
        ScreenOrientation = ScreenOrientation.SensorPortrait, MainLauncher = true, NoHistory = true)]
    public class SplashView : BaseView
    {
        #region UI Controls

        #endregion

        #region Overrides

        public SplashViewModel ViewModel
        {
            get { return base.ViewModel as SplashViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.SplashView);
        }

        #endregion

        #region Implements

        #endregion

        #region Methods

        #endregion
    }
}