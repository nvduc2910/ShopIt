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
    public class ProfileStep : ImageView
    {
        public ProfileStep(Context context) : base(context)
        {
        }

        public ProfileStep(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public ProfileStep(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        #region Binding Properties

        public bool IsSelected
        {
            set
            {
                if (value)
                {
                    this.SetImageResource(Resource.Drawable.ic_progress_bar_filled);
                }
                else
                {
                    this.SetImageResource(Resource.Drawable.ic_progress_bar_not_filled);
                }
            }
        }

        #endregion
    }
}