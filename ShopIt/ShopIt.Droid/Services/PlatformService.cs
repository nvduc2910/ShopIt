using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Locations;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Util;
using MvvmCross.Platform;
using ShopIt.Core.Services;
using Uri = Android.Net.Uri;

namespace ShopIt.Droid.Services
{
    public class PlatformService : IPlatformService
    {
        public const string SharedPreferenceFileName = "ShopIt";
        private static readonly long[] ThreeCycles = { 100, 1000, 1000, 1000, 1000, 1000 };
        public TaskCompletionSource<bool> Tcs { get; set; }

        public OS OS
        {
            get { return OS.Droid; }
        }

        public string GetPreference(string key)
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var sp = context.GetSharedPreferences(SharedPreferenceFileName, FileCreationMode.Private);
            return sp.GetString(key, string.Empty);
        }

        public void SetPreference(string key, string value)
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var sp = context.GetSharedPreferences(SharedPreferenceFileName, FileCreationMode.Private);
            var editor = sp.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        public void RegisterPushNotification()
        {
            // throw new NotImplementedException();
        }

        public bool DetectInternerConnection()
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            return networkInfo != null && networkInfo.IsConnected;
        }

        public bool IsAppInForeground()
        {
            Context context = Application.Context;
            ActivityManager activityManager = (ActivityManager)context.GetSystemService(Context.ActivityService);
            IList<Android.App.ActivityManager.RunningAppProcessInfo> appProcesses = activityManager.RunningAppProcesses;
            if (appProcesses == null)
            {
                return false;
            }
            string packageName = context.PackageName;
            foreach (Android.App.ActivityManager.RunningAppProcessInfo appProcess in appProcesses)
            {
                if (appProcess.Importance == Importance.Foreground && appProcess.ProcessName == packageName && appProcesses.IndexOf(appProcess) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public object ScheduleLocalNotification(int secs, string message)
        {
            //try
            //{
            //    var context = Mvx.Resolve<IDroidService>().CurrentContext;

            //    var intent = new Intent(context, typeof(MainView));
            //    var stackBuilder = TaskStackBuilder.Create(context);
            //    stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainView)));
            //    stackBuilder.AddNextIntent(intent);

            //    Random rnd = new Random();
            //    int requestId = rnd.Next(1000);
            //    var resultPendingIntent = stackBuilder.GetPendingIntent(requestId, PendingIntentFlags.UpdateCurrent);

            //    var builder = new NotificationCompat.Builder(context)
            //        .SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
            //        .SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
            //        .SetContentTitle("MiChild") // Set the title
            //        .SetSmallIcon(Resource.Drawable.appicon) // This is the icon to display
            //        .SetContentText(string.Format("{0}", message)); // the message to display.

            //    // Finally publish the notification
            //    var notificationManager = (NotificationManager)context.GetSystemService(Android.Content.Context.NotificationService);
            //    notificationManager.Notify(requestId, builder.Build());
            //}
            //catch (Exception)
            //{

            //}

            return null;
        }

        public object ScheduleLocalNotificationRepeat(DateTime dateTime, string message, RepeatEachTime repeat)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllLocalNotification()
        {

        }

        public void RemoveLocalNotification(object localNotification)
        {

        }

        public void SendEmail(string recipients, string subject)
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("message/rfc822");
            intent.PutExtra(Intent.ExtraEmail, new[] { recipients });
            intent.PutExtra(Intent.ExtraSubject, subject);
            context.StartActivity(Intent.CreateChooser(intent, "Choose Email Client"));
        }

        public void ChangeAutoLockScreen(bool enable)
        {
            throw new NotImplementedException();
        }

        public void OpenWeb(string url)
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var uri = Android.Net.Uri.Parse(url);
            var intent = new Intent(Intent.ActionView, uri);
            context.StartActivity(intent);

        }

        public BluetoothClass.Device GetDeviceType()
        {
            //return BluetoothClass.Device.Android_Smartphone;
            return null;
        }

        public string GetDeviceUDID()
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var androidId = Settings.Secure.GetString(context.ContentResolver,
                Settings.Secure.AndroidId);
            return androidId;
        }

        public void ShowNetworkIndicator()
        {
        }

        public void HideNetworkIndicator()
        {
        }

        public void MakePhoneCall(string phoneNumber)
        {
            //try
            //{
            //    var context = Mvx.Resolve<IDroidService>().CurrentContext;
            //    var number = Uri.Parse(string.Format("tel:{0}", phoneNumber));
            //    var callIntent = new Intent(Intent.ActionDial, number);
            //    context.StartActivity(callIntent);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("MakePhoneCall", ex.Message);
            //}
        }

        public void SendSms(string phoneNumber)
        {
            try
            {
                var context = Mvx.Resolve<IDroidService>().CurrentContext;
                var intent = new Intent(Intent.ActionView, Uri.Parse("sms:" + phoneNumber));
                context.StartActivity(intent);
            }
            catch (Exception ex)
            {
                Log.Error("SendSms", ex.Message);
            }
        }

        public void SendEmail(string listingTitle, string link, string email)
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("message/rfc822");
            intent.PutExtra(Intent.ExtraEmail, new[] { email });
            intent.PutExtra(Intent.ExtraSubject, "Zingit enquiry - " + listingTitle);
            intent.PutExtra(Intent.ExtraText,
                string.Format("I am enquiring about {0} on Zingit {1}. Please contact me.", listingTitle, link));
            context.StartActivity(Intent.CreateChooser(intent, "Choose Email Client"));
        }

        public bool DetectLocationService()
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var lm = (LocationManager)context.GetSystemService(Context.LocationService);

            try
            {
                var gpsEnabled = lm.IsProviderEnabled(LocationManager.GpsProvider);

                if (gpsEnabled)
                    return true;
            }
            catch (Exception ex)
            {
                Log.Error("DetectLocationService", ex.Message);
            }

            try
            {
                var networkEnabled = lm.IsProviderEnabled(LocationManager.NetworkProvider);

                if (networkEnabled)
                    return true;
            }
            catch (Exception exx)
            {
                Log.Error("DetectLocationService", exx.Message);
            }

            return false;
        }

        public bool IsNetworkAvailable()
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            var networkInfo = connectivityManager.ActiveNetworkInfo;
            return networkInfo != null && networkInfo.IsConnected;
        }

        public async Task<bool> GetLocation()
        {
            return true;
        }

        public string GetAppVersion()
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
        }

        public void VibrateAndSound()
        {
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            var notification = new Notification();

            // notification.Vibrate = ThreeCycles;
            notification.Sound = Settings.System.DefaultNotificationUri;
            notificationManager.Notify(0, notification);

            var vibrator = (Vibrator)context.GetSystemService(Context.VibratorService);
            vibrator.Vibrate(1000);
        }

        public string GetDeviceToken()
        {
            return "";
        }

        public Task<string> GetAddressFromCoordinate(double lat, double lng)
        {
            var tcs = new TaskCompletionSource<string>();
            var context = Mvx.Resolve<IDroidService>().CurrentContext;
            var geocoder = new Geocoder(context, Locale.Default);

            try
            {
                var addresses = geocoder.GetFromLocation(lat, lng, 1);

                if (addresses != null && addresses.Count > 0)
                {
                    var address = addresses[0].GetAddressLine(0);
                    var city = addresses[0].Locality;
                    var state = addresses[0].AdminArea;
                    var country = addresses[0].CountryName;
                    var postalCode = addresses[0].PostalCode;
                    var knownName = addresses[0].FeatureName;

                    // locationTxt.setText(address + " " + city + " " + country);
                    // return address + ", " + city + ", " + state;

                    tcs.SetResult(city + ", " + state);
                    return tcs.Task;
                }
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
            }


            tcs.SetResult("");
            return tcs.Task;
        }
    }
}