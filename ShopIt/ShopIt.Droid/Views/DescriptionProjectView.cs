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
    public class DescriptionProjectView : BaseView
    {
        #region UI Controls

        private EditText etDesc;

        #endregion

        #region Overrides

        public DescriptionProjectViewModel ViewModel
        {
            get { return base.ViewModel as DescriptionProjectViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.DescriptionProjectView);
            Init();
        }

        protected override void OnPause()
        {
            base.OnPause();
            HideKeyboard("DescriptionProjectView");
        }

        #endregion

        #region Implements

        #endregion

        #region Methods

        void Init()
        {
            etDesc = this.FindViewById<EditText>(Resource.Id.etDesc);
            etDesc.SetSelection(etDesc.Text.Length);
        }

        #endregion
    }
}