using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core.ViewModels;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class ProfileView : BaseView
	{
		#region Private 

		private HomeView mHomeView;

		#endregion

		public ProfileView() : base("ProfileView", null)
		{
		}

		#region ViewModel

		public new ProfileViewModel ViewModel
		{
			get
			{
				return base.ViewModel as ProfileViewModel;
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
			InitView();

			var set = this.CreateBindingSet<ProfileView, ProfileViewModel>();

			set.Bind(vCreateProfile).For(s => s.Hidden).To(vm => vm.IsShowViewProfileView);
			set.Bind(btnCreateProfile).To(vm => vm.CreateProfileCommand);
			set.Bind(btnViewProfile).To(vm => vm.ViewProfileCommand);
			set.Bind(btnMenu).To(vm => vm.ShowMenuCommand);

			set.Bind(btnShareProfile).To(vm => vm.ShareProfileCommand);
			//set.Bind(btnShareProfile1).To(vm => vm.ShareProfileCommand);
			set.Bind(btnSendProfile).To(vm => vm.ShareProfileCommand);
			//set.Bind(btnSendProfile1).To(vm => vm.ShareProfileCommand);

			//set.Bind(btnSendProfile1).For(s => s.Highlighted).To(vm => vm.IsShowViewProfileView).WithConversion("TrueFalse");
			//set.Bind(btnShareProfile1).For(s => s.Enabled).To(vm => vm.IsShowViewProfileView).WithConversion("TrueFalse");

			set.Bind(ivStatusAlert).For(s => s.Highlighted).To(s => s.IsShowAlert);
			set.Apply();
			// Perform any additional setup after loading the view, typically from a nib.

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}

		#region Init View

		public void InitView()
		{
			vCreateProfile.Layer.CornerRadius = 5f;
			vCreateProfile.Layer.MasksToBounds = true;
			vViewProfile.Layer.CornerRadius = 5f;
			vViewProfile.Layer.MasksToBounds = true;
		}

		#endregion

		public void ShowMenu()
		{
			throw new NotImplementedException();
		}

		public void HideMenu()
		{
			throw new NotImplementedException();
		}
	}
}

