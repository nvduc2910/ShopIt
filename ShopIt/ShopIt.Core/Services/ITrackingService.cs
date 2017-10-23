using System;
using System.Collections.Generic;

namespace Services
{
	public interface ITrackingService
	{
		void Initialize(string mixPanelToken);
		void SetIdentity(string identity);
		void SetSuperProperties(Dictionary<string, string> properties);
		void ClearSuperProperties();
		void SendEvent(string eventname, Dictionary<string, string> properties = null);
		void StartTimeEvent(string eventname);
		void EndTimeEvent(string eventname);
	}
}
