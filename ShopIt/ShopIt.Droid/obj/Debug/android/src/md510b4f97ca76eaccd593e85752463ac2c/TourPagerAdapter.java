package md510b4f97ca76eaccd593e85752463ac2c;


public class TourPagerAdapter
	extends mvvmcross.droid.support.v4.MvxFragmentPagerAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getItem:(I)Landroid/support/v4/app/Fragment;:GetGetItem_IHandler\n" +
			"n_getCount:()I:GetGetCountHandler\n" +
			"";
		mono.android.Runtime.register ("ShopIt.Droid.Views.TourPagerAdapter, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", TourPagerAdapter.class, __md_methods);
	}


	public TourPagerAdapter (android.support.v4.app.FragmentManager p0)
	{
		super (p0);
		if (getClass () == TourPagerAdapter.class)
			mono.android.TypeManager.Activate ("ShopIt.Droid.Views.TourPagerAdapter, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Support.V4.App.FragmentManager, Xamarin.Android.Support.Fragment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public android.support.v4.app.Fragment getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native android.support.v4.app.Fragment n_getItem (int p0);


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();

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
