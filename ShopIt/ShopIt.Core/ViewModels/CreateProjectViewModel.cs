using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Validators;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using ShopIt.Core;
using ShopIt.Core.Messengers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using ViewModels.Items;
using ShopIt.Core.ViewModels.Items;
using System.Threading;
using Services;

namespace ViewModels
{

	public interface ICreateProjectView
	{
		void HidenKeyboard();
	}

	public class CreateProjectViewModel : BaseViewModel
	{ 
		#region Constructor
		public CreateProjectViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
			
		}
		#endregion

		#region Properties

		#region Private
		bool isClickSuggestList;
 		#endregion


		#region View 

		public ICreateProjectView View { get; set; }

		#endregion

		#region Id
        private int mId;

		public int Id
		{
			get
			{
				return mId;
			}
			set
			{
				mId = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Title
		private string mTitle;

		public string Title
		{
			get
			{
				return mTitle;
			}
			set
			{
				mTitle = value;
				LeghtTitle = mTitle.Length.ToString() + "/20";
				RaisePropertyChanged();
			}
		}
		#endregion

		#region UserId
		private int mUserId;

		public int UserId
		{
			get
			{
				return mUserId;
			}
			set
			{
				mUserId = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Description
		private string mDescription = "Add a project description";

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

		#region Stage
		private ProjectStage mStage;

		public ProjectStage Stage
		{
			get
			{
				return mStage;
			}
			set
			{
				mStage = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Created
		private DateTime mCreated;

		public DateTime Created
		{
			get
			{
				return mCreated;
			}
			set
			{
				mCreated = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Started
		private DateTime mStarted;

		public DateTime Started
		{
			get
			{
				return mStarted;
			}
			set
			{
				mStarted = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Updated
		private DateTime mUpdated;

		public DateTime Updated
		{
			get
			{
				return mUpdated;
			}
			set
			{
				mUpdated = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Invites
		private MvxObservableCollection<InviteCreateProjectItemViewModel> mInvites = new MvxObservableCollection<InviteCreateProjectItemViewModel>();

		public MvxObservableCollection<InviteCreateProjectItemViewModel> Invites
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

		#region IsSelecteTender
		private bool mIsSelecteTender = true;

		public bool IsSelecteTender
		{
			get
			{
				return mIsSelecteTender;
			}
			set
			{
				mIsSelecteTender = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region LeghtTitle
        private string mLeghtTitle = "0/20";

		public string LeghtTitle
		{
			get
			{
				return mLeghtTitle;
			}
			set
			{
				mLeghtTitle = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region EmailInvite
		private string mEmailInvite;

		public string EmailInvite
		{
			get
			{
				return mEmailInvite;
			}
			set
			{
				mEmailInvite = value;
				if (!string.IsNullOrEmpty(mEmailInvite))
				{
					SearchSuggestedContacts();
				}
				else
				{
					ShowSuggestedContacts = false;
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region InviteCreateProjectItem
		private InviteCreateProjectItemViewModel mInviteCreateProjectItem;

		public InviteCreateProjectItemViewModel InviteCreateProjectItem
		{
			get
			{
				return mInviteCreateProjectItem;
			}
			set
			{
				mInviteCreateProjectItem = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region SuggestContacts
		private MvxObservableCollection<Invite> mSuggestedContacts = new MvxObservableCollection<Invite>();

		public MvxObservableCollection<Invite> SuggestedContacts
		{
			get
			{
				return mSuggestedContacts;
			}
			set
			{
				mSuggestedContacts = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region ShowSuggestedContacts
		private bool mShowSuggestedContacts = false;

		public bool ShowSuggestedContacts
		{
			get
			{
				return mShowSuggestedContacts;
			}
			set
			{
				mShowSuggestedContacts = value;
				RaisePropertyChanged();
			}
		}

		#endregion

		#region IsShowButtonTapInvite 		private bool mIsShowButtonTapInvite = true;  		public bool IsShowButtonTapInvite
		{ 			get
			{ 				return mIsShowButtonTapInvite; 			} 			set
			{ 				mIsShowButtonTapInvite = value; 				RaisePropertyChanged(); 			} 		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
			mCacheService.TempDescription = string.Empty;
			RegisterMessengers();
		}
		#endregion

		#region Commands

		#region TenderCommand
		private MvxCommand mTenderCommand;

		public MvxCommand TenderCommand
		{
			get
			{
				if (mTenderCommand == null)
				{
					mTenderCommand = new MvxCommand(this.Tender);
				}

				return mTenderCommand;
			}
		}

		private void Tender()
		{
			IsSelecteTender = true;
			Stage = ProjectStage.Tender;
		}
		#endregion

		#region SiteCommand
		private MvxCommand mSiteCommand;

		public MvxCommand SiteCommand
		{
			get
			{
				if (mSiteCommand == null)
				{
					mSiteCommand = new MvxCommand(this.Site);
				}
				return mSiteCommand;
			}
		}

		private void Site()
		{
			IsSelecteTender = false;
			Stage = ProjectStage.Site;
		}
		#endregion

		#region AddDescriptionCommand
		private MvxCommand mAddDescriptionCommand;

		public MvxCommand AddDescriptionCommand
		{
			get
			{
				if (mAddDescriptionCommand == null)
				{
					mAddDescriptionCommand = new MvxCommand(this.AddDescription);
				}
				return mAddDescriptionCommand;
			}
		}

		private void AddDescription()
		{
			ShowViewModel<DescriptionProjectViewModel>();
		}
		#endregion

		#region ClearProjectNameCommand
		private MvxCommand mClearProjectNameCommand;

		public MvxCommand ClearProjectNameCommand
		{
			get
			{
				if (mClearProjectNameCommand == null)
				{
					mClearProjectNameCommand = new MvxCommand(this.ClearProjectName);
				}
				return mClearProjectNameCommand;
			}
		}

		private void ClearProjectName()
		{
			Title = string.Empty;
		}
		#endregion

		#region CloseCommand
        private MvxCommand mCloseCommand;

		public MvxCommand CloseCommand
		{
			get
			{
				if (mCloseCommand == null)
				{
					mCloseCommand = new MvxCommand(this.Close);
				}
				return mCloseCommand;
			}
		}

		private void Close()
		{
			GoBack();
		}
		#endregion

		#region InviteCommand
        private MvxCommand mInviteCommand;

		public MvxCommand InviteCommand
		{
			get
			{
				if (mInviteCommand == null)
				{
					mInviteCommand = new MvxCommand(this.Invite);
				}
				return mInviteCommand;
			}
		}

		private void Invite()
		{
			if (EmailInvite != string.Empty && EmailInvite != null)
			{
				Validators.EmailValidator validator = new Validators.EmailValidator();

				var result = validator.Validate(EmailInvite);

				if (result.IsValid)
				{
					CancelAllSearch();
					Invite item = new ShopIt.Core.Models.Invite();
					item.InviteeEmail = EmailInvite;
					Invites.Add(new InviteCreateProjectItemViewModel(this, item));
					Mvx.Resolve<ITrackingService>().SendEvent("Invite a user on create project");
					RaisePropertyChanged("Invites");
					EmailInvite = string.Empty;
					IsShowButtonTapInvite = false;
				}
				else if (result.Errors != null)
				{
					var firstError = result.Errors.First();
					mMessageboxService.ShowToast(firstError.ErrorMessage);
				}
			}
			else
			{
				View.HidenKeyboard();
			}

		}
		#endregion

		#region SaveProjectCommand
		private MvxAsyncCommand mSaveProjectCommand;

		public MvxAsyncCommand SaveProjectCommand
		{
			get
			{
				if (mSaveProjectCommand == null)
				{
					mSaveProjectCommand = new MvxAsyncCommand(this.SaveProject);
				}
				return mSaveProjectCommand;
			}
		}

		private async Task SaveProject()
		{
			View.HidenKeyboard();

			if (EmailInvite != string.Empty)
			{
				Invite();
			}

			if (Title == null || Title == "")
			{
				mMessageboxService.ShowToast(Messages.EmptyProjectName);
				return;
			}
			if (string.IsNullOrEmpty(mCacheService.TempDescription))
			{
				mMessageboxService.ShowToast(Messages.EmptyDescriptionProjectName);
				return;
			}
			else
			{
				Description = mPlatformService.GetPreference("Description");
				//mPlatformService.SetPreference("Description", "");
			}


			var inviteData = new List<Invite>();

			for (int i = 0; i < Invites.Count; i++)
			{
				inviteData.Add(Invites[i].Invite);
			}

			Project project = new Project()
			{
				UserId = Mvx.Resolve<ICacheService>().CurrentUserData.UserId,
				Title = Title,
				Description = mCacheService.TempDescription,
				Stage = Stage,
				Created = DateTime.Now,
				Started = DateTime.Now,
				Updated = DateTime.Now,
				Invites = inviteData
			};

			mPlatformService.ShowNetworkIndicator();
			mProgressDialogService.ShowProgressDialog();

			var response = await mApiService.CreateProject(project);

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var data = response.Data;
				mMessageboxService.ShowToast("Add project successful");
				Mvx.Resolve<ITrackingService>().SendEvent("Created Project");
				mPlatformService.SetPreference("Description", String.Empty);

				var newProject = new ProjectHeading
				{
					ProjectId = (long)data.Id,
					Stage = data.Stage,
					Title = data.Title,
					UserIsCreator = true,
					CreatedJoined = (System.DateTimeOffset)data.Created,
					Alert = false,
				};

				ViewModelAction action = new ViewModelAction
				{
					ActionType = ViewModelActionType.Add,
					ViewModelType = typeof(HomeViewModel),
					SubViewModelType = typeof(ProjectsViewModel),
					Data = newProject
				};
				Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action));
				Close(this);
			}
			else
			{
				string errorData = response.ErrorData;
				if (!string.IsNullOrEmpty(errorData))
					mMessageboxService.ShowToast(errorData);
			}

		}
		#endregion

		#region SelectSuggestedContactCommand

		private MvxCommand<Invite> mSelectSuggestedContactCommand;

		public MvxCommand<Invite> SelectSuggestedContactCommand
		{
			get
			{
				if (mSelectSuggestedContactCommand == null)
				{
					mSelectSuggestedContactCommand = new MvxCommand<Invite>(this.SelectSuggestedContact);
				}
				return mSelectSuggestedContactCommand;
			}
		}

		private void SelectSuggestedContact(Invite contact)
		{
			CancelAllSearch();
			Invites.Add(new InviteCreateProjectItemViewModel(this, contact));
			EmailInvite = string.Empty;
		}

		#endregion

		#endregion

		#region Method

		#region SearchSuggestedContacts

		private CancellationTokenSource mSearchTokenSource;

		private void SearchSuggestedContacts()
		{
			if (mSearchTokenSource != null)
			{
				mSearchTokenSource.Cancel();
				mSearchTokenSource = null;
			}

			mSearchTokenSource = new CancellationTokenSource();
			var token = mSearchTokenSource.Token;
			Task.Run(async () =>
			{
				await Task.Delay(500, token);

				mPlatformService.ShowNetworkIndicator();

				var result = await mApiService.GetContactList(EmailInvite);

				mPlatformService.HideNetworkIndicator();

				if (result.StatusCode == System.Net.HttpStatusCode.OK && result.Data != null)
				{
					var realList = result.Data.Where(p => !Invites.Any(ct => ct.Invite.InviteeEmail == p.InviteeEmail));
					if (realList.Count() > 0)
					{
						ShowSuggestedContacts = true;
						SuggestedContacts = new MvxObservableCollection<Invite>(realList);
					}
					else
					{
						ShowSuggestedContacts = false;
					}
				}
				else
				{
					ShowSuggestedContacts = false;
				}

			}, token);;
		}

		private void CancelAllSearch()
		{
			if (mSearchTokenSource != null)
			{
				mSearchTokenSource.Cancel();
				mSearchTokenSource = null;
			}
			ShowSuggestedContacts = false;
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
				if (obj.Action.ActionType == ViewModelActionType.Reload)
				{
					string description = obj.Action.Data as string;
					if (description != null && description != "")
					{
						Description = description;
					}
					else if (description == null || description == string.Empty)
					{
						Description = "Add a project description";
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
