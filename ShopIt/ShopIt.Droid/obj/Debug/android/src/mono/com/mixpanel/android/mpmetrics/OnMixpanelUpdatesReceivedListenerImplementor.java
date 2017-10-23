package mono.com.mixpanel.android.mpmetrics;


public class OnMixpanelUpdatesReceivedListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mixpanel.android.mpmetrics.OnMixpanelUpdatesReceivedListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMixpanelUpdatesReceived:()V:GetOnMixpanelUpdatesReceivedHandler:Mixpanel.Android.MpMetrics.IOnMixpanelUpdatesReceivedListenerInvoker, MixpanelDroid\n" +
			"";
		mono.android.Runtime.register ("Mixpanel.Android.MpMetrics.IOnMixpanelUpdatesReceivedListenerImplementor, MixpanelDroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", OnMixpanelUpdatesReceivedListenerImplementor.class, __md_methods);
	}


	public OnMixpanelUpdatesReceivedListenerImplementor ()
	{
		super ();
		if (getClass () == OnMixpanelUpdatesReceivedListenerImplementor.class)
			mono.android.TypeManager.Activate ("Mixpanel.Android.MpMetrics.IOnMixpanelUpdatesReceivedListenerImplementor, MixpanelDroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onMixpanelUpdatesReceived ()
	{
		n_onMixpanelUpdatesReceived ();
	}

	private native void n_onMixpanelUpdatesReceived ();

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
