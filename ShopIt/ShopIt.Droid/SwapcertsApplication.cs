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
using CrittercismAndroid;
using Gcm.Client;
using Java.Lang;
using ShopIt.Droid.GCM;
using UniversalImageLoader.Core;
using UniversalImageLoader.Core.Assist;

namespace ShopIt.Droid
{
    [Application]
    public class ShopItApplication : Application
    {
        public ShopItApplication(IntPtr handle, JniHandleOwnership ownerShip)
            : base(handle, ownerShip)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            var config = new ImageLoaderConfiguration.Builder(this)
                .MemoryCacheExtraOptions(DroidConstants.ImageSize, DroidConstants.ImageSize) // default = device screen dimensions
                .DiskCacheExtraOptions(DroidConstants.ImageSize, DroidConstants.ImageSize, null)
                .ThreadPoolSize(10) // default
                .ThreadPriority(Thread.NormPriority - 2) // default
                .TasksProcessingOrder(QueueProcessingType.Fifo) // default
                .MemoryCacheSize(20 * 1024 * 1024)
                .MemoryCacheSizePercentage(30) // default
                .DiskCacheSize(50 * 1024 * 1024)
                .DiskCacheFileCount(300)
                .WriteDebugLogs()
                .Build();

            ImageLoader.Instance.Init(config);
            Crittercism.Init(ApplicationContext, "1c2c97937d6f4d0aa6235204ae512f4000555300");

            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);
            GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);

            var handler = new ApplicationLifecycleHandler();
            RegisterActivityLifecycleCallbacks(handler);
            RegisterComponentCallbacks(handler);
        }
    }
}