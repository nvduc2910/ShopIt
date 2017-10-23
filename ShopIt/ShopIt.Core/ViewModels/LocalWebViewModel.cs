using System;
using ShopIt.Core.Services;

namespace ShopIt.Core.ViewModels
{
	public class LocalWebViewModel : BaseViewModel
	{
		#region Constructors
		public LocalWebViewModel(IApiService apiService, ICacheService cacheService, IMessageboxService messageboxService, IProgressDialogService progressDialogService, IPlatformService platformService)
			: base(apiService, cacheService, messageboxService, progressDialogService, platformService)
		{
		}

		#endregion

		#region Properties

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
				RaisePropertyChanged();
			}
		}
		#endregion

		#region HTMLFile
		private string mHTMLFile;

		public string HTMLFile
		{
			get
			{
				return mHTMLFile;
			}
			set
			{
				mHTMLFile = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init(string title, string fileName)
		{
			this.Title = title;
			this.HTMLFile = fileName;
		}
		#endregion

		#region Commands

		#endregion

		#region Methods

		#endregion
	}
}
