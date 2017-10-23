using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using ShopIt.Core.ViewModels;
using ShopIt.Droid.Views.Fragments;
using Fragment = Android.Support.V4.App.Fragment;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace ShopIt.Droid.Views
{
    [Activity(Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.SensorPortrait, NoHistory = true)]
    public class TourView : BaseView, ViewPager.IOnPageChangeListener
    {
        #region UI Controls

        private ViewPager mViewPager;
        #endregion

        #region Overrides

        public TourViewModel ViewModel
        {
            get { return base.ViewModel as TourViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.TourView);
            InitView();
        }

        #endregion

        #region Implements

        #region IOnPageChangeListener

        public void OnPageScrollStateChanged(int state)
        {
        }

        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageSelected(int position)
        {
            ViewModel.CurrentPage = position;
        }

        #endregion

        #endregion

        #region Methods

        #region InitView

        void InitView()
        {
            mViewPager = (ViewPager) this.FindViewById(Resource.Id.pager);
            TourPagerAdapter tourPagerAdapter = new TourPagerAdapter(this, SupportFragmentManager, null, ViewModel);
            mViewPager.AddOnPageChangeListener(this);
            mViewPager.Adapter = tourPagerAdapter;
            mViewPager.SetPageTransformer(true, new FadeTransformer());
        }

        #endregion

        #endregion
       
    }

    #region TourPagerAdapter

    public class TourPagerAdapter : MvxFragmentPagerAdapter
    {
        private List<TourFragment> mTourFraments;
        public TourPagerAdapter(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public TourPagerAdapter(Context context, FragmentManager fragmentManager, IEnumerable<FragmentInfo> fragments, BaseViewModel viewModel) : base(context, fragmentManager, fragments)
        {
            mTourFraments = new List<TourFragment>();
            mTourFraments.Add(new TourFragment(0, viewModel));
            mTourFraments.Add(new TourFragment(1, viewModel));
            mTourFraments.Add(new TourFragment(2, viewModel));
        }

        public override Fragment GetItem(int position)
        {
            return mTourFraments[position];
        }

        public override int Count
        {
            get { return mTourFraments.Count; }
        }
    }

    #endregion

    #region FadeTransformer

    public class FadeTransformer : Java.Lang.Object, ViewPager.IPageTransformer
    {
        private const float MaxAngle = 30F;
        public void TransformPage(View view, float position)
        {
            if (position < -1 || position > 1)
            {
                view.Alpha = 0; // The view is offscreen.
            }
            else
            {
                view.Alpha = 1;

                view.PivotY = view.Height / 2; // The Y Pivot is halfway down the view.

                // The X pivots need to be on adjacent sides.
                if (position < 0)
                {
                    view.PivotX = view.Width;
                }
                else
                {
                    view.PivotX = 0;
                }

                view.RotationY = MaxAngle * position; // Rotate the view.
            }
        }
    }

    #endregion



}