using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.Models;
using ShopIt.Core.ViewModels.Items;
using UIKit;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class ProjectJoinedTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("ProjectJoinedTableViewCell");
		public static readonly UINib Nib;
		public ProjectItemViewModel Model { get; set; }


		static ProjectJoinedTableViewCell()
		{
			Nib = UINib.FromName("ProjectJoinedTableViewCell", NSBundle.MainBundle);
		}

		protected ProjectJoinedTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<ProjectJoinedTableViewCell, ProjectItemViewModel>();
				set.Bind(lbJoinedOn).To(vm => vm.CreatedJoinedText);
				set.Bind(lbInvitedBy).To(vm => vm.ProjectHeading.OwnerName);
				set.Bind(lbProjectName).To(vm => vm.ProjectHeading.Title);
				set.Bind(lbStage).To(vm => vm.ProjectHeading.Stage);
				set.Bind(ivStatus).For(v => v.Highlighted).To(vm => vm.ProjectHeading.Alert);
				set.Apply();

				InitView();
			});
		}

		private void InitView()
		{
			vBound.Layer.CornerRadius = 10f;
			vBound.Layer.MasksToBounds = true;
		}

		public static ProjectJoinedTableViewCell Create()
		{
			return (ProjectJoinedTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
