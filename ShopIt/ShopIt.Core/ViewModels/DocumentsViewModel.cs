using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Services;
using System.Collections.ObjectModel;
using ShopIt.Core.ViewModels.Items;
using MvvmCross.Platform;
using ShopIt.Core.Models;

namespace ShopIt.Core.ViewModels
{
	public class DocumentsViewModel : BaseViewModel
	{
		#region Constructors
		public DocumentsViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region Properties

		#region DocumentItems
		private MvxObservableCollection<DocumentItemViewModel> mDocumentItems = new MvxObservableCollection<DocumentItemViewModel>();

		public MvxObservableCollection<DocumentItemViewModel> DocumentItems
		{
			get
			{
				return mDocumentItems;
			}
			set
			{
				mDocumentItems = value;
				RaisePropertyChanged();
			}
		}
		#endregion

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

		#endregion

		#region Init
		public void Init(bool canEdit)
		{
			this.CanEdit = canEdit;
		}
		#endregion

		#region Commands

		#region AddDocumentCommand
		private MvxCommand mAddDocumentCommand;

		public MvxCommand AddDocumentCommand
		{
			get
			{
				if (mAddDocumentCommand == null)
				{
					mAddDocumentCommand = new MvxCommand(this.AddDocument);
				}
				return mAddDocumentCommand;
			}
		}

		private async void AddDocument()
		{
			var option = await mMessageboxService.ShowThreeOptions("Add document",
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

					var path = Guid.NewGuid().ToString();

					Document document = new Document
					{
						
					};
					DocumentItemViewModel itemModel = new DocumentItemViewModel(document);
					DocumentItems.Add(itemModel);
				}
			}
		}
		#endregion

		#region SelectDocumenItemCommand
		private MvxCommand<DocumentItemViewModel> mSelectDocumenItemCommand;

		public MvxCommand<DocumentItemViewModel> SelectDocumenItemCommand
		{
			get
			{
				if (mSelectDocumenItemCommand == null)
				{
					mSelectDocumenItemCommand = new MvxCommand<DocumentItemViewModel>(this.SelectDocumenItem);
				}
				return mSelectDocumenItemCommand;
			}
		}

		private void SelectDocumenItem(DocumentItemViewModel documentItem)
		{
			//mCacheService.DocumentItems = this.DocumentItems;
			//mCacheService.DocumentItem = documentItem;
			ShowViewModel<DocumentDetailsViewModel>(new { canEdit = mCanEdit});
		}
		#endregion

		#endregion

		#region Methods

		#endregion
	}
}
