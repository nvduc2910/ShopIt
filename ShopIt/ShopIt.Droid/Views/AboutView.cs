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
using ViewModels;
using ViewModels.Items;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class AboutView : BaseView
    {
        #region UI Controls


        #endregion

        #region Overrides

        public AboutViewModel ViewModel
        {
            get { return base.ViewModel as AboutViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.AboutView);
            Init();
        }

        protected override void OnPause()
        {
            base.OnPause();
            HideKeyboard("AboutView");
        }

        #endregion

        #region Implements

        #endregion

        #region Methods

        void Init()
        {
        }

        #endregion
    }
}