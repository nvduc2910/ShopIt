using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform;
using ShopIt.Droid.Services;

namespace ShopIt.Droid
{
    public class ApplicationLifecycleHandler : Java.Lang.Object, Application.IActivityLifecycleCallbacks, IComponentCallbacks2
    {

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
           
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
           
        }

        public void OnConfigurationChanged(Configuration newConfig)
        {
        }

        public void OnLowMemory()
        {
        }

        public void OnTrimMemory(TrimMemory level)
        {
        }
    }
}