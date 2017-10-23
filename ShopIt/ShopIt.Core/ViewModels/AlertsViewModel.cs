using System;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;
using ViewModels.Items;
using System.Linq;
using ShopIt.Core.Models;

namespace ShopIt.Core.ViewModels
{
	public interface IAlertsView
	{
		void CloseOptionsOnAlert(int idx);
	}

	public class AlertsViewModel : BaseViewModel
	{
		#region Constructors
		public AlertsViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region HomeVM

		public HomeViewModel HomeVM { get; set; }

		#endregion

		#region View

		public IAlertsView View { get; set; }

		#endregion

		#region Properties

		#region Title
		public string Title
		{
			get
			{
				int numberOrUnread = mAlertItems.Where(s => s.Alert.Read == null).Count();

				return "Alerts" + (numberOrUnread > 0 ? " (" + numberOrUnread + ")" : "");
			}
		}
		#endregion

		#region TextSearch
		private string mTextSearch = string.Empty;

		public string TextSearch
		{
			get
			{
				return mTextSearch;
			}
			set
			{
				mTextSearch = value;

				if (mTextSearch != null || mTextSearch != "")
				{
					AlertItems = new MvxObservableCollection<AlertItemViewModel>(mAlertItemsStatic.Where(x => x.Alert.Title.ToLower().Contains(mTextSearch.ToLower())));
				}
				else
				{
					AlertItems = mAlertItemsStatic;
				}
				RaisePropertyChanged();
				RaisePropertyChanged("IsHaveAlerts");
				RaisePropertyChanged("HasClearSearch");

			}
		}
		#endregion

		#region AlertIndicator
		private string mAlertIndicator;

		public string AlertIndicator
		{
			get
			{
				return mAlertIndicator;
			}
			set
			{
				mAlertIndicator = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region AmountAlertIndicator
		private int mAmountAlertIndicator;

		public int AmountAlertIndicator
		{
			get
			{
				return mAmountAlertIndicator;
			}
			set
			{
				mAmountAlertIndicator = value;
				if (mAmountAlertIndicator == 0)
				{
					AlertIndicator = string.Empty;
				}
				else
				{
					if (mAmountAlertIndicator < 100)
					{
						AlertIndicator = mAmountAlertIndicator.ToString();
					}
					else
					{
						AlertIndicator = "99+";
					}

				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region HasClearSearch
		private bool mHasClearSearch;

		public bool HasClearSearch
		{
			get
			{
				return string.IsNullOrEmpty(TextSearch) ? false : true;
			}
		}
		#endregion


		//#region IsSeach
		//private bool mIsSeaching;

		//public bool IsSeaching
		//{
		//	get
		//	{
		//		return mIsSeaching;
		//	}
		//	set
		//	{
		//		mIsSeaching = value;
		//		RaisePropertyChanged();
		//	}
		//}
		//#endregion

		#region AlertItemsStatic

		private MvxObservableCollection<AlertItemViewModel> mAlertItemsStatic = new MvxObservableCollection<AlertItemViewModel>();

		#endregion

		#region AlertItems
		private MvxObservableCollection<AlertItemViewModel> mAlertItems = new MvxObservableCollection<AlertItemViewModel>();

		public MvxObservableCollection<AlertItemViewModel> AlertItems
		{
			get
			{
				return mAlertItems;
			}
			set
			{
				mAlertItems = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsHaveAlerts
		public bool IsHaveAlerts
		{
			get
			{
				return !(mAlertItems.Count == 0);
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
			//InitData();
			//if (mCacheService.AmountAlert == 0)
			//{
			//	AlertIndicator = string.Empty;
			//}
			//else
			//{
			//	AlertIndicator = mCacheService.AmountAlert.ToString();
			//}
			//AmountAlertIndicator = mCacheService.AmountAlert;

			LoadData();
		}
		#endregion

		#region Commands

		#region ShowMenuCommand
		private MvxCommand mShowMenuCommand;

		public MvxCommand ShowMenuCommand
		{
			get
			{
				if (mShowMenuCommand == null)
				{
					mShowMenuCommand = new MvxCommand(this.ShowMenu);
				}
				return mShowMenuCommand;
			}
		}

		private void ShowMenu()
		{
			HomeVM.MainVM.ShowMenuCommand.Execute();
		}
		#endregion

		#region ClearSearchCommand
		private MvxCommand mClearSearchCommand;

		public MvxCommand ClearSearchCommand
		{
			get
			{
				if (mClearSearchCommand == null)
				{
					mClearSearchCommand = new MvxCommand(this.ClearSearch);
				}
				return mClearSearchCommand;
			}
		}

		private void ClearSearch()
		{
			TextSearch = "";
		}
		#endregion

		#region AlertItemClickCommand
		private MvxCommand<AlertItemViewModel> mSelectAlertItemCommand;

		public MvxCommand<AlertItemViewModel> SelectAlertItemCommand
		{
			get
			{
				if (mSelectAlertItemCommand == null)
				{
					mSelectAlertItemCommand = new MvxCommand<AlertItemViewModel>(this.SelectAlertItem);
				}
				return mSelectAlertItemCommand;
			}
		}

		private void SelectAlertItem(AlertItemViewModel alertItem)
		{
			if (AmountAlertIndicator > 0)
			{
				AmountAlertIndicator--;
			}
			mCacheService.AlertItem = alertItem;
			// 
			if (alertItem.Alert.ProjectId == null)
			{
				// direct alert
				ShowViewModel<AlertDetailViewModel>();
			}
			else
			{
				if (HomeVM.ProfileVM.UserProfileCompleted())
				{
					mCacheService.AlertItems = mAlertItems;
					ShowViewModel<AlertInviteViewModel>();
				}
				else
				{
					mMessageboxService.ShowToast("You must complete your personal profile first!");
				}
			}
		}
		#endregion

		#endregion

		#region Methods

		#region InitData
		private void InitData()
		{
			var alert = new Alert();
			alert.Title = "Test swipes";
			var alertItem = new AlertItemViewModel(alert);

			AlertItems = new MvxObservableCollection<AlertItemViewModel>();

			AlertItems.Add(alertItem);
		}
		#endregion

		#region LoadData

		public async void LoadData(bool resetTextSearch = true)
		{
			AmountAlertIndicator = 0;
			mPlatformService.ShowNetworkIndicator();
			//mProgressDialogService.ShowProgressDialog();

			var response = await mApiService.GetAlertList();

			foreach (var item in response.Data)
			{
				if (item.Read == null)
				{
					AmountAlertIndicator++;
				}
			}

			mPlatformService.HideNetworkIndicator();
			//mProgressDialogService.HideProgressDialog();

			mAlertItemsStatic.Clear();

			if (response.Data != null)
			{
				response.Data.Sort((Alert x, Alert y) => 0 - x.Created.CompareTo(y.Created)); ;

				foreach (var alert in response.Data)
				{
					AlertItemViewModel alertItem = new AlertItemViewModel(alert);
					alertItem.AlertsVM = this;
					mAlertItemsStatic.Add(alertItem);
				}
			}

			if (resetTextSearch)
				TextSearch = "";
			else
				TextSearch = TextSearch;
			RaisePropertyChanged("IsHaveAlerts");
			RaisePropertyChanged("Title");
		}

		#endregion

		#region DeleteAlertItem

		public void DeleteAlertItem(AlertItemViewModel alertItem)
		{
			if (alertItem.Alert.Read == null)
			{
				if (AmountAlertIndicator > 0)
				{
					AmountAlertIndicator--;
				}
			}
			mAlertItemsStatic.Remove(alertItem);
			AlertItems.Remove(alertItem);
			RaisePropertyChanged("Title");
		}

		#endregion

		public void FlagAlertItem(AlertItemViewModel alertItem)
		{
			if (alertItem.Alert.Read != null)
			{
				AmountAlertIndicator++;
				return;
			}
			else if (alertItem.Alert.Read == null)
			{
				AmountAlertIndicator--;
				return;
			}

		}

		#endregion
	}
}
