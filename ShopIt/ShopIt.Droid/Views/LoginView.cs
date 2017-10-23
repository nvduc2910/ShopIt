using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Interop;
using ShopIt.Core.ViewModels;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.SensorPortrait,WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class LoginView : BaseView, ILoginView
    {
        #region UI Controls

        private LinearLayout lnForgotPasswordDialog;

        #endregion

        #region Overrides

        public LoginViewModel ViewModel
        {
            get { return base.ViewModel as LoginViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.LoginView);
            ViewModel.View = this;
            InitView();
        }

        #endregion

        #region Implements

        #region ILoginView

        public void ShowPopUpForgotPassword()
        {
            ObjectAnimator fadeIn = ObjectAnimator.OfFloat(this.lnForgotPasswordDialog, "alpha", new float[] {0,1});
            fadeIn.SetDuration(500);
            fadeIn.AnimationStart += (sender, args) =>
            {
                this.lnForgotPasswordDialog.Visibility = ViewStates.Visible;
            };
            fadeIn.Start();
        }

        public void HidePopupForgotPassword()
        {
            ObjectAnimator fadeOut = ObjectAnimator.OfFloat(this.lnForgotPasswordDialog, "alpha", new float[] { 1, 0 });
            fadeOut.SetDuration(500);
            fadeOut.AnimationEnd += (sender, args) =>
            {
                this.lnForgotPasswordDialog.Visibility = ViewStates.Gone;
            };
            fadeOut.Start();
        }

        public void HideKeyboard()
        {
           this.HideKeyboard("LoginView");
        }

        #endregion

        #endregion

        #region Methods

        void InitView()
        {
            this.lnForgotPasswordDialog = this.FindViewById<LinearLayout>(Resource.Id.lnForgotPasswordDialog);


        }
        #endregion


    }
}