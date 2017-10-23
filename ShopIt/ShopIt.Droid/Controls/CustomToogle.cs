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

namespace ShopIt.Droid.Controls
{
    public class CustomToogle : RelativeLayout
    {
        #region Constructors

        public CustomToogle(Context context) : base(context)
        {
        }

        public CustomToogle(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public CustomToogle(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        #endregion

        #region Binding Properties

        #region IsChecked

        public bool IsChecked
        {
            set
            {
                if (value)
                {
                    this.SetBackgroundResource(Resource.Drawable.bg_toogle_filled);
                }
                else
                {
                    this.SetBackgroundResource(Resource.Drawable.bg_toogle_outline);
                }
            }
        }

        #endregion

        #endregion

    }
}