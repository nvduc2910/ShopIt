using System;
using MvvmCross.Binding.BindingContext;
using Foundation;
using ShopIt.Core.Models;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using ShopIt.Core.ViewModels.Items;

namespace ShopIt.iOS.Views.TableCells
{
	public partial class ProjectCreateTableViewCell : MvxTableViewCell
	{
		public static readonly NSString Key = new NSString("ProjectCreateTableViewCell");
		public static readonly UINib Nib;

		public ProjectItemViewModel Model { get; set; }

		public UILabel LbStatus
		{
			get
			{
				return lbStatus;
			}
		}

		public UIView VBound
		{
			get
			{
				return vBound;
			}
		}

		static ProjectCreateTableViewCell()
		{
			Nib = UINib.FromName("ProjectCreateTableViewCell", NSBundle.MainBundle);
		}

		protected ProjectCreateTableViewCell(IntPtr handle) : base(handle)
		{
			this.DelayBind(() =>
			{
				var set = this.CreateBindingSet<ProjectCreateTableViewCell, ProjectItemViewModel>();
				set.Bind(lbCreatedOn).To(vm => vm.CreatedJoinedText);
				set.Bind(lbStatus).To(vm => vm.ProjectHeading.Stage);
				set.Bind(lbProjectName).To(vm => vm.ProjectHeading.Title);
				set.Bind(ivStatusProject).For(v => v.Highlighted).To(vm => vm.ProjectHeading.Alert);
				set.Apply();

				InitView();
			});
		}

		private void InitView()
		{
			vBound.Layer.CornerRadius = 10f;
			vBound.Layer.MasksToBounds = true;
		}

		public static ProjectCreateTableViewCell Create()
		{
			return (ProjectCreateTableViewCell)Nib.Instantiate(null, null)[0];
		}
	}
}
