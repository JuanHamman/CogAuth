package md51b6905f8e81e066cc7329652d4a6cbe1;


public class SignInFaceActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("CogAuth.droid.Activities.SignInFaceActivity, CogAuth.droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SignInFaceActivity.class, __md_methods);
	}


	public SignInFaceActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SignInFaceActivity.class)
			mono.android.TypeManager.Activate ("CogAuth.droid.Activities.SignInFaceActivity, CogAuth.droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
