using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using MvvmCross.Platform;
using ShopIt.Core.Services;
using ShopIt.Droid.Services;
using ShopIt.Droid.Views;

namespace ShopIt.Droid.GCM
{
    [Service]
    public class GcmService : GcmServiceBase
    {
        public const string GCM_ID = "GCMID";
        public GcmService() : base(GcmBroadcastReceiver.SENDER_IDS) { }

        protected override void OnMessage(Context context, Intent intent)
        {
            // Todo: show notification here
            String message = "";
            var msg = new StringBuilder();
            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                {
                    //msg.AppendLine(key + "=" + intent.Extras.Get(key).ToString());
                    //if (key.Equals("alert"))
                    //{
                    //    alert = intent.Extras.Get(key).ToString();
                    //}
                    //if (key.Equals("deeplink"))
                    //{
                    //    deeplink = intent.Extras.Get(key).ToString();
                    //}
                    if (key.Equals("message")) message = intent.Extras.Get(key).ToString();
                }
            }
            Log.Debug("GCM Mes:", msg.ToString());
            HandlePushNotification("ShopIt", message, "");
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Debug("GCM ERROR", errorId);
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            var prefs = GetSharedPreferences(PlatformService.SharedPreferenceFileName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString(GCM_ID, registrationId);
            edit.Commit();
            Log.Debug("GCM ID", registrationId);
            Mvx.Resolve<ICacheService>().DeviceToken = registrationId;

        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
        }

        private void HandlePushNotification(string title, string message, string deeplink)
        {
            var intent = new Intent(Intent.ActionView);
            //intent.SetData(Android.Net.Uri.Parse(deeplink));

            var stackBuilder = TaskStackBuilder.Create(this);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainView)));
            stackBuilder.AddNextIntent(intent);

            var resultPendingIntent = stackBuilder.GetPendingIntent(0, PendingIntentFlags.UpdateCurrent);

            // Build the notification
            var builder = new NotificationCompat.Builder(this)
                .SetAutoCancel(true) // dismiss the notification from the notification area when the user clicks on it
                .SetContentIntent(resultPendingIntent) // start up this activity when the user clicks the intent.
                .SetContentTitle(title) // Set the title
                .SetSmallIcon(Resource.Drawable.appicon) // This is the icon to display
                .SetContentText(string.Format("{0}", message)); // the message to display.

            // Finally publish the notification
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.Notify(0, builder.Build());
        }
    }
}