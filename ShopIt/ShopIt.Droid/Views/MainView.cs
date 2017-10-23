using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using ShopIt.Core.ViewModels;
using ShopIt.Droid.Controls;
using ShopIt.Droid.Helper;
using ShopIt.Droid.Views.Fragments;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait,WindowSoftInputMode = SoftInput.AdjustPan|SoftInput.StateAlwaysHidden)]
    public class MainView : BaseView, IMainView, IHomeView
    {
        #region Properties

        HomeTab mCurrentTab = HomeTab.Profile;


        #endregion

        #region UI Controls

        private FrameLayout mProfileContainer, mProjectsContainer, mAlertsContainer;
        private LinearLayout mMenu,mDimLayout;

        #endregion

        #region Overrides

        public MainViewModel ViewModel
        {
            get { return base.ViewModel as MainViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.MainView);
            ViewModel.View = this;
            ViewModel.HomeVM.View = this;
            InitView();
        }

        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            if (ev.Action == MotionEventActions.Down)
            {
                View view = this.CurrentFocus;
                if (view != null && view is CustomEditText)
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

            return base.DispatchTouchEvent(ev);
        }

        #endregion

        #region Implements

        #region IMainView

        public void ShowMenu()
        {
            mDimLayout.Visibility = ViewStates.Visible;
            AnimationUtil.InFromTop(mMenu,duration:300);
        }

        public void HideMenu()
        {
            AnimationUtil.OutToTop(mMenu, duration: 300,animationEnded: () =>
            {
                mDimLayout.Visibility = ViewStates.Gone;
            });
        }

        #endregion

        #region IHomeView

        public void SwitchToTab(HomeTab tab)
        {
            if (mCurrentTab != tab)
            {
                switch (tab)
                {
                    case HomeTab.Projects:
                        {
                            GoToProjectsTab();
                            break;
                        }
                    case HomeTab.Alerts:
                        {
                            GoToAlertsTab();
                            break;
                        }
                    default:
                        {
                            GoToProfileTab();
                            break;
                        }
                }
                mCurrentTab = tab;
            }

        }

        #endregion

        #endregion

        #region Methods

        void InitView()
        {
            mProfileContainer = this.FindViewById<FrameLayout>(Resource.Id.fl_container_profile);
            mProjectsContainer = this.FindViewById<FrameLayout>(Resource.Id.fl_container_projects);
            mAlertsContainer = this.FindViewById<FrameLayout>(Resource.Id.fl_container_alerts);
            mMenu = this.FindViewById<LinearLayout>(Resource.Id.ll_menu);
            mDimLayout = this.FindViewById<LinearLayout>(Resource.Id.ll_bg_menu);

            ReplaceFragment(new ProfileFragment(), ViewModel.HomeVM.ProfileVM, Resource.Id.fl_container_profile);
            ReplaceFragment(new ProjectsFragment(), ViewModel.HomeVM.ProjectsVM, Resource.Id.fl_container_projects);
            ReplaceFragment(new AlertsFragment(), ViewModel.HomeVM.AlertsVM, Resource.Id.fl_container_alerts);

            GoToProfileTab();

        }

        #region GoToProfileTab

        void GoToProfileTab()
        {
            mProfileContainer.Visibility = ViewStates.Visible;
            mProjectsContainer.Visibility = ViewStates.Gone;
            mAlertsContainer.Visibility = ViewStates.Gone;
            if (mCurrentTab == HomeTab.Projects)
            {
                AnimationUtil.OutToRight(mProjectsContainer);
            }
            else if (mCurrentTab == HomeTab.Alerts)
            {
                AnimationUtil.OutToRight(mAlertsContainer);
            }
            AnimationUtil.InFromLeft(mProfileContainer);
        }
        #endregion

        #region GoToProjectsTab

        void GoToProjectsTab()
        {
            mProfileContainer.Visibility = ViewStates.Gone;
            mProjectsContainer.Visibility = ViewStates.Visible;
            mAlertsContainer.Visibility = ViewStates.Gone;
            if (mCurrentTab == HomeTab.Profile)
            {
                AnimationUtil.OutToLeft(mProfileContainer);
                AnimationUtil.InFromRight(mProjectsContainer);
            }
            else if(mCurrentTab == HomeTab.Alerts)
            {
                AnimationUtil.OutToRight(mAlertsContainer);
                AnimationUtil.InFromLeft(mProjectsContainer);
            }
        }

        #endregion

        #region GoToAlertsTab

        void GoToAlertsTab()
        {
            mProfileContainer.Visibility = ViewStates.Gone;
            mProjectsContainer.Visibility = ViewStates.Gone;
            mAlertsContainer.Visibility = ViewStates.Visible;
            if (mCurrentTab == HomeTab.Profile)
            {
                AnimationUtil.OutToLeft(mProfileContainer);
            }
            else if (mCurrentTab == HomeTab.Projects)
            {
                AnimationUtil.OutToLeft(mProjectsContainer);
            }

            AnimationUtil.InFromRight(mAlertsContainer);
        }

        #endregion

        #endregion

    }
}