using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using ShopIt.Droid.Helper;

namespace ShopIt.Droid.Controls
{
    public class CustomTextView : TextView
    {
        public CustomTextView(Context context) : base(context)
        {
            Initialize(context);
        }

        public CustomTextView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        public CustomTextView(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Initialize(context, attrs);
        }

        #region Initialize
        private void Initialize(Context context)
        {
            var type = TypefaceHelper.GeTypeface(context);
            SetTypeface(type, TypefaceStyle.Normal);
        }

        private void Initialize(Context context, IAttributeSet attrs)
        {
            var array = context.ObtainStyledAttributes(attrs, Resource.Styleable.CustomTextView);
            var textStyle = array.GetString(Resource.Styleable.CustomTextView_textStyle);
            var type = TypefaceHelper.GeTypeface(context, textStyle);
            SetTypeface(type, TypefaceStyle.Normal);
        }
        #endregion
    }
}