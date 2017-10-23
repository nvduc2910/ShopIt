using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        ScreenOrientation = ScreenOrientation.SensorPortrait,WindowSoftInputMode = SoftInput.AdjustResize)]
    public class ViewProjectView : BaseView, IViewProject
    {
        #region UI Controls

        #endregion

        #region Overrides

        public ViewProjectViewModel ViewModel
        {
            get { return base.ViewModel as ViewProjectViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ViewProjectView);
            ViewModel.View = this;
        }

        #endregion

        #region Implements
        #region IViewProject
        public void ShowInvitee()
        {
        }

        public void HidenInvitee()
        {
        }
        #endregion

        #endregion

        #region Methods

        #endregion


    }
}