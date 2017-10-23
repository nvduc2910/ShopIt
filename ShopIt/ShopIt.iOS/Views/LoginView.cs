using System;
using MvvmCross.Binding.BindingContext;
using ShopIt.Core;
using ShopIt.Core.ViewModels;
using UIKit;

namespace ShopIt.iOS.Views
{
	public partial class LoginView : BaseView, ILoginView
	{
		public LoginView() : base("LoginView", null)
		{
		}

		public new LoginViewModel ViewModel

		{
			get
			{
				return base.ViewModel as LoginViewModel;
			}
		}

		public override void ViewDidLoad()
		{
			
			base.ViewDidLoad();
			ViewModel.View = this;

			MarkeUIForView();

			var set = this.CreateBindingSet<LoginView, LoginViewModel>();

			set.Bind(tfEmail).To(vm => vm.Email);
			set.Bind(tfPassword).To(vm => vm.Password);

			set.Bind(tfEmailForgot).To(vm => vm.ForgotEmail);

			set.Bind(btnSignin).To(vm => vm.SignInCommand);
			set.Bind(btnForgotPassword).To(vm => vm.ForgotPasswordCommand);

			set.Bind(btnSendMyPassword).To(vm => vm.SendMyPasswordCommand);

			set.Bind(btnRegister).To(vm => vm.RegisterCommand);

			set.Bind(btnClosePopupForgotFW).To(vm => vm.CancelForgotPasswordCommand);

			set.Bind(btnToS).To(vm => vm.TermsOfServiceCommand);
			set.Bind(btnPP).To(vm => vm.PrivacyPolicyCommand);


			set.Apply();

			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}


		#region Implement ILoginView

		public void ShowPopUpForgotPassword()
		{
			SetAnimationShowView(vPopupForgotPW);
		}

		public void HidePopupForgotPassword()
		{
			setAnimationHiddenView(vPopupForgotPW);
		}

		public void HideKeyboard()
		{
			View.EndEditing(true);
		}

		#region Animation Show/Hidden Popup

		public void setAnimationHiddenView(UIView uiview)
		{
			uiview.Hidden = true;
			UIView.Animate(1, () =>
			{
				View.LayoutIfNeeded();
			});
		}

		public void SetAnimationShowView(UIView uiView)
		{
			uiView.Hidden = false;
			UIView.Animate(1, () =>
			{
				View.LayoutIfNeeded();
			});
		}
		#endregion

		#region Handle UI

		public void MarkeUIForView()
		{
			vPopupForgotPW.Layer.CornerRadius = 10;
			vPopupForgotPW.Layer.MasksToBounds = true;
		}

		#endregion

		#endregion
	}
}

