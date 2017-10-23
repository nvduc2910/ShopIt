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
    public class TourFragment : BaseFragment
    {
        private int mCurrentPage;

        #region Constructor

        public TourFragment(int currentPage,BaseViewModel viewModel)
        {
            this.mCurrentPage = currentPage;
            this.ViewModel = viewModel;
        }

        #endregion

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            this.EnsureBindingContextIsSet(savedInstanceState);
            View view;
            switch (this.mCurrentPage)
            {
                case 1:
                    {
                        view = this.BindingInflate(Resource.Layout.Tour2View, container, false);
                        break;
                    }

                case 2:
                    {
                        view = this.BindingInflate(Resource.Layout.Tour3View, container, false);
                        break;
                    }

                default:
                    {
                        view = this.BindingInflate(Resource.Layout.Tour1View, container, false);
                        break;
                    }
            }
            return view;
        }

        #endregion

        #region Methods

        #endregion
    }
}