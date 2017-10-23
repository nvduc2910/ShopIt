using System;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.ViewModels.Items;
using ShopIt.iOS.Views.CollectionCells;
using UIKit;

namespace ShopIt.iOS.Views.CollectionSources
{
	public class DocumentsCollectionSource : MvxCollectionViewSource
	{
		public DocumentsView mDocumentsView;

		public DocumentsCollectionSource(UICollectionView collectionView, DocumentsView view) : base(collectionView)
		{
			collectionView.RegisterNibForCell(UINib.FromName("DocumentCollectionCell", NSBundle.MainBundle), DocumentCollectionCell.Key);
			this.mDocumentsView = view;
		}

		protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView, Foundation.NSIndexPath indexPath, object item)
		{
			var cell = collectionView.DequeueReusableCell(DocumentCollectionCell.Key, indexPath) as DocumentCollectionCell;
			if (cell == null)
				cell = DocumentCollectionCell.Create();

			cell.ViewModel = item as DocumentItemViewModel;

			return cell;
		}
	}
}
