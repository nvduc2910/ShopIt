package md510b4f97ca76eaccd593e85752463ac2c;


public class LoginView
	extends md510b4f97ca76eaccd593e85752463ac2c.BaseView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("ShopIt.Droid.Views.LoginView, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginView.class, __md_methods);
	}


	public LoginView ()
	{
		super ();
		if (getClass () == LoginView.class)
			mono.android.TypeManager.Activate ("ShopIt.Droid.Views.LoginView, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
