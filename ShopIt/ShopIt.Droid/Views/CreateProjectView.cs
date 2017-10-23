using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Webkit;
using Android.Widget;
using ShopIt.Core.ViewModels;
using ViewModels;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait, WindowSoftInputMode = SoftInput.AdjustResize)]
    public class CreateProjectView : BaseView, ICreateProjectView
    {
        #region UI Controls

        private EditText etEmailInvite;

        #endregion

        #region Overrides

        public CreateProjectViewModel ViewModel
        {
            get { return base.ViewModel as CreateProjectViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.CreateProjectView);
            Init();
            ViewModel.View = this;
        }

        #endregion

        #region Implements

        #region ICreateProjectView

        public void HidenKeyboard()
        {
        }

        #endregion
        #endregion

        #region Methods

        void Init()
        {
            etEmailInvite = this.FindViewById<EditText>(Resource.Id.etEmailInvite);
            etEmailInvite.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done)
                {
                    ViewModel.InviteCommand.Execute();
                }
            };
            etEmailInvite.FocusChange += (sender, args) =>
            {
                if (!args.HasFocus)
                {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromInputMethod(etEmailInvite.WindowToken, 0);

                    Window.SetSoftInputMode(SoftInput.StateHidden);

                    ViewModel.InviteCommand.Execute();
                }
            };
        }

        #endregion

        
    }
}