using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;

namespace ShopIt.Droid.Controls
{
    public class MvxClickableLinearLayout : MvxLinearLayout
    {
        public MvxClickableLinearLayout(Context context, IAttributeSet attrs)
            : this(context, attrs, new MvxClickableLinearLayoutAdapter(context))
        {
        }

        public MvxClickableLinearLayout(Context context, IAttributeSet attrs, MvxClickableLinearLayoutAdapter adapter)
            : base(context, attrs, adapter)
        {
            var mvxClickableLinearLayoutAdapter = Adapter as MvxClickableLinearLayoutAdapter;
            if (mvxClickableLinearLayoutAdapter != null)
            {
                mvxClickableLinearLayoutAdapter.OnItemClick = OnItemClick;
            }
        }

        public ICommand ItemClick { get; set; }

        public void OnItemClick(object item)
        {
            if (ItemClick != null && ItemClick.CanExecute(item))
            {
                ItemClick.Execute(item);
            }
        }
    }
}