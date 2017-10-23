using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Mono.Security;
using ShopIt.Droid.Helper;
using String = System.String;

namespace ShopIt.Droid.Controls
{
    public class CustomDollarEditText : EditText
    {
        #region Constructors

        public CustomDollarEditText(Context context)
            : base(context)
        {
            Init(context);
        }

        public CustomDollarEditText(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Init(context, attrs);
        }

        public CustomDollarEditText(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs);
        }

        #endregion

        #region Overrides

        public override void SetText(ICharSequence text, BufferType type)
        {
            base.SetText(text, BufferType.Spannable);
        }

        #endregion

        #region Methods

        #region Init

        private void Init(Context context)
        {
            var type = TypefaceHelper.GeTypeface(context);
            SetTypeface(type, TypefaceStyle.Normal);
            this.AfterTextChanged += (sender, args) =>
            {
                this.SetSelection(this.Text.Length);
            };
        }

        private void Init(Context context, IAttributeSet attrs)
        {
            ; var array = context.ObtainStyledAttributes(attrs, Resource.Styleable.CustomTextView);
            var textStyle = array.GetString(Resource.Styleable.CustomTextView_textStyle);
            var type = TypefaceHelper.GeTypeface(context, textStyle);
            SetTypeface(type, TypefaceStyle.Normal);
            var background = attrs.GetAttributeValue("http://schemas.android.com/apk/res/android", "background");
            if (String.IsNullOrEmpty(background))
            {
                SetBackgroundResource(Android.Resource.Color.Transparent);
            }
          this.AfterTextChanged += (sender, args) =>
           {
               this.SetSelection(this.Text.Length);
           };
        }

        #endregion

        #endregion
    }
}