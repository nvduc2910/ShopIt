using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;
using ViewModels.Items;
using System.Threading.Tasks;

namespace ShopIt.Core.ViewModels
{

	public interface IAlertDetailsView
	{
		void ShareAlert();
	}

	public class AlertDetailViewModel : BaseViewModel
	{
		#region Constructors
		public AlertDetailViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region Properties

		#region View
		public IAlertDetailsView View { get; set; }	
  		#endregion

		#region AlertItem
		private AlertItemViewModel mAlertItem;

		public AlertItemViewModel AlertItem
		{
			get
			{
				return mAlertItem;
			}
			set
			{
				mAlertItem = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
			InitData();
			MarkAsRead();
		}
		#endregion

		#region Commands

		#region ShareCommand
		private MvxCommand mShareCommand;

		public MvxCommand ShareCommand
		{
			get
			{
				if (mShareCommand == null)
				{
					mShareCommand = new MvxCommand(this.Share);
				}
				return mShareCommand;
			}
		}

		private void Share()
		{
			if(View != null)
			{
				View.ShareAlert();
				mMessageboxService.ShowToast("Copied");
			}
		}
		#endregion

		#endregion

		#region Methods

		#region InitData

		private void InitData()
		{
			AlertItem = mCacheService.AlertItem;
		}

		#endregion

		#region MarkAsRead

		private async Task MarkAsRead()
		{
			if (AlertItem.Alert.Read == null)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				DateTimeOffset read = DateTimeOffset.Now;
				var response = await mApiService.SettingAlert(AlertItem.Alert.Id, read, null);

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
				{
					AlertItem.Alert.Read = read;
					AlertItem.RaisePropertyChanged("IsRead");
					AlertItem.AlertsVM.RaisePropertyChanged("Title");
				}
			}
		}

		#endregion

		#endregion
	}
}
