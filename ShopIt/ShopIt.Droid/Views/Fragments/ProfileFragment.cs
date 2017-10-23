using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using ShopIt.Core.ViewModels;

namespace ShopIt.Droid.Views.Fragments
{
    public class ProfileFragment : BaseFragment
    {
        #region Constructor

        #endregion

        #region Overrides

        public ProfileViewModel ViewModel
        {
            get { return base.ViewModel as ProfileViewModel; }
            set { base.ViewModel = value; }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            View view = this.BindingInflate(Resource.Layout.ProfileView, container, false);
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

        }

        #endregion

        #endregion

    }
}