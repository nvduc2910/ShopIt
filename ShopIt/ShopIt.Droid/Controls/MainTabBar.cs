using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;

namespace ShopIt.Droid.Controls
{
    public class MainTabBar : FrameLayout, IMvxBindingContextOwner
    {
        #region Properties

        private LayoutInflater mInflater;
        private IMvxAndroidBindingContext mAndroidBindingContext;

        #endregion

        #region UIControls

        private ImageView mIvAvatar;
        private ImageView mIvAlbums;
        private ImageView mIvBell;

        #endregion

        #region Constructor

        public MainTabBar(Context context) : base(context)
        {
            Init();
        }

        public MainTabBar(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public MainTabBar(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        #endregion

        #region Methods

        void Init()
        {
            mAndroidBindingContext = new MvxAndroidBindingContext(this.Context, (IMvxLayoutInflaterHolder) this.Context);
            this.DelayBind(() =>
            {
                mAndroidBindingContext.BindingInflate(Resource.Layout.MainTabBar, this);
            });
            
        }

        #endregion

        #region Binding Properties

        [MvxSetToNullAfterBinding]
        public object DataContext
        {
            get { return mAndroidBindingContext.DataContext; }
            set
            {
                mAndroidBindingContext.DataContext = value;
            }
        }

        #endregion

        #region Implement

        #region Properties

        public IMvxBindingContext BindingContext
        {
            get { return mAndroidBindingContext; }
            set { throw new NotImplementedException("BindingContext is readonly"); }
        }

        #endregion

        #endregion


    }
}