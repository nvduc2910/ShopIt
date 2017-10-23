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
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using ShopIt.Core.Services;

namespace ShopIt.Droid.Services
{
    public class ProgressDialogService : IProgressDialogService
    {
        private Dialog mProgressDialog;

        public void ShowProgressDialog(string message = "")
        {
            var context = (Activity)Mvx.Resolve<IDroidService>().CurrentContext;

            if (context == null)
                return;

            //HideProgressDialog();
            context.RunOnUiThread(() =>
            {
                try
                {
                    if (mProgressDialog != null)
                    {
                        var title = mProgressDialog.FindViewById<TextView>(Resource.Id.title);
                        if (string.IsNullOrEmpty(message))
                        {
                            title.Visibility = ViewStates.Gone;
                        }
                        else
                        {
                            title.Visibility = ViewStates.Visible;
                            title.Text = message;
                        }
                    }
                    else
                    {
                        mProgressDialog = new Dialog(context);
                        mProgressDialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                        mProgressDialog.SetContentView(Resource.Layout.CustomProgressDialog);
                        mProgressDialog.Window.SetBackgroundDrawableResource(Android.Resource.Color.Transparent);
                        mProgressDialog.SetCancelable(false);
                        mProgressDialog.SetCanceledOnTouchOutside(false);
                        var title = mProgressDialog.FindViewById<TextView>(Resource.Id.title);

                        if (string.IsNullOrEmpty(message))
                        {
                            title.Visibility = ViewStates.Gone;
                        }
                        else
                        {
                            title.Visibility = ViewStates.Visible;
                            title.Text = message;
                        }
                    }
                   

                    mProgressDialog.Show();
                }
                catch (Exception ex)
                {
                    MvxTrace.Warning(ex.Message);
                    mProgressDialog = null;
                }
            });
        }

        public void HideProgressDialog()
        {
            var context = (Activity)Mvx.Resolve<IDroidService>().CurrentContext;

            if (context == null)
                return;

            context.RunOnUiThread(() =>
            {
                try
                {
                    if (mProgressDialog != null)
                    {
                        mProgressDialog.Dismiss();
                        mProgressDialog = null;
                    }
                }
                catch (Exception ex)
                {
                    MvxTrace.Warning(ex.Message);
                    mProgressDialog = null;
                }
            });
        }
    }
}