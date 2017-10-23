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
    public class ProjectsFragment : BaseFragment
    {
        #region UI Control
        private SwipeRefreshLayout refresher;


        #endregion

        #region Constructor

        #endregion

        #region Overrides

        public ProjectsViewModel ViewModel
        {
            get { return base.ViewModel as ProjectsViewModel; }
            set { base.ViewModel = value; }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.ProjectsView, container, false);
            Init(view);
            return view;
        }
        #endregion

        #region Implements

        #endregion

        #region Methods

        #region InitView

        void Init(View view)
        {
            refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            refresher.Refresh += delegate (object sender, EventArgs args)
            {
                refresher.Refreshing = false;
                ViewModel.LoadData();
            };
        }

        #endregion

        #endregion

    }
}