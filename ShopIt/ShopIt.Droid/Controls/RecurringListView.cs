using System;
using System.Collections;
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
using MvvmCross.Binding.Droid.ResourceHelpers;
using MvvmCross.Binding.Droid.Views;

namespace ShopIt.Droid.Controls
{
    public class RecurringListView : ListView, ICustomItemClick
    {
        public RecurringListView(Context context, IAttributeSet attrs)
            : this(context, attrs, new RecurringAdapter(context))
        {
        }


        public RecurringListView(Context context, IAttributeSet attrs, RecurringAdapter adapter)
            : base(context, attrs)
        {
            var itemTemplateId = MvxAttributeHelpers.ReadAttributeValue(context, attrs,
                MvxAndroidBindingResource.Instance.ListViewStylableGroupId,
                MvxAndroidBindingResource.Instance.ListItemTemplateId);

            adapter.ItemTemplateId = itemTemplateId;
            Adapter = adapter;
            Adapter.CustomItemClick = this;
            SetupItemClickListeners();
        }

        public new RecurringAdapter Adapter
        {
            get
            {
                return base.Adapter as RecurringAdapter;
            }
            set
            {
                var existing = Adapter;
                if (existing == value)
                    return;

                if (value != null && existing != null)
                {
                    value.ItemsSource = existing.ItemsSource;
                }

                base.Adapter = value;
            }
        }

        public IEnumerable ItemsSource
        {
            get { return Adapter.ItemsSource; }
            set { Adapter.ItemsSource = value; }
        }

        public int ItemTemplateId
        {
            get { return Adapter.ItemTemplateId; }
            set { Adapter.ItemTemplateId = value; }
        }

        public new ICommand ItemClick { get; set; }

        public new ICommand ItemLongClick { get; set; }

        protected void SetupItemClickListeners()
        {
            base.ItemLongClick += (sender, args) => ExecuteCommandOnItem(this.ItemLongClick, args.Position);
        }

        protected virtual void ExecuteCommandOnItem(ICommand command, int position)
        {
            if (command == null)
                return;

            var item = Adapter.GetRawItem(position);
            if (item == null)
                return;

            if (!command.CanExecute(item))
                return;

            command.Execute(item);
        }

        void ICustomItemClick.ItemClick(int pos)
        {
            ExecuteCommandOnItem(this.ItemClick, pos);
        }
    }
}