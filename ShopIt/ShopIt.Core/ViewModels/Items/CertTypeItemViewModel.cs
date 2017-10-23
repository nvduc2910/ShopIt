using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ShopIt.Core.Models;
using ShopIt.Core.Services;
using ShopIt.Core.ViewModels;

namespace ViewModels.Items
{
	public class CertTypeItemViewModel : MvxViewModel
	{
		#region Constructor
		public CertTypeItemViewModel(string certNameDisplay, bool isCanEdit = false)
		{
			this.CertNameDisplay = certNameDisplay;
			this.IsCanedit = isCanEdit;
		}
		#endregion

		#region Properties

		#region CreatePersonalProfileVM

		public CreatePersonalProfileViewModel CreatePersonalProfileVM { get; set; }

		#endregion

		#region EditProfileVM

		public EditProfileViewModel EditProfileVM { get; set; }

		#endregion

		#region CreateCompanyProfileVM
		private CreateCompanyProfileViewModel mCreateCompanyProfileVM;

		public CreateCompanyProfileViewModel CreateCompanyProfileVM
		{
			get
			{
				return mCreateCompanyProfileVM;
			}
			set
			{
				mCreateCompanyProfileVM = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CertItem
		private Cert mCertItem = new Cert();

		public Cert CertItem
		{
			get
			{
				return mCertItem;
			}
			set
			{
				mCertItem = value;
				ExpiryText = mCertItem == null ? "" : mCertItem.Expiry?.ToLocalTime().ToString("dd/MM/yyyy");
				AmountText = mCertItem.Amount == null ? "" : mCertItem.Amount.ToString().Replace(".0", string.Empty).Replace(",0", string.Empty);
				IsOverdue = mCertItem.Expiry < DateTime.Now ? true : false;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ExpiryText
		private string mExpiryText;
		public string ExpiryText
		{
			set
			{
				mExpiryText = value;
				RaisePropertyChanged();
			}
			get
			{
				return mExpiryText.Split(' ')[0];
			}
		}
		#endregion

		#region AmountText
		private string mAmountText;

		public string AmountText
		{
			get
			{
				return mAmountText;
			}
			set
			{
				mAmountText = value;
				var DollarTemp = mAmountText;
				if (DollarTemp != null && DollarTemp != string.Empty)
				{
					CertItem.Amount = Convert.ToDecimal(DollarTemp.Replace(",", string.Empty).Replace("$", string.Empty));
					mAmountText = ConvertDecimalToString(mAmountText);
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region LocalPathImage
		private string mLocalPathImage;

		public string LocalPathImage
		{
			get
			{
				return mLocalPathImage;
			}
			set
			{
				mLocalPathImage = value;
				if (mLocalPathImage != null || mLocalPathImage != "")
				{
					CertItem.Document = new Document() { LocalMedia = mLocalPathImage, Media = ServerPathImage, MediaCreated = DateTime.Now, UserProfileId = Mvx.Resolve<ICacheService>().CurrentUserData.UserId };
					RaisePropertyChanged("CertItem");
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region LocalImage
		private byte[] mLocalImage;

		public byte[] LocalImage
		{
			get
			{
				return mLocalImage;
			}
			set
			{
				mLocalImage = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CertNameDisplay
		private string mCertNameDisplay;

		public string CertNameDisplay
		{
			get
			{
				return mCertNameDisplay;
			}
			set
			{
				mCertNameDisplay = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region CancelUploadImageTask
		private CancelableTask mCancelUploadImageTask;

		public CancelableTask CancelUploadImageTask
		{
			get
			{
				return mCancelUploadImageTask;
			}
			set
			{
				mCancelUploadImageTask = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region ServerPathImage
		private string mServerPathImage;

		public string ServerPathImage
		{
			get
			{
				return mServerPathImage;
			}
			set
			{
				mServerPathImage = value;
				if (mServerPathImage != null || mServerPathImage != "")
				{
					CertItem.Document = new Document() { Media = mServerPathImage, LocalMedia = LocalPathImage, MediaCreated = DateTime.Now, UserProfileId = Mvx.Resolve<ICacheService>().CurrentUserData.UserId };
					RaisePropertyChanged("CertItem");
				}
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsCanEdit
		private bool mIsCanedit;

		public bool IsCanedit
		{
			get
			{
				return mIsCanedit;
			}
			set
			{
				mIsCanedit = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsOverdue
		private bool mIsOverdue;

		public bool IsOverdue
		{
			get
			{
				return mIsOverdue;
			}
			set
			{
				mIsOverdue = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#region IsShowOrignalImage
		private bool mIsShowOrignalImage;

		public bool IsShowOrignalImage
		{
			get
			{
				return mIsShowOrignalImage;
			}
			set
			{
				mIsShowOrignalImage = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Commands

		#region SetPersonalExpiryCommand
		private MvxCommand mSetPersonalExpiryCommand;
		public MvxCommand SetPersonalExpiryCommand
		{
			get
			{
				if (mSetPersonalExpiryCommand == null)
				{
					mSetPersonalExpiryCommand = new MvxCommand(this.SetTimeExpiry);
				}
				return mSetPersonalExpiryCommand;
			}
		}

		private void SetTimeExpiry()
		{
			if (CreatePersonalProfileVM != null)
			{
				CreatePersonalProfileVM.EdittingTimeItem = this;
				CreatePersonalProfileVM.View.HideKeyboard();
				CreatePersonalProfileVM.View.ShowSetTimeDialog();
				return;
			}
			if (EditProfileVM != null)
			{
				EditProfileVM.View.HidenKeyboard();
				EditProfileVM.EdittingTimeItem = this;
				EditProfileVM.View.ShowSetTimeDialog();
				return;
			}
		}
		#endregion

		#region EditExpiryCommand
		private MvxCommand mEditExpiryCommand;

		public MvxCommand EditExpiryCommand
		{
			get
			{
				if (mEditExpiryCommand == null)
				{
					mEditExpiryCommand = new MvxCommand(this.SetTimeEditExpiry);
				}
				return mEditExpiryCommand;
			}
		}
		private void SetTimeEditExpiry()
		{
			if (EditProfileVM != null)
			{
				EditProfileVM.View.HidenKeyboard();
				EditProfileVM.EdittingTimeItem = this;
				EditProfileVM.View.ShowSetTimeDialog();
			}

		}
		#endregion

		#region SetCompanyTimeExpiryCommand
		private MvxCommand mSetCompanyExpiryCommand;

		public MvxCommand SetCompanyExpiryCommand
		{
			get
			{
				if (mSetCompanyExpiryCommand == null)
				{
					mSetCompanyExpiryCommand = new MvxCommand(this.SetTimeForCompany);
				}
				return mSetCompanyExpiryCommand;
			}
		}

		private void SetTimeForCompany()
		{
			if (CreateCompanyProfileVM != null)
			{
				CreateCompanyProfileVM.EdittingTimeItem = this;
				CreateCompanyProfileVM.View.HideKeyboard();
				CreateCompanyProfileVM.View.ShowSetTimeDialog();
				return;
			}
			if (EditProfileVM != null)
			{
				EditProfileVM.View.HidenKeyboard();
				EditProfileVM.EdittingTimeItem = this;
				EditProfileVM.View.ShowSetTimeDialog();
				return;
			}
		}
		#endregion

		#region RemoveOperationalCertCommand
		private MvxCommand mRemoveOperationalCertCommand;

		public MvxCommand RemoveOperationalCertCommand
		{
			get
			{
				if (mRemoveOperationalCertCommand == null)
				{
					mRemoveOperationalCertCommand = new MvxCommand(this.RemoveOperationalCert);
				}
				return mRemoveOperationalCertCommand;
			}
		}

		private void RemoveOperationalCert()
		{
			if (CreatePersonalProfileVM != null)
			{
				CreatePersonalProfileVM.OperationalCerts.Remove(this);
			}
			if (EditProfileVM != null)
			{
				EditProfileVM.NewOperationalCerts.Remove(this);
				EditProfileVM.View.RemoveOperationalCert();
				//EditProfileVM.RaisePropertyChanged("NewOperationalCerts");
			}
		}
		#endregion

		#region DeleteOperationalCertCommand
		private MvxCommand mDeleteOperationalCertCommand;

		public MvxCommand DeleteOperationalCertCommand
		{
			get
			{
				if (mDeleteOperationalCertCommand == null)
				{
					mDeleteOperationalCertCommand = new MvxCommand(this.DeleteOperationalCert);
				}
				return mDeleteOperationalCertCommand;
			}
		}

		private async void DeleteOperationalCert()
		{
			if (EditProfileVM != null)
			{
				var option = await Mvx.Resolve<IMessageboxService>().ShowTwoOptions("Remove Certificate", "Do you want to remove this certificate?", "Cancel", "Yes");

				if (option)
				{
					EditProfileVM.OperationalCerts.Remove(this);
					//EditProfileVM.PersonalProfile.Certs.Remove(this.CertItem);
					//var item = 
					this.CertItem.Document = null;
					this.CertItem.Descriptor = string.Empty;
					this.CertItem.Expiry = null;
					EditProfileVM.RaisePropertyChanged("OperationalCerts");
				}
				else
				{

				}
			}
		}
		#endregion

		#region RemoveLicenceCertCommand
		private MvxCommand mRemoveLicenceCertCommand;

		public MvxCommand RemoveLicenceCertCommand
		{
			get
			{
				if (mRemoveLicenceCertCommand == null)
				{
					mRemoveLicenceCertCommand = new MvxCommand(this.RemoveLicenceCert);
				}
				return mRemoveLicenceCertCommand;
			}
		}

		private void RemoveLicenceCert()
		{
			if (CreateCompanyProfileVM != null)
			{
				CreateCompanyProfileVM.LicenceCerts.Remove(this);
			}
			else if (EditProfileVM != null)
			{
				EditProfileVM.NewLicences.Remove(this);
				EditProfileVM.View.RemoveLicences();
			}
		}
		#endregion

		#region ViewDocumentCommand
		private MvxCommand mViewDocumentCommand;

		public MvxCommand ViewDocumentCommand
		{
			get
			{
				if (mViewDocumentCommand == null)
				{
					mViewDocumentCommand = new MvxCommand(this.ViewDocument);
				}
				return mViewDocumentCommand;
			}
		}

		private void ViewDocument()
		{
			Mvx.Resolve<ICacheService>().CertTypeItem = this;
			ShowViewModel<DocumentDetailsViewModel>(new { canEdit = IsCanedit });
		}
		#endregion

		#region ChooseCertPhotoCommand
		private MvxCommand mChooseCertPhotoCommand;
		public MvxCommand ChooseCertPhotoCommand
		{
			get
			{
				if (mChooseCertPhotoCommand == null)
				{
					mChooseCertPhotoCommand = new MvxCommand(this.ChooseCertPhoto);
				}
				return mChooseCertPhotoCommand;
			}
		}
		private async void ChooseCertPhoto()
		{
			int option = 0;

			if (CertItem.Document == null)
			{
				option = await Mvx.Resolve<IMessageboxService>().ShowThreeOptions(CertNameDisplay,
															"Please choose an option",
															 "Cancel", "Take photo", "Select from library");
			}
			else
			{
				var arrayOption = new MessageboxOption[4];
				arrayOption[0] = new MessageboxOption() { Text = "Cancel", Type = MessageboxOptionType.Cancel };
				arrayOption[1] = new MessageboxOption() { Text = "Take photo", Type = MessageboxOptionType.Default };
				arrayOption[2] = new MessageboxOption() { Text = "Select from library", Type = MessageboxOptionType.Default };
				arrayOption[3] = new MessageboxOption() { Text = "Delete", Type = MessageboxOptionType.Destruction };

				option = await Mvx.Resolve<IMessageboxService>().ShowOptions(CertNameDisplay, "Please choose an option", MessageboxShowType.Center, arrayOption);
			}
			if (option > 0)
			{
				if (option == 1)
				{
					IsShowOrignalImage = false;

					var bitmapImage = await Mvx.Resolve<IImageService>().TakePicture();

					if (bitmapImage != null)
					{
						var imageName = Guid.NewGuid().ToString() + ".jpeg";
						LocalPathImage = await Mvx.Resolve<IImageService>().SaveImage(imageName, bitmapImage);
						//RaisePropertyChanged("LocalPathImage");
						if (CancelUploadImageTask != null)
						{
							Mvx.Resolve<ITaskManagementService>().CancelTask(CancelUploadImageTask);
							CancelUploadImageTask = null;
						}
						var cancelToken = new CancellationTokenSource();
						var taskUpload = System.Threading.Tasks.Task.Run(async () =>
						{
							var urlResponse = await Mvx.Resolve<IAzureService>().Upload(bitmapImage, imageName);
							ServerPathImage = urlResponse.Replace("/ShopItstore", string.Empty);
							Debug.WriteLine("url: " + ServerPathImage);

						}, cancelToken.Token);
						CancelUploadImageTask = Mvx.Resolve<ITaskManagementService>().AddTask(taskUpload, cancelToken);
					}
				}
				else if (option == 2)
				{
					IsShowOrignalImage = false;
					var bitmapImage = await Mvx.Resolve<IImageService>().SelectFromLibrary();
					if (bitmapImage != null)
					{
						var imageName = Guid.NewGuid().ToString() + ".jpeg";
						LocalPathImage = await Mvx.Resolve<IImageService>().SaveImage(imageName, bitmapImage);
						//RaisePropertyChanged("LocalPathImage");
						if (CancelUploadImageTask != null)
						{
							Mvx.Resolve<ITaskManagementService>().CancelTask(CancelUploadImageTask);
							CancelUploadImageTask = null;
						}

						var cancelToken = new CancellationTokenSource();
						var taskUpload = System.Threading.Tasks.Task.Run(async () =>
						{
							var urlResponse = await Mvx.Resolve<IAzureService>().Upload(bitmapImage, imageName);
							ServerPathImage = urlResponse.Replace("/ShopItstore", string.Empty);
							Debug.WriteLine("url: " + ServerPathImage);

						}, cancelToken.Token);
						CancelUploadImageTask = Mvx.Resolve<ITaskManagementService>().AddTask(taskUpload, cancelToken);
					}
				}
				else if (option == 3)
				{
					CertItem.Document = null;
					IsShowOrignalImage = true;
				}
			}
		}
		#endregion

		#region DeleteCompanyLicenceCertCommand         private MvxCommand mDeleteCompanyLicenceCertCommand;  		public MvxCommand DeleteCompanyLicenceCertCommand
		{ 			get
			{ 				if (mDeleteCompanyLicenceCertCommand == null)
				{ 					mDeleteCompanyLicenceCertCommand = new MvxCommand(this.DeleteCompanyLicenceCert); 				} 				return mDeleteCompanyLicenceCertCommand; 			} 		}  		private async void DeleteCompanyLicenceCert()
		{
			if (EditProfileVM != null)
			{
				var option = await Mvx.Resolve<IMessageboxService>().ShowTwoOptions("Remove Certificate", "Do you want to remove this certificate?", "Cancel", "Yes");

				if (option)
				{
					EditProfileVM.LicencesCert.Remove(this);
					this.CertItem.Document = null;
					this.CertItem.Descriptor = string.Empty;
					this.CertItem.Expiry = null;
					EditProfileVM.RaisePropertyChanged("LicencesCert");
				}
			} 		}
		#endregion

		#endregion

		#region Method

		public string ConvertDecimalToString(string value)
		{
			int count = 0;
			var valueStr = value.Replace(",", string.Empty).Replace("$", string.Empty);
			string temp = valueStr;
			for (int i = valueStr.Length; i > 0; i--)
			{
				count++;
				if (count == 3 && i != 1)
				{
					temp = temp.Insert(i - 1, ",");
					count = 0;
				}
			}
			return "$" + temp;
		}

		#endregion
	}
}
