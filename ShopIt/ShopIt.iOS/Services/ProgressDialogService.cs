﻿using System;
using MBProgressBinding;
using ShopIt.Core.Services;
using UIKit;

namespace ShopIt.iOS
{
	public class ProgressDialogService : IProgressDialogService
	{
		public ProgressDialogService()
		{
		}

		public void HideProgressDialog()
		{
			UIViewController rootVC = ((AppDelegate)UIApplication.SharedApplication.Delegate).Presenter.MasterNavigationController;

			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				MBProgressHUD.HideHUDForView(rootVC.View, true);
			});
		}

		public void ShowProgressDialog(string message = "")
		{
			UIViewController rootVC = ((AppDelegate)UIApplication.SharedApplication.Delegate).Presenter.MasterNavigationController;

			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				MBProgressHUD hud = MBProgressHUD.HUDForView(rootVC.View);
				if (hud == null)
				 	hud = MBProgressHUD.ShowHUDAddedTo(rootVC.View, true);
				hud.UserInteractionEnabled = true;
				hud.DetailsLabel.Text = message;
			});
		}
	}
}
