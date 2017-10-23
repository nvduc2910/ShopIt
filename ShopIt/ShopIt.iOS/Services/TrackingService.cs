using System;
using System.Collections.Generic;
using Foundation;
using Services;
using ShopIt.Core;

namespace ShopIt.iOS.Services
{
	public class TrackingService : ITrackingService
	{
		public TrackingService()
		{
			
		}
		public void Initialize(string mixPanelToken)
		{
//#if DEBUG
//			return;
//#endif

			try
			{
				Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstanceWithToken(mixPanelToken);
			}
			catch (Exception ex)
			{
				
			}
		}

		public void SetIdentity(string identity)
		{
//#if DEBUG
//			return;
//#endif
			try
			{
				Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstance;
				if (mixpanel == null)
					return;
				mixpanel.Identify(identity);
			}
			catch (Exception ex)
			{
				
			}
		}

		public void SendEvent(string eventname, Dictionary<string, string> properties = null)
		{
//#if DEBUG
//			return;
//#endif
			try
			{
				InitIfNull();
				Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstance;
				if (mixpanel == null)
					return;

				NSDictionary<NSString, NSString> nsdictionary;

				if (properties != null && properties.Count > 0)
				{
					NSString[] keys = new NSString[properties.Count];
					NSString[] values = new NSString[properties.Count];

					int i = 0;

					foreach (var item in properties)
					{
						keys[i] = new NSString(item.Key);
						values[i] = new NSString(item.Value);
						i++;
					}

					nsdictionary = new NSDictionary<NSString, NSString>(keys, values);

					mixpanel.Track(eventname, nsdictionary);

				}
				else
				{
					mixpanel.Track(eventname);
				}
			}
			catch (Exception ex)
			{ 
				
			}

		}

		public void SetSuperProperties(Dictionary<string, string> properties)
		{
			//#if DEBUG
			//return;
			//#endif

			try {
				InitIfNull();

				Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstance;

				NSDictionary<NSString, NSString> nsdictionary;

				if (properties != null && properties.Count > 0)
				{
					NSString[] keys = new NSString[properties.Count];
					NSString[] values = new NSString[properties.Count];

					int i = 0;
					foreach (var item in properties)
					{
						keys[i] = new NSString(item.Key);
						values[i] = new NSString(item.Value);
						i++;
					}

					nsdictionary = new NSDictionary<NSString, NSString>(keys, values);

					mixpanel.RegisterSuperProperties(nsdictionary);
				}
			}
			catch (Exception ex)
			{

			}
		}
		public void ClearSuperProperties()
		{
			//#if DEBUG
			//return;
			//#endif

			try {
				InitIfNull();

				Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstance;

				mixpanel.ClearSuperProperties();
			}
			catch (Exception ex)
			{

			}
		}
		public void StartTimeEvent(string eventname)
		{
			#if DEBUG
			return;
			#endif

			try {
				InitIfNull();

				Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstance;

				if (mixpanel == null)
					return;

				mixpanel.TimeEvent(eventname);
			}
			catch (Exception ex)
			{

			}
		}
		public void EndTimeEvent(string eventname)
		{
			//#if DEBUG
			//return;
			//#endif

			try {
				InitIfNull();

				Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstance;

				if (mixpanel == null)
					return;

				mixpanel.Track(eventname);
			}
			catch (Exception ex)
			{

			}
		}

		private void InitIfNull()
		{
			Mixpanel.Mixpanel mixpanel = Mixpanel.Mixpanel.SharedInstance;
			if (mixpanel == null)
			{
				Initialize(AppConstants.MixPanelToken);
			}
		}
	}
}
