using System;
using MvvmCross.Plugins.Messenger;

namespace ShopIt.Core.Messengers
{
	public enum ViewModelActionType
	{
		Reload,
		Remove,
		Add,
		Update,
		Other
	}

	public class ViewModelAction
	{
		public Type ViewModelType { get; set; }
		public Type SubViewModelType { get; set; }
		public ViewModelActionType ActionType { get; set; }
		public object Data { get; set; }
	}

	public class ViewModelActionMessage : MvxMessage
	{
		public ViewModelAction Action { get; private set; }
		public ViewModelActionMessage(object sender, ViewModelAction action) : base (sender)
		{
			this.Action = action;
		}
	}
}

