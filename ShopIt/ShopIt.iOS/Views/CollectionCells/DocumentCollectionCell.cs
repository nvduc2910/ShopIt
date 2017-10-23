using System;

using Foundation;
using MvvmCross.Binding.iOS.Views;
using UIKit;
using ShopIt.Core.ViewModels.Items;

namespace ShopIt.iOS.Views.CollectionCells
{
	public partial class DocumentCollectionCell : MvxCollectionViewCell
	{
		public static readonly NSString Key = new NSString("DocumentCollectionCell");
		public static readonly UINib Nib;

		#region ViewModel
		public DocumentItemViewModel ViewModel
		{
			get;
			set;
		}
		#endregion

		static DocumentCollectionCell()
		{
			Nib = UINib.FromName("DocumentCollectionCell", NSBundle.MainBundle);
		}

		protected DocumentCollectionCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public static DocumentCollectionCell Create()
		{
			return (DocumentCollectionCell)Nib.Instantiate(null, null)[0];
		}
	}
}
