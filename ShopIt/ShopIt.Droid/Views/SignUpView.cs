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
using ShopIt.Core.ViewModels;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.SensorPortrait)]
    public class SignUpView : BaseView, ISignUpView
    {
        #region UI Controls

        #endregion

        #region Overrides

        public SignUpViewModel ViewModel
        {
            get { return base.ViewModel as SignUpViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.SignUpView);
            ViewModel.View = this;
        }

        #endregion

        #region Implements

        #region ISignUpView

        public void HideKeyboard()
        {
            this.HideKeyboard("SignUp View");
        }

        #endregion

        #endregion

        #region Methods

        #endregion


    }
}