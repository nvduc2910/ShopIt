using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;
using ViewModels.Items;
using System.Threading.Tasks;
using ShopIt.Core.Messengers;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace ShopIt.Core.ViewModels
{
	public class AlertInviteViewModel : BaseViewModel
	{
		#region Constructors
		public AlertInviteViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}
		#endregion

		#region Properties

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

		#region JoinProjectCommand
		private MvxAsyncCommand mJoinProjectCommand;

		public MvxAsyncCommand JoinProjectCommand
		{
			get
			{
				if (mJoinProjectCommand == null)
				{
					mJoinProjectCommand = new MvxAsyncCommand(this.JoinProject);
				}
				return mJoinProjectCommand;
			}
		}

		private async Task JoinProject()
		{
			if (mAlertItem.Alert.ProjectId != null)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var reponse = await mApiService.JoinProject(mAlertItem.Alert.ProjectId.Value, mAlertItem.Alert.Id);

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (reponse.StatusCode == System.Net.HttpStatusCode.OK)
				{
					mCacheService.AlertItems.Remove(mAlertItem);
					Close(this);

					ViewModelAction action = new ViewModelAction
					{
						ActionType = ViewModelActionType.Reload,
						ViewModelType = typeof(HomeViewModel),
						SubViewModelType = typeof(ProjectsViewModel)
					};
					Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action));
				}
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
