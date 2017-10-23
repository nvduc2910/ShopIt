package md510b4f97ca76eaccd593e85752463ac2c;


public class ViewProjectView
	extends md510b4f97ca76eaccd593e85752463ac2c.BaseView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ShopIt.Droid.Views.ViewProjectView, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ViewProjectView.class, __md_methods);
	}


	public ViewProjectView ()
	{
		super ();
		if (getClass () == ViewProjectView.class)
			mono.android.TypeManager.Activate ("ShopIt.Droid.Views.ViewProjectView, ShopIt.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
