package md58b7ea26f883564da320e8436bebc2824;


public class RecurringAdapter
	extends md5bf0126c95bf9fc0db24c02c9adb4cfa7.MvxAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"";
		mono.android.Runtime.register ("ShopIt.Droid.Controls.RecurringAdapter, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RecurringAdapter.class, __md_methods);
	}


	public RecurringAdapter ()
	{
		super ();
		if (getClass () == RecurringAdapter.class)
			mono.android.TypeManager.Activate ("ShopIt.Droid.Controls.RecurringAdapter, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public RecurringAdapter (android.content.Context p0)
	{
		super ();
		if (getClass () == RecurringAdapter.class)
			mono.android.TypeManager.Activate ("ShopIt.Droid.Controls.RecurringAdapter, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
