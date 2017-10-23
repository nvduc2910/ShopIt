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
    public class CustomImageView : ImageView
    {
        public CustomImageView(Context context) : base(context)
        {
        }

        public CustomImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public CustomImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        #region Binding Properties

        public bool IsChecked
        {
            set
            {
                if (value)
                {
                    this.SetImageResource(Resource.Drawable.ic_project_error);
                }
                else
                {
                    this.SetImageResource(Resource.Drawable.ic_check_filled);
                }
            }
        }

        #endregion
    }
}