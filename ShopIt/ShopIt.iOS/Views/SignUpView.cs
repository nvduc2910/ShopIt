using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core;
using ShopIt.Core.ViewModels;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class SignUpView : BaseView, ISignUpView
	{
		public SignUpView() : base("SignUpView", null)
		{
		}

		public new SignUpViewModel ViewModel

		{
			get
			{
				return base.ViewModel as SignUpViewModel;
			}
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			ViewModel.View = this;

			var set = this.CreateBindingSet<SignUpView, SignUpViewModel>();
			set.Bind(tfEmail).To(vm => vm.Email);
			set.Bind(tfPassword).To(vm => vm.Password);
			set.Bind(tfConfirmPassword).To(vm => vm.ConfirmPassword);

			set.Bind(btnSignup).To(vm => vm.SignUpCommand);

			set.Bind(btnClose).To(vm => vm.GoBackCommand);

			set.Bind(btnToS).To(vm => vm.TermsOfServiceCommand);
			set.Bind(btnPP).To(vm => vm.PrivacyPolicyCommand);


			set.Apply();

		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		#region Implement ISignUpView
		public void HideKeyboard()
		{
			View.EndEditing(true);
		}
		#endregion
	}
}

