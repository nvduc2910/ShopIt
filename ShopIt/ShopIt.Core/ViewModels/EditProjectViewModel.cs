using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Validators;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using ShopIt.Core.Messengers;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;
using ViewModels.Items;
using ShopIt.Core.Validators;
using System.Threading;
using ShopIt.Core.ViewModels.Items;
using Services;

namespace ViewModels
{
	public interface IEditProjectView
	{
		void HideKeyboard();
	}

	public class EditProjectViewModel : BaseViewModel
	{
		#region Constructors
		public EditProjectViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
			
		}
		#endregion

		#region Init
		public void Init()
		{
			if (mCacheService.ProjectItem != null)
			{
				mProjectItem = mCacheService.ProjectItem;
				Title = mProjectItem.Title;
				Description = mProjectItem.Description;
				Stage = mProjectItem.Stage;

				var invitesAccepted = mProjectItem.Invites.Where(s => s.JoinedProject != null);
				foreach (var item in invitesAccepted)
				{
					mCurrentTracesStatic.Add(new CurrentTradesItemViewModel(this, item));
				}

				if (mCurrentTracesStatic.Count > 3)
				{
					for (int i = 0; i < 3; i++)
					{
						CurrentTrades.Add(mCurrentTracesStatic[i]);
					}
					IsShowMoreButton = true;
				}
				else
				{
					foreach (var item in mCurrentTracesStatic)
					{
						CurrentTrades.Add(item);
					}
					IsShowMoreButton = false;
				}

				var intvitesPending = mProjectItem.Invites.Where(s => s.JoinedProject == null);
				foreach (var invite in intvitesPending)
				{
					InviteTrades.Add(new PendingTradesItemViewModel(this, invite) { EditProjectViewModel = this, PendingTrades = invite });
				}
			}

			RegisterMessengers();
		}
		#endregion

		#region View

		public IEditProjectView View { get; set; }

		#endregion

		#region Properties

		#region Private

		#region mCurrentTracesStatic
		private List<CurrentTradesItemViewModel> mCurrentTracesStatic = new List<CurrentTradesItemViewModel>();
		#endregion

		#region TimesClickShowMore
		private int TimesClickShowMore = 0;
		#endregion

		#region ProjectItem
		private Project mProjectItem;
		#endregion

		#endregion

		#region CurrentTradesItemViewModel
        private CurrentTradesItemViewModel mCurrentTradesItemViewModel;

		public CurrentTradesItemViewModel CurrentTradesItemViewModel
		{
			get
			{
				return mCurrentTradesItemViewModel;
			}
			set
			{
				mCurrentTradesItemViewModel = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region PendingTradesItemViewModel
		private PendingTradesItemViewModel mPendingTradesItemViewModel;

		public PendingTradesItemViewModel PendingTradesItemViewModel
		{
			get
			{
				return mPendingTradesItemViewModel;
			}
			set
			{
				mPendingTradesItemViewModel = value;
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
				if (mStage == ProjectStage.Site)
				{
					IsSiteStage = true;
					IsTenderStage = false;
				}
				else
				{
					IsSiteStage = false;
					IsTenderStage = true;
				}
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

		#region CurrentTrades
		private MvxObservableCollection<CurrentTradesItemViewModel> mCurrentTrades = new MvxObservableCollection<CurrentTradesItemViewModel>();

		public MvxObservableCollection<CurrentTradesItemViewModel> CurrentTrades
		{
			get
			{
				return mCurrentTrades;
			}
			set
			{
				mCurrentTrades = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsTenderStage
		private bool mIsTenderStage;

		public bool IsTenderStage
		{
			get
			{
				return mIsTenderStage;
			}
			set
			{
				mIsTenderStage = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsSiteStage
		private bool mIsSiteStage;

		public bool IsSiteStage
		{
			get
			{
				return mIsSiteStage;
			}
			set
			{
				mIsSiteStage = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region InviteTrades
		private MvxObservableCollection<PendingTradesItemViewModel> mInviteTrades = new MvxObservableCollection<PendingTradesItemViewModel>();

		public MvxObservableCollection<PendingTradesItemViewModel> InviteTrades
		{
			get
			{
				return mInviteTrades;
			}
			set
			{
				mInviteTrades = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowMoreButton
		private bool mIsShowMoreButton;

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
			Stage = ProjectStage.Site;
		}
		#endregion

		#region DeleteProjectCommand
		private MvxAsyncCommand mDeleteProjectCommand;

		public MvxAsyncCommand DeleteProjectCommand
		{
			get
			{
				if (mDeleteProjectCommand == null)
				{
					mDeleteProjectCommand = new MvxAsyncCommand(this.DeleteProject);
				}
				return mDeleteProjectCommand;
			}
		}

		private async Task DeleteProject()
		{
			mPlatformService.ShowNetworkIndicator();
			mProgressDialogService.ShowProgressDialog();

			var succeed = await mApiService.DeleteProject(mProjectItem.Id.Value);

			mPlatformService.HideNetworkIndicator();
			mProgressDialogService.HideProgressDialog();

			if (succeed)
			{
				ViewModelAction action = new ViewModelAction
				{
					ActionType = ViewModelActionType.Remove,
					ViewModelType = typeof(HomeViewModel),
					SubViewModelType = typeof(ProjectsViewModel),
					Data = mProjectItem.Id
				};
				Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action));
				BackToViewModel<MainViewModel>();
			}
		}
		#endregion

		#region SaveAndUpdateCommand
		private MvxAsyncCommand mSaveAndUpdateCommand;

		public MvxAsyncCommand SaveAndUpdateCommand
		{
			get
			{
				if (mSaveAndUpdateCommand == null)
				{
					mSaveAndUpdateCommand = new MvxAsyncCommand(this.SaveAndUpdate);
				}
				return mSaveAndUpdateCommand;
			}
		}

		private async Task SaveAndUpdate()
		{
			View.HideKeyboard();

			ProjectValidator validator = new ProjectValidator();

			var project = new Project
			{
				Title = Title,
				Description = Description,
				Stage = Stage,
				Invites = mProjectItem.Invites,
				Id = mProjectItem.Id,
				UserId = mProjectItem.UserId,
				Owner = mProjectItem.Owner,
				Active = true
			};

			var result = validator.Validate(project);
			if (result.IsValid)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var succeed = await mApiService.UpdateProject(project);

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (succeed)
				{
					mMessageboxService.ShowToast("Saved!");
					mCacheService.ProjectHeadingItem.ProjectHeading.Title = Title;
					mCacheService.ProjectHeadingItem.RaisePropertyChanged("ProjectHeading");

					// update the view project
					ViewModelAction action = new ViewModelAction
					{
						ActionType = ViewModelActionType.Update,
						ViewModelType = typeof(ViewProjectViewModel),
						Data = project
					};
					Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action));

					// update the project tabs
					ViewModelAction action2 = new ViewModelAction
					{
						ActionType = ViewModelActionType.Update,
						ViewModelType = typeof(HomeViewModel),
						SubViewModelType = typeof(ProjectsViewModel),
						Data = project
					};
					Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action2));

					Close(this);
				}
			}
			else if (result.Errors != null)
			{
				var firstError = result.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
			}
		}
		#endregion

		#region SendInviteCommand
		private MvxAsyncCommand mSendInviteCommand;

		public MvxAsyncCommand SendInviteCommand
		{
			get
			{
				if (mSendInviteCommand == null)
				{
					mSendInviteCommand = new MvxAsyncCommand(this.SendInvite);
				}
				return mSendInviteCommand;
			}
		}

		private async Task SendInvite()
		{
			View.HideKeyboard();

			InviteEmailValidator validator = new InviteEmailValidator();
			InviteEmail inviteEmail = new InviteEmail(EmailInvite, mProjectItem.Invites);

			var result = validator.Validate(inviteEmail);

			if (result.IsValid)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var response = await mApiService.AddInvitee(mProjectItem.Id.Value, EmailInvite, null);

				Mvx.Resolve<ITrackingService>().SendEvent("Invite a user in edit project");

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Data != null)
				{
					Invite invite = response.Data;
					InviteTrades.Add(new PendingTradesItemViewModel(this, invite) { PendingTrades = invite, EditProjectViewModel = this });
					mCacheService.ProjectItem.Invites.Add(invite);
					mCacheService.EdittingProjectInvitee.Add(invite);
					EmailInvite = string.Empty;
				}
				else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				{
					mMessageboxService.ShowToast("Email is not exist");
					EmailInvite = string.Empty;
				}
			}
			else if (result.Errors != null)
			{
				var firstError = result.Errors.First();
				mMessageboxService.ShowToast(firstError.ErrorMessage);
			}
		}
		#endregion

		#region RemoveCurrentTradesCommand
		private MvxAsyncCommand<CurrentTradesItemViewModel> mRemoveCurrentTradesCommand;

		public MvxAsyncCommand<CurrentTradesItemViewModel> RemoveCurrentTradesCommand
		{
			get
			{
				if (mRemoveCurrentTradesCommand == null)
				{
					mRemoveCurrentTradesCommand = new MvxAsyncCommand<CurrentTradesItemViewModel>(this.RemoveCurrentTrades);
				}
				return mRemoveCurrentTradesCommand;
			}
		}

		private async Task RemoveCurrentTrades(CurrentTradesItemViewModel itemModel)
		{
			if (itemModel != null)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var reponse = await mApiService.DeleteInvitee(mProjectItem.Id.Value, itemModel.CurrentTrades.InviteeEmail, itemModel.CurrentTrades.InviteeUserId);

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (reponse)
				{
					CurrentTrades.Remove(itemModel);
					mCacheService.ProjectItem.Invites.Remove(itemModel.CurrentTrades);
					mCacheService.EdittingProjectInvitee.Remove(itemModel.CurrentTrades);
					mCacheService.EdittingProjectInviteeStatic.Remove(itemModel.CurrentTrades);
				}
			}
		}
		#endregion

		#region RemovePendingTradesCommand
		private MvxAsyncCommand<PendingTradesItemViewModel> mRemovePendingTradesCommand;

		public MvxAsyncCommand<PendingTradesItemViewModel> RemovePendingTradesCommand
		{
			get
			{
				if (mRemovePendingTradesCommand == null)
				{
					mRemovePendingTradesCommand = new MvxAsyncCommand<PendingTradesItemViewModel>(this.RemovePendingTrades);
				}
				return mRemovePendingTradesCommand;
			}
		}

		private async Task RemovePendingTrades(PendingTradesItemViewModel itemModel)
		{
			if (itemModel != null)
			{
				mPlatformService.ShowNetworkIndicator();
				mProgressDialogService.ShowProgressDialog();

				var reponse = await mApiService.DeleteInvitee(mProjectItem.Id.Value, itemModel.PendingTrades.InviteeEmail, itemModel.PendingTrades.InviteeUserId);

				mPlatformService.HideNetworkIndicator();
				mProgressDialogService.HideProgressDialog();

				if (reponse)
				{
					InviteTrades.Remove(itemModel);
					mCacheService.ProjectItem.Invites.Remove(itemModel.PendingTrades);
					mCacheService.EdittingProjectInvitee.Remove(itemModel.PendingTrades);
					mCacheService.EdittingProjectInviteeStatic.Remove(itemModel.PendingTrades);
				}
			}
		}
		#endregion

		#region ShowMoreCurrentTracesCommand
        private MvxCommand mShowMoreCurrentTracesCommand;

		public MvxCommand ShowMoreCurrentTracesCommand
		{
			get
			{
				if (mShowMoreCurrentTracesCommand == null)
				{
					mShowMoreCurrentTracesCommand = new MvxCommand(this.ShowMoreCurrentTraces);
				}
				return mShowMoreCurrentTracesCommand;
			}
		}

		private void ShowMoreCurrentTraces()
		{
			TimesClickShowMore++;

			CurrentTrades.Clear();

			var itemIndex = TimesClickShowMore * 3;

			if (itemIndex >= mCurrentTracesStatic.Count)
			{
				//Invites = mInvitesStatic;

				foreach (var item in mCurrentTracesStatic)
				{
					CurrentTrades.Add(item);
				}

				IsShowMoreButton = false;
			}
			else
			{
				for (int i = 0; i < itemIndex; i++)
				{
					CurrentTrades.Add(mCurrentTracesStatic[i]);
				}
				IsShowMoreButton = true;
			}
		}
		#endregion

		#region ClearTitleCommand
		private MvxCommand mClearTitleCommand;

		public MvxCommand ClearTitleCommand
		{
			get
			{
				if (mClearTitleCommand == null)
				{
					mClearTitleCommand = new MvxCommand(this.ClearTitle);
				}
				return mClearTitleCommand;
			}
		}

		private void ClearTitle()
		{
			Title = string.Empty;
		}
		#endregion

		#region EditDescriptionCommand
		private MvxCommand mEditDescriptionCommand;

		public MvxCommand EditDescriptionCommand
		{
			get
			{
				if (mEditDescriptionCommand == null)
				{
					mEditDescriptionCommand = new MvxCommand(this.EditDescription);
				}
				return mEditDescriptionCommand;
			}
		}

		private void EditDescription()
		{
			mCacheService.TempDescription = Description;
			ShowViewModel<DescriptionProjectViewModel>();
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
			EmailInvite = contact.InviteeEmail;
		}

		#endregion

		#endregion

		#region Methods

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
					var realList = result.Data.Where(p => !mProjectItem.Invites.Any(ct => ct.InviteeEmail == p.InviteeEmail));
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

			}, token); ;
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
