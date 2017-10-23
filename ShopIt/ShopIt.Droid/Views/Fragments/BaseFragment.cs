using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using ShopIt.Core.ViewModels;

namespace ShopIt.Droid.Views.Fragments
{
    public class BaseFragment : MvxFragment
    {
        #region Methods

        #region ReplaceFragment

        protected void ReplaceFragment(BaseFragment fragment, BaseViewModel viewModel, int containerViewId)
        {
            fragment.ViewModel = viewModel;
            this.Activity.SupportFragmentManager
                .BeginTransaction()
                .Replace(containerViewId, fragment)
                .Commit();
        }

        #endregion

        protected void ShowToast(Context context, string message)
        {
            Toast.MakeText(context, message, ToastLength.Short);
        }

        #endregion
    }
}