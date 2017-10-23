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
using Mixpanel.Android.MpMetrics;
using MvvmCross.Platform;
using Newtonsoft.Json;
using Org.Json;
using Services;

namespace ShopIt.Droid.Services
{
    class TrackingService : ITrackingService
    {
        private MixpanelAPI mMixpanel;

        public void Initialize(string mixPanelToken)
        {
            if (mMixpanel == null)
            {
                var context = Mvx.Resolve<IDroidService>().CurrentContext;
                mMixpanel = MixpanelAPI.GetInstance(context, mixPanelToken);
            }
        }

        public void SetIdentity(string identity)
        {
            if (mMixpanel != null)
                mMixpanel.Identify(identity);
        }

        public void SetSuperProperties(Dictionary<string, string> properties)
        {
            if (mMixpanel != null)
            {
                if (properties != null && properties.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(properties);
                    var obj = JsonConvert.DeserializeObject<JSONObject>(json);
                    mMixpanel.RegisterSuperProperties(obj);
                }
            }

        }

        public void ClearSuperProperties()
        {
            if (mMixpanel != null)
                mMixpanel.ClearSuperProperties();
        }

        public void SendEvent(string eventname, Dictionary<string, string> properties = null)
        {
            if (mMixpanel != null)
            {
                if (properties != null && properties.Count > 0)
                {
                    var json = JsonConvert.SerializeObject(properties);
                    var obj = JsonConvert.DeserializeObject<JSONObject>(json);
                    mMixpanel.Track(eventname, obj);
                }
                else
                {
                    mMixpanel.Track(eventname);
                }
            }
        }

        public void StartTimeEvent(string eventname)
        {
            {
                try
                {
                    mMixpanel.Track(eventname);
                }
                catch (Java.Lang.Exception ex)
                {
                    ex.PrintStackTrace();
                }
            }
        }

        public void EndTimeEvent(string eventname)
        {
            if (mMixpanel != null)
            {
                try
                {
                    mMixpanel.Track(eventname);
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}