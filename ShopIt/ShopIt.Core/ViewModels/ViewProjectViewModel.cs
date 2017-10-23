using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using ShopIt.Core.Messengers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;

namespace ViewModels
{
	public interface IViewProject
	{
		void ShowInvitee();
		void HidenInvitee();
	}

	public class ViewProjectViewModel : BaseViewModel
	{
		#region Constructors
		public ViewProjectViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region Init
		public async void Init()
		{
			if (mPlatformService.OS == OS.Droid)
			{
				await Task.Delay(50);
			}
			InitData();
			RegisterMessengers();
		}
		#endregion

		#region Properties

		#region Private

		#region mInvitesStatic

		private MvxObservableCollection<Invite> mInvitesStatic = new MvxObservableCollection<Invite>();

		#endregion

		#region View

		public IViewProject View { get; set; }

		#endregion

		#region TimesClickShowMore
		private int TimesClickShowMore = 0;
		#endregion

		#endregion

		#region Project
		private Project mProject;

		public Project Project
		{
			get
			{
				return mProject;
			}
			set
			{
				mProject = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ProjectName
		private string mProjectName;

		public string ProjectName
		{
			get
			{
				return mProjectName;
			}
			set
			{
				mProjectName = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region OwnerName         private string mOwnerName;  		public string OwnerName
		{ 			get
			{ 				return mOwnerName; 			} 			set
			{ 				mOwnerName = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region OwnerType         private string mOwnerType;  		public string OwnerType
		{ 			get
			{ 				return mOwnerType; 			} 			set
			{ 				mOwnerType = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region Owner         private string mOwner;  		public string Owner
		{ 			get
			{ 				return mOwner; 			} 			set
			{ 				mOwner = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#region Description
		private string mDescription;

		public string Description
		{
			get
			{
				return mDescription;
			}
			set
			{
				mDescription = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowMoreButton
		private bool mIsShowMoreButton = true;

		public bool IsShowMoreButton
		{
			get
			{
				return mIsShowMoreButton;
			}
			set
			{
				mIsShowMoreButton = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region SearchText
		private string mTextSearch;

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
					if (Invites != null)
					{
						Invites.Clear();
						var invites = mInvitesStatic.Where(x => x.InviteeEmail.ToLower().Contains(mTextSearch.ToLower()));
						if (invites != null)
						{
							foreach (var item in invites)
							{
								Invites.Add(item);
							}
						}

						IsShowMoreButton = false;
					}
				}
				else
				{
					Invites = mInvitesStatic;
					IsShowMoreButton = true;
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Invites
		private MvxObservableCollection<Invite> mInvites = new MvxObservableCollection<Invite>();

		public MvxObservableCollection<Invite> Invites
		{
			get
			{
				return mInvites;
			}
			set
			{
				mInvites = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsLoading
		private bool mIsLoading = true;

		public bool IsLoading
		{
			get
			{
				return mIsLoading;
			}
			set
			{
				mIsLoading = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CanEdit
		private bool mCanEdit = false;

		public bool CanEdit
		{
			get
			{
				return mCanEdit;
			}
			set
			{
				mCanEdit = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region IsShowInvitee         private bool mIsShowInvitee;  		public bool IsShowInvitee
		{ 			get
			{ 				return mIsShowInvitee; 			} 			set
			{ 				mIsShowInvitee = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#endregion

		#region Commands 

		#region ClearSearchTextCommand
		private MvxCommand mClearSearchTextCommand;

		public MvxCommand ClearSearchTextCommand
		{
			get
			{
				if (mClearSearchTextCommand == null)
				{
					mClearSearchTextCommand = new MvxCommand(this.ClearSearchText);
				}
				return mClearSearchTextCommand;
			}
		}

		private void ClearSearchText()
		{
			TextSearch = "";
		}
		#endregion

		#region ShowMoreInvitesCommand
		private MvxCommand mShowMoreInvitesCommand;

		public MvxCommand ShowMoreInvitesCommand
		{
			get
			{
				if (mShowMoreInvitesCommand == null)
				{
					mShowMoreInvitesCommand = new MvxCommand(this.ShowMoreInvites);
				}
				return mShowMoreInvitesCommand;
			}
		}
		private void ShowMoreInvites()
		{
			TimesClickShowMore++;

			Invites.Clear();

			var itemIndex = TimesClickShowMore * 3;

			if (itemIndex >= mInvitesStatic.Count)
			{
				foreach (var item in mInvitesStatic)
				{
					Invites.Add(item);
				}

				IsShowMoreButton = false;
			}
			else
			{
				for (int i = 0; i < itemIndex; i++)
				{
					Invites.Add(mInvitesStatic[i]);
				}
				IsShowMoreButton = true;
			}
		}
		#endregion

		#region EditProjectCommand
		private MvxCommand mEditProjectCommand;

		public MvxCommand EditProjectCommand
		{
			get
			{
				if (mEditProjectCommand == null)
				{
					mEditProjectCommand = new MvxCommand(this.EditProject);
				}
				return mEditProjectCommand;
			}
		}

		private void EditProject()
		{
			mCacheService.ProjectItem = mProject;
			mCacheService.EdittingProjectInvitee = Invites;
			mCacheService.EdittingProjectInviteeStatic = mInvitesStatic;
			ShowViewModel<EditProjectViewModel>();
		}
		#endregion

		#region SelectInviteCommand
		private MvxCommand<Invite> mSelectInviteCommand;

		public MvxCommand<Invite> SelectInviteCommand
		{
			get
			{
				if (mSelectInviteCommand == null)
				{
					mSelectInviteCommand = new MvxCommand<Invite>(this.SelectInvite);
				}
				return mSelectInviteCommand;
			}
		}

		private void SelectInvite(Invite invite)
		{
			// go to view profile
			if (invite.InviteeUserId != null && invite.ProfileComplete && invite.JoinedProject != null && mCacheService.CurrentUserData.UserId == Project.UserId)
			{
				ShowViewModel<ViewProfileViewModel>(new { isThisUser = false, userId = invite.InviteeUserId.Value.ToString() });
			}
			else if (!invite.ProfileComplete)
			{
				mMessageboxService.ShowToast("User hasn't completed personal profile");
			}
			else
			{
				mMessageboxService.ShowToast("This invitation is still pending");
			}
		}
		#endregion

		#endregion

		#region Method

		#region InitData
		public async void InitData()
		{
			if (mCacheService.ProjectHeadingItem != null)
			{

				var projectId = mCacheService.ProjectHeadingItem.ProjectHeading.ProjectId;
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var response = await mApiService.ViewProject(projectId);



				mInvitesStatic.Clear();
				Invites.Clear();
				TimesClickShowMore = 0;

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					Project = response.Data;
					if (Project.UserId == mCacheService.CurrentUserData.UserId)
					{
						IsShowInvitee = true;
						View.ShowInvitee();
					}
					else
					{
						IsShowInvitee = false;
						View.HidenInvitee();
					}
					LoadProject(Project);
					IsLoading = false;
				}
				else
				{
					IsShowMoreButton = false;
				}
				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();
			}
		}
		#endregion

		#region LoadProject

		public void LoadProject(Project project)
		{
			if (project.Owner.CompanyName == null || project.Owner.CompanyName == string.Empty)
			{
				OwnerName = project.Owner.Name;
				OwnerType = string.Empty;
				Owner = OwnerName;
			}
			else
			{
				OwnerName = project.Owner.CompanyName;
				OwnerType = "Company";
				Owner = OwnerType + " " + OwnerName;
			}

			mInvitesStatic.Clear();
			Invites.Clear();
			TimesClickShowMore = 0;

			this.ProjectName = project.Title;
			this.Description = project.Description;
			this.CanEdit = mCacheService.ProjectHeadingItem.ProjectHeading.UserIsCreator;



			foreach (var item in project.Invites)
			{
				mInvitesStatic.Add(item);
			}

			if (mInvitesStatic.Count > 3)
			{
				for (int i = 0; i < 3; i++)
				{
					Invites.Add(mInvitesStatic[i]);
				}
				IsShowMoreButton = true;
			}
			else
			{
				foreach (var item in mInvitesStatic)
				{
					Invites.Add(item);
				}
				IsShowMoreButton = false;
			}
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

		private void RegisterMessengers()
		{
			actionSubscriptionToken = Mvx.Resolve<IMvxMessenger>().SubscribeOnMainThread<ViewModelActionMessage>(HandleViewModelAction);
		}

		private void HandleViewModelAction(ViewModelActionMessage obj)
		{
			if (obj.Action.ViewModelType == this.GetType())
			{
				if (obj.Action.ActionType == ViewModelActionType.Update)
				{
					Project project = obj.Action.Data as Project;
					if (project != null)
					{
						LoadProject(project);
						mProject = project;
					}
				}
			}
		}
		private void UnRegisterMessengers()
		{
			if (actionSubscriptionToken != null)
			{
				Mvx.Resolve<IMvxMessenger>().Unsubscribe<ViewModelActionMessage>(actionSubscriptionToken);
				actionSubscriptionToken = null;
			}
		}

		#endregion
	}
}
