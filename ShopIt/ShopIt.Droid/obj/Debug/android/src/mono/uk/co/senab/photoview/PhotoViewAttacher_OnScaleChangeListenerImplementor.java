package mono.uk.co.senab.photoview;


public class PhotoViewAttacher_OnScaleChangeListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		uk.co.senab.photoview.PhotoViewAttacher.OnScaleChangeListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onScaleChange:(FFF)V:GetOnScaleChange_FFFHandler:ImageViews.Photo.PhotoViewAttacher/IOnScaleChangeListenerInvoker, PhotoView\n" +
			"";
		mono.android.Runtime.register ("ImageViews.Photo.PhotoViewAttacher+IOnScaleChangeListenerImplementor, PhotoView, Version=1.2.4.0, Culture=neutral, PublicKeyToken=null", PhotoViewAttacher_OnScaleChangeListenerImplementor.class, __md_methods);
	}


	public PhotoViewAttacher_OnScaleChangeListenerImplementor ()
	{
		super ();
		if (getClass () == PhotoViewAttacher_OnScaleChangeListenerImplementor.class)
			mono.android.TypeManager.Activate ("ImageViews.Photo.PhotoViewAttacher+IOnScaleChangeListenerImplementor, PhotoView, Version=1.2.4.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onScaleChange (float p0, float p1, float p2)
	{
		n_onScaleChange (p0, p1, p2);
	}

	private native void n_onScaleChange (float p0, float p1, float p2);

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
