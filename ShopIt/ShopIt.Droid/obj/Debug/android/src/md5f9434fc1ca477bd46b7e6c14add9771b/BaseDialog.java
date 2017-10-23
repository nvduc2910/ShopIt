package md5f9434fc1ca477bd46b7e6c14add9771b;


public class BaseDialog
	extends mvvmcross.droid.support.v4.MvxDialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ShopIt.Droid.Views.Dialogs.BaseDialog, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseDialog.class, __md_methods);
	}


	public BaseDialog ()
	{
		super ();
		if (getClass () == BaseDialog.class)
			mono.android.TypeManager.Activate ("ShopIt.Droid.Views.Dialogs.BaseDialog, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
