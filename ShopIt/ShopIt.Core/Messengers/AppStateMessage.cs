using System;
using MvvmCross.Plugins.Messenger;

namespace ShopIt.Core.Messengers
{
	public enum AppState
	{
		Active = 0,
		InActive
	}

	public class ChangeAppStateMessage : MvxMessage
	{
		public AppState AppState { get; private set;}
		public ChangeAppStateMessage(object sender, AppState appstate) : base (sender)
		{
			this.AppState = appstate;
		}
	}
}