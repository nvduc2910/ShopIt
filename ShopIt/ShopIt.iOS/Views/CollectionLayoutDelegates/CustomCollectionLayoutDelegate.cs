using System;
using CoreGraphics;
using ShopIt.iOS.Views.CollectionCells;
using UIKit;

namespace ShopIt.iOS.Views.CollectionLayoutDelegates
{
	public class CustomCollectionLayoutDelegate : UICollectionViewDelegateFlowLayout
	{
		private CGSize mSize;
		private DocumentsView mDocumentView;

		public CustomCollectionLayoutDelegate(CGSize size, DocumentsView documentView)
		{
			mSize = size;
			mDocumentView = documentView;
		}

		public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
		{
			return mSize;
		}

		public override void ItemSelected(UICollectionView collectionView, Foundation.NSIndexPath indexPath)
		{
			if (mDocumentView != null)
			{
				var cell = collectionView.CellForItem(indexPath) as DocumentCollectionCell;

				mDocumentView.ViewModel.SelectDocumenItemCommand.Execute(cell.ViewModel);
			}
		}
	}
}
