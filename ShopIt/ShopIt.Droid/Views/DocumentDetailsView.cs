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
        ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class DocumentDetailsView : BaseView
    {
        #region UI Controls

        #endregion

        #region Overrides

        public DocumentDetailsViewModel ViewModel
        {
            get { return base.ViewModel as DocumentDetailsViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.DocumentDetailsView);
            Init();
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