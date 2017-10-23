using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using ShopIt.Core.ViewModels;
using ShopIt.Droid.Services;
using ShopIt.Droid.Views.Fragments;

namespace ShopIt.Droid.Views
{
    public class BaseView : MvxFragmentActivity
    {
        #region Overrides

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Mvx.Resolve<IDroidService>().CurrentContext = this;
        }

        protected override void OnResume()
        {
            base.OnResume();
            Mvx.Resolve<IDroidService>().CurrentContext = this;
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            Mvx.Resolve<IDroidService>().CurrentContext = this;
        }

        #region Handling When Keyboard Appears

        bool isScrolling = false;

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Down)
            {
                isScrolling = false;
            }

            if (ev.Action == MotionEventActions.Move)
            {
                isScrolling = true;
            }
            if (ev.Action == MotionEventActions.Up)
            {
                if (!isScrolling)
                {
                    View view = this.CurrentFocus;
                    if (view != null && view is EditText)
                    {
                        Rect outRect = new Rect();
                        view.GetGlobalVisibleRect(outRect);
                        if (!outRect.Contains((int)ev.RawX, (int)ev.RawY))
                        {
                            view.ClearFocus();
                            HideKeyboard("");
                        }
                    }
                }
                else
                {
                    isScrolling = false;
                }

            }

            return base.DispatchTouchEvent(ev);
        }

        #endregion

        #endregion

        #region Methods

        #region ReplaceFragment

        protected void ReplaceFragment(BaseFragment fragment, BaseViewModel viewModel, int containerViewId)
        {
            fragment.ViewModel = viewModel;
            SupportFragmentManager
                .BeginTransaction()
                .Replace(containerViewId, fragment)
                .Commit();
        }

        #endregion

        #region HideKeyboard

        protected void HideKeyboard(string view)
        {
            try
            {
                InputMethodManager inputMethodManager = (InputMethodManager)this
                    .GetSystemService(Activity.InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(Window.CurrentFocus.WindowToken, 0);
            }
            catch (Exception e)
            {
                Log.Debug(view, e.Message);
            }
        }

        #endregion

        #region ShowMessage

        protected void ShowMessage(string msg)
        {
            Toast.MakeText(this, msg, ToastLength.Short).Show();
        }

        #endregion


        #endregion
    }
}