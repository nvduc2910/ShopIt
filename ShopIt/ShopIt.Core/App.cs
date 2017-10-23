using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Localization;

namespace ShopIt.Core
{
	public class App : MvxApplication
	{
		public App()
		{
			RegisterAppStart<SplashViewModel>();
		}
	}
}
