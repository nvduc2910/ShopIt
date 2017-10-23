package mono.com.mixpanel.android.surveys;


public class CardCarouselLayout_OnQuestionAnsweredListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.mixpanel.android.surveys.CardCarouselLayout.OnQuestionAnsweredListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onQuestionAnswered:(Lcom/mixpanel/android/mpmetrics/Survey$Question;Ljava/lang/String;)V:GetOnQuestionAnswered_Lcom_mixpanel_android_mpmetrics_Survey_Question_Ljava_lang_String_Handler:Mixpanel.Android.Surveys.CardCarouselLayout/IOnQuestionAnsweredListenerInvoker, MixpanelDroid\n" +
			"";
		mono.android.Runtime.register ("Mixpanel.Android.Surveys.CardCarouselLayout+IOnQuestionAnsweredListenerImplementor, MixpanelDroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CardCarouselLayout_OnQuestionAnsweredListenerImplementor.class, __md_methods);
	}


	public CardCarouselLayout_OnQuestionAnsweredListenerImplementor ()
	{
		super ();
		if (getClass () == CardCarouselLayout_OnQuestionAnsweredListenerImplementor.class)
			mono.android.TypeManager.Activate ("Mixpanel.Android.Surveys.CardCarouselLayout+IOnQuestionAnsweredListenerImplementor, MixpanelDroid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onQuestionAnswered (com.mixpanel.android.mpmetrics.Survey.Question p0, java.lang.String p1)
	{
		n_onQuestionAnswered (p0, p1);
	}

	private native void n_onQuestionAnswered (com.mixpanel.android.mpmetrics.Survey.Question p0, java.lang.String p1);

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
