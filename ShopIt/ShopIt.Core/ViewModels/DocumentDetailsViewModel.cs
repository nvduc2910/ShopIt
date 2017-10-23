using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels.Items;
using ViewModels.Items;

namespace ShopIt.Core.ViewModels
{
	public class DocumentDetailsViewModel : BaseViewModel
	{
		#region Constructors
		public DocumentDetailsViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}
		#endregion

		#region Properties

		#region CanEdit
		private bool mCanEdit;

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

		#region CertItem
		private CertTypeItemViewModel mCertItem;

		public CertTypeItemViewModel CertItem
		{
			get
			{
				return mCertItem;
			}
			set
			{
				mCertItem = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Media
		private string mMedia;

		public string Media
		{
			get
			{
				return mMedia;
			}
			set
			{
				mMedia = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region LocalMedia
		private byte[] mLocalMedia = null;

		public byte[] LocalMedia
		{
			get
			{
				return mLocalMedia;
			}
			set
			{
				mLocalMedia = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsHaveMedia
		private bool mIsHaveMedia = false;

		public bool IsHaveMedia
		{
			get
			{
				return mIsHaveMedia;
			}
			set
			{
				mIsHaveMedia = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region Titile
		private string mTitile;

		public string Titile
		{
			get
			{
				return mTitile;
			}
			set
			{
				mTitile = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init(bool canEdit)
		{
			this.CanEdit = canEdit;
			this.CertItem = mCacheService.CertTypeItem;
			Titile = CertItem.CertItem.CertType.Title;
			if (CertItem.CertItem != null)
			{
				if (CertItem.CertItem.Document != null && CertItem.CertItem.Document.Media != null)
				{
					LocalMedia = this.CertItem.LocalImage;
					Media = ApiConstants.AzureBlobUrl + CertItem.CertItem.Document.Media;
					IsHaveMedia = true;
				}
			}
		}
		#endregion

		#region Commands

		#region DeleteCommand
		private MvxCommand mDeleteCommand;

		public MvxCommand DeleteCommand
		{
			get
			{
				if (mDeleteCommand == null)
				{
					mDeleteCommand = new MvxCommand(this.Delete);
				}
				return mDeleteCommand;
			}
		}

		private void Delete()
		{
			mCertItem.CertItem.Document = null;
			mCertItem.CertItem.Descriptor = string.Empty;
			mCertItem.CertItem.Expiry = null;
			Close(this);
		}
		#endregion

		#region EditCommand
		private MvxAsyncCommand mEditCommand;

		public MvxAsyncCommand EditCommand
		{
			get
			{
				if (mEditCommand == null)
				{
					mEditCommand = new MvxAsyncCommand(this.Edit);
				}
				return mEditCommand;
			}
		}

		private async Task Edit()
		{
			if (!mCanEdit)
				return;
			
			var option = await mMessageboxService.ShowThreeOptions("Change document",
															"Please choose an option",
															 "Cancel", "Take photo", "Select from library");

			if (option > 0)
			{
				byte[] image;
				if (option == 1)
				{
					image = await Mvx.Resolve<IImageService>().TakePicture();
				}
				else
				{
					image = await Mvx.Resolve<IImageService>().SelectFromLibrary();
				}
				if (image != null)
				{
					LocalMedia = image;
					this.CertItem.LocalImage = image;
					var imageName = Guid.NewGuid().ToString() + ".jpg";
					var cancelToken = new CancellationTokenSource();

					mProgressDialogService.ShowProgressDialog();

					var taskUpload = System.Threading.Tasks.Task.Run(async () =>
					{
						
						var urlResponse = await Mvx.Resolve<IAzureService>().Upload(image, imageName);

						Debug.WriteLine("url: " + urlResponse);

						if (mCertItem.CertItem.Document == null)
						{
							mCertItem.CertItem.Document = new Models.Document() { Media = urlResponse.Replace("/ShopItstore", string.Empty), MediaCreated = DateTime.Now, UserProfileId = Mvx.Resolve<ICacheService>().CurrentUserData.UserId };
						}
						else
						{
							mCertItem.CertItem.Document.Media = urlResponse.Replace("/ShopItstore", string.Empty);
						}

					}, cancelToken.Token);
					Mvx.Resolve<ITaskManagementService>().AddTask(taskUpload, cancelToken);
					while (!Mvx.Resolve<ITaskManagementService>().IsAllTaskDone())
					{
						await Task.Delay(150);
					}

					mProgressDialogService.HideProgressDialog();
				}
			}
		}
		#endregion

		#region GoBack

		public override void GoBack()
		{
			base.GoBack();

			// check if have changes
		}

		#endregion

		#endregion

		#region Methods

		#endregion
	}
}
