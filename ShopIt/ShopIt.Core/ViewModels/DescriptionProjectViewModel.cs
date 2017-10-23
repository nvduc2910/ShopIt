 using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using ShopIt.Core;
using ShopIt.Core.Messengers;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;

namespace ViewModels.Items
{
	public class DescriptionProjectViewModel : BaseViewModel
	{
		#region Constructors
		public DescriptionProjectViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{

		}

		#endregion

		#region Properties

		#region Description
		private string mDescription = string.Empty;

		public string Description
		{
			get
			{
				return mDescription;
			}
			set
			{
				if (value.Length <= 200)
					mDescription = value;
				LenghtCharactor = mDescription.Length.ToString();
				RaisePropertyChanged();
			}
		}
		#endregion

		#region LenghtCharactor
		private string mLenghtCharacters =  "0";

		public string LenghtCharactor
		{
			get
			{
				return mLenghtCharacters;
			}
			set
			{
				mLenghtCharacters = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
			Description = mCacheService.TempDescription;
		}
		#endregion

		#region Commands

		#region SaveCommand
        private MvxCommand mSaveCommand;

		public MvxCommand SaveCommand
		{
			get
			{
				if (mSaveCommand == null)
				{
					mSaveCommand = new MvxCommand(this.Save);
				}
				return mSaveCommand;
			}
		}

		private void Save()
		{
			mCacheService.TempDescription = Description;
			ViewModelAction action = new ViewModelAction
			{
				ActionType = ViewModelActionType.Reload,
				ViewModelType = typeof(CreateProjectViewModel),
				Data = Description
			};
			Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action));

			ViewModelAction action1 = new ViewModelAction
			{
				ActionType = ViewModelActionType.Reload,
				ViewModelType = typeof(EditProjectViewModel),
				Data = Description
			};
			Mvx.Resolve<IMvxMessenger>().Publish(new ViewModelActionMessage(this, action1));

			Close(this);
		}
		#endregion

		#endregion

		#region Methods

		#endregion
	}
}
