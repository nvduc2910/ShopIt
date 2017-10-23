using System;
using MvvmCross.Core.ViewModels;
using ShopIt.Core.Models;
using ViewModels;

namespace ShopIt.Core.ViewModels.Items
{
	public class DocumentItemViewModel : MvxViewModel
	{
		#region Constructors
		public DocumentItemViewModel(Document document)
		{
			this.Document = document;
		}

		#endregion

		#region Properties

		#region Document
		private Document mDocument;

		public Document Document
		{
			get
			{
				return mDocument;
			}
			set
			{
				mDocument = value;
				RaisePropertyChanged();
			}
		}
		#endregion

		#endregion

		#region Init
		public void Init()
		{
			
		}
		#endregion

		#region Commands

		#endregion

		#region Methods

		#endregion
	}
}
