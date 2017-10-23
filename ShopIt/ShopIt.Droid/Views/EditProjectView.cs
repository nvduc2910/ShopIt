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
using ShopIt.Droid.Controls;
using ViewModels;
using ViewModels.Items;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.SensorPortrait,WindowSoftInputMode = SoftInput.AdjustResize)]
    public class EditProjectView : BaseView, IEditProjectView
    {
        #region UI Controls

        private CustomEditText etEmailInvite;

        #endregion

        #region Overrides

        public EditProjectViewModel ViewModel
        {
            get { return base.ViewModel as EditProjectViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.EditProjectView);
            ViewModel.View = this;
            Init();
        }

        #endregion

        #region Implements

        #region IEditProjectView

        public void HideKeyboard()
        {
            HideKeyboard("EditProjectView");
        }

        #endregion

        #endregion

        #region Methods
        void Init()
        {
            etEmailInvite = this.FindViewById<CustomEditText>(Resource.Id.etEmailInvite);
            etEmailInvite.EditorAction += (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done)
                {
                    ViewModel.SendInviteCommand.Execute();
                }
            };
            etEmailInvite.FocusChange += (sender, args) =>
            {
                if (!args.HasFocus)
                {
                    InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    imm.HideSoftInputFromInputMethod(etEmailInvite.WindowToken, 0);

                    Window.SetSoftInputMode(SoftInput.StateHidden);

                    ViewModel.SendInviteCommand.Execute();
                }
            };
        }
        #endregion


    }
}