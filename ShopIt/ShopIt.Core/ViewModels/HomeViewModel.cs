using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using ShopIt.Core.Messengers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;

namespace ShopIt.Core.ViewModels
{
	public enum HomeTab
	{
		Profile = 0,
		Projects,
		Alerts
	}

	public interface IHomeView
	{
		void SwitchToTab(HomeTab tab);
	}

	public class HomeViewModel : BaseViewModel
	{
		#region Constructors
		public HomeViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region View

		public IHomeView View { get; set; }

		#endregion

		#region MainVM

		public MainViewModel MainVM { get; set; }

		#endregion

		#region Properties

		#region IsNeedLoad

		private bool mIsAlertsNeedLoad = true;
		private bool mIsProjectsNeedLoad = true;

		#endregion

		#region ProfileVM
		private ProfileViewModel mProfileVM;

		public ProfileViewModel ProfileVM
		{
			get
			{
				return mProfileVM;
			}
			set
			{
				mProfileVM = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ProjectsVM
		private ProjectsViewModel mProjectsVM;

		public ProjectsViewModel ProjectsVM
		{
			get
			{
				return mProjectsVM;
			}
			set
			{
				mProjectsVM = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region AlertsVM
		private AlertsViewModel mAlertsVM;

		public AlertsViewModel AlertsVM
		{
			get
			{
				return mAlertsVM;
			}
			set
			{
				mAlertsVM = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsTabProfileSelected
		private bool mIsTabProfileSelected;

		public bool IsTabProfileSelected
		{
			get
			{
				return mIsTabProfileSelected;
			}
			set
			{
				mIsTabProfileSelected = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsTabProjectSelected
		private bool mIsTabProjectSelected;

		public bool IsTabProjectSelected
		{
			get
			{
				return mIsTabProjectSelected;
			}
			set
			{
				mIsTabProjectSelected = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsTabAlertsSelected
		private bool mIsTabAlertsSelected;

		public bool IsTabAlertsSelected
		{
			get
			{
				return mIsTabAlertsSelected;
			}
			set
			{
				mIsTabAlertsSelected = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
			ProfileVM = new ProfileViewModel(mApiService, mCacheService, mMessageboxService, mProgressDialogService, mPlatformService);
			ProfileVM.HomeVM = this;
			ProfileVM.Init();
			IsTabProfileSelected = true;

			ProjectsVM = new ProjectsViewModel(mApiService, mCacheService, mMessageboxService, mProgressDialogService, mPlatformService);
			ProjectsVM.HomeVM = this;
			ProjectsVM.Init();
			IsTabProjectSelected = false;

			AlertsVM = new AlertsViewModel(mApiService, mCacheService, mMessageboxService, mProgressDialogService, mPlatformService);
			AlertsVM.HomeVM = this;
			AlertsVM.Init();
			IsTabAlertsSelected = false;

			RegisterMessengers();
		}
		#endregion

		#region Commands

		#region SelectTabProfileCommand
		private MvxCommand mSelectTabProfileCommand;

		public MvxCommand SelectTabProfileCommand
		{
			get
			{
				if (mSelectTabProfileCommand == null)
				{
					mSelectTabProfileCommand = new MvxCommand(this.SelectTabProfile);
				}
				return mSelectTabProfileCommand;
			}
		}

		private void SelectTabProfile()
		{
			UnSelectAllTabs();
			IsTabProfileSelected = true;
			View.SwitchToTab(HomeTab.Profile);
		}
		#endregion

		#region SelectTabProjectsCommand
		private MvxCommand mSelectTabProjectsCommand;

		public MvxCommand SelectTabProjectsCommand
		{
			get
			{
				if (mSelectTabProjectsCommand == null)
				{
					mSelectTabProjectsCommand = new MvxCommand(this.SelectTabProjects);
				}
				return mSelectTabProjectsCommand;
			}
		}

		private void SelectTabProjects()
		{
			UnSelectAllTabs();
			IsTabProjectSelected = true;
			View.SwitchToTab(HomeTab.Projects);

			if (mIsProjectsNeedLoad)
			{
				mIsProjectsNeedLoad = false;
				ProjectsVM.LoadData();
			}
		}
		#endregion

		#region SelectTabAlertsCommand
		private MvxCommand mSelectTabAlertsCommand;

		public MvxCommand SelectTabAlertsCommand
		{
			get
			{
				if (mSelectTabAlertsCommand == null)
				{
					mSelectTabAlertsCommand = new MvxCommand(this.SelectTabAlerts);
				}
				return mSelectTabAlertsCommand;
			}
		}

		private void SelectTabAlerts()
		{
			UnSelectAllTabs();
			IsTabAlertsSelected = true;
			View.SwitchToTab(HomeTab.Alerts);

			if (mIsAlertsNeedLoad)
			{
				mIsAlertsNeedLoad = false;
				AlertsVM.LoadData(false);
			}
		}
		#endregion

		#endregion

		#region Methods

		#region UnSelectAllTabs

		private void UnSelectAllTabs()
		{
			IsTabProfileSelected = false;
			IsTabProjectSelected = false;
			IsTabAlertsSelected = false;
		}

		#endregion

		#region Destroy

		public override void Destroy()
		{
			base.Destroy();
			UnRegisterMessengers();
		}

		#endregion

		#endregion

		#region Messenger
		MvxSubscriptionToken actionSubscriptionToken;
		MvxSubscriptionToken appStateSubscriptionToken;

		private void RegisterMessengers()
		{
			actionSubscriptionToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<ViewModelActionMessage>(HandleViewModelAction);
			appStateSubscriptionToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<ChangeAppStateMessage>(HandleAppStateAction);
		}

		private void HandleViewModelAction(ViewModelActionMessage obj)
		{
			if (obj.Action.ViewModelType == this.GetType())
			{
				if (obj.Action.ActionType == ViewModelActionType.Reload)
				{
					if (obj.Action.SubViewModelType == typeof(ProjectsViewModel))
					{
						mIsProjectsNeedLoad = true;
					}
				}
				else if (obj.Action.ActionType == ViewModelActionType.Add)
				{
					if (obj.Action.SubViewModelType == typeof(ProjectsViewModel))
					{
						if (obj.Action.Data is ProjectHeading)
						{
							var projectHeading = obj.Action.Data as ProjectHeading;
							mProjectsVM.AddProjectHeading(projectHeading);
						}
					}
				}
				else if (obj.Action.ActionType == ViewModelActionType.Remove)
				{
					if (obj.Action.SubViewModelType == typeof(ProjectsViewModel))
					{
						Nullable<long> projectId = obj.Action.Data as Nullable<long>;
						if (projectId != null)
						{
							mProjectsVM.RemoveProject(projectId.Value);
						}
					}
				}
				else if (obj.Action.ActionType == ViewModelActionType.Update)
				{
					if (obj.Action.SubViewModelType == typeof(ProjectsViewModel))
					{
						if (obj.Action.Data is Project)
						{
							var project = obj.Action.Data as Project;
							mProjectsVM.UpdateProject(project);
						}
					}
				}
			}
		}

		private void HandleAppStateAction(ChangeAppStateMessage obj)
		{
			if (obj.AppState == AppState.Active)
			{
				// get alert background and count
				if (AlertsVM != null)
				{
					AlertsVM.LoadData(false);
				}
			}
			else if (obj.AppState == AppState.InActive)
			{
			}
		}

		private void UnRegisterMessengers()
		{
			if (actionSubscriptionToken != null)
			{
				Mvx.Resolve<IMvxMessenger>().Unsubscribe<ViewModelActionMessage>(actionSubscriptionToken);
				actionSubscriptionToken = null;
			}

			if (appStateSubscriptionToken != null)
			{
				Mvx.Resolve<IMvxMessenger>().Unsubscribe<ChangeAppStateMessage>(appStateSubscriptionToken);
				appStateSubscriptionToken = null;
			}
		}

		#endregion
	}
}
