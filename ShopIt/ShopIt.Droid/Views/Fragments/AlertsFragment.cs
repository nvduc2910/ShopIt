using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using ShopIt.Core.ViewModels;

namespace ShopIt.Droid.Views.Fragments
{
    public class AlertsFragment : BaseFragment, IAlertsView
    {
        #region UI Controls

        private SwipeRefreshLayout refresher;

        #endregion

        #region Constructor

        #endregion

        #region Overrides

        public AlertsViewModel ViewModel
        {
            get { return base.ViewModel as AlertsViewModel; }
            set { base.ViewModel = value; }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.AlertsView, container, false);
            Init(view);
            ViewModel.View = this;
            return view;
        }
        #endregion

        #region Implements


        #region IAlertsView

        public void CloseOptionsOnAlert(int idx)
        {
            ShowToast(this.Activity,"Not implement yet");
        }

        #endregion

        #endregion

        #region Methods

        #region InitView

        void Init(View view)
        {
            refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            refresher.Refresh+=  delegate(object sender, EventArgs args)
            {
                refresher.Refreshing = false;
                ViewModel.LoadData();
            };
        }

        #endregion

        #endregion

       
    }
}