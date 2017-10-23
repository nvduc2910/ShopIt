using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;
using System.Threading.Tasks;
using MvvmCross.Platform;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;

namespace ViewModels.Items
{
	public class AlertItemViewModel : MvxViewModel
	{
		#region Constructor
		public AlertItemViewModel(Alert alertItem)
		{
			this.Alert = alertItem;
		}

		#endregion

		#region AlertsVM

		public AlertsViewModel AlertsVM { get; set; }

		#endregion

		#region Properties

		#region Alert
		private Alert mAlert;

		public Alert Alert
		{
			get
			{
				return mAlert;
			}
			set
			{
				mAlert = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region IsRead
		public bool IsRead
		{
			get
			{
				if (Alert.Read == null)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		#endregion

		#region Time
        private string mTime;

		public string Time
		{
			get
			{
				return mAlert.Created.ToString("hh:mm");
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{

		}
		#endregion

		#region Commands

		#region DeleteAlertCommand
		private MvxAsyncCommand mDeleteAlertCommand;

		public MvxAsyncCommand DeleteAlertCommand
		{
			get
			{
				if (mDeleteAlertCommand == null)
				{
					mDeleteAlertCommand = new MvxAsyncCommand(this.DeleteAlert);
				}
				return mDeleteAlertCommand;
			}
		}

		private async Task DeleteAlert()
		{
			if (this.Alert != null && AlertsVM != null)
			{
				
				Mvx.Resolve<IPlatformService>().ShowNetworkIndicator();
				Mvx.Resolve<IProgressDialogService>().ShowProgressDialog();

				var response = await Mvx.Resolve<IApiService>().SettingAlert(mAlert.Id, mAlert.Read, DateTimeOffset.Now);

				Mvx.Resolve<IPlatformService>().HideNetworkIndicator();
				Mvx.Resolve<IProgressDialogService>().HideProgressDialog();

				if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
				{
					AlertsVM.DeleteAlertItem(this);
				}
			}
		}
		#endregion

		#region FlagAlertCommand
		private MvxAsyncCommand mFlagAlertCommand;

		public MvxAsyncCommand FlagAlertCommand
		{
			get
			{
				if (mFlagAlertCommand == null)
				{
					mFlagAlertCommand = new MvxAsyncCommand(this.FlagAlert);
				}
				return mFlagAlertCommand;
			}
		}

		private async Task FlagAlert()
		{
			

			Mvx.Resolve<IPlatformService>().ShowNetworkIndicator();
			Mvx.Resolve<IProgressDialogService>().ShowProgressDialog();

			if (Alert.Read != null)
			{
				var response = await Mvx.Resolve<IApiService>().SettingAlert(mAlert.Id, null, null);

				Mvx.Resolve<IPlatformService>().HideNetworkIndicator();
				Mvx.Resolve<IProgressDialogService>().HideProgressDialog();

				AlertsVM.FlagAlertItem(this);

				if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
				{
					mAlert.Read = null;
					RaisePropertyChanged("IsRead");
					AlertsVM.RaisePropertyChanged("Title");

					int idx = AlertsVM.AlertItems.IndexOf(this);
					AlertsVM.View.CloseOptionsOnAlert(idx);

				}
				return;
			}
			if (Alert.Read == null)
			{
				DateTimeOffset read = DateTimeOffset.Now;
				var response = await Mvx.Resolve<IApiService>().SettingAlert(mAlert.Id, read, null);

				Mvx.Resolve<IPlatformService>().HideNetworkIndicator();
				Mvx.Resolve<IProgressDialogService>().HideProgressDialog();

				AlertsVM.FlagAlertItem(this);

				if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
				{
					mAlert.Read = read;
					RaisePropertyChanged("IsRead");
					AlertsVM.RaisePropertyChanged("Title");

					int idx = AlertsVM.AlertItems.IndexOf(this);
					AlertsVM.View.CloseOptionsOnAlert(idx);

				}
				return;
			}

		}
		#endregion

		#endregion

		#region Methods

		#endregion

	}
}
