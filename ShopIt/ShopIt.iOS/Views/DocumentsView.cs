using System;
using ShopIt.Core.ViewModels;
using UIKit;
using MvvmCross.Binding.BindingContext;
using ShopIt.iOS.Views.CollectionSources;
using ShopIt.iOS.Views.CollectionLayoutDelegates;
using CoreGraphics;

namespace ShopIt.iOS.Views
{
	public partial class DocumentsView : BaseView
	{
		public DocumentsView() : base("DocumentsView", null)
		{
		}

		#region Private

		#endregion

		#region ViewModel

		public new DocumentsViewModel ViewModel
		{
			get
			{
				return base.ViewModel as DocumentsViewModel;
			}
			set
			{
				base.ViewModel = value;
			}
		}

		#endregion

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var set = this.CreateBindingSet<DocumentsView, DocumentsViewModel>();

			var documentSource = new DocumentsCollectionSource(colDocuments, this);
			set.Bind(documentSource).For(s => s.ItemsSource).To(vm => vm.DocumentItems);

			set.Bind(btnBack).To(vm => vm.GoBackCommand);
			set.Bind(btnAdd).To(vm => vm.AddDocumentCommand);

			set.Apply();

			nfloat size = (UIScreen.MainScreen.Bounds.Width - 20f) / 2f;
			CustomCollectionLayoutDelegate customLayoutDelegate = new CustomCollectionLayoutDelegate(new CGSize(size, size), this);

			colDocuments.Source = documentSource;
			colDocuments.Delegate = customLayoutDelegate;
			colDocuments.ReloadData();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

