package crc648016037127868f67;


public class LibrarianSettingsActivity
	extends androidx.appcompat.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onOptionsItemSelected:(Landroid/view/MenuItem;)Z:GetOnOptionsItemSelected_Landroid_view_MenuItem_Handler\n" +
			"";
		mono.android.Runtime.register ("SSDMobileApp.Views.LibrarianSettingsActivity, SSDMobileApp", LibrarianSettingsActivity.class, __md_methods);
	}


	public LibrarianSettingsActivity ()
	{
		super ();
		if (getClass () == LibrarianSettingsActivity.class)
			mono.android.TypeManager.Activate ("SSDMobileApp.Views.LibrarianSettingsActivity, SSDMobileApp", "", this, new java.lang.Object[] {  });
	}


	public LibrarianSettingsActivity (int p0)
	{
		super (p0);
		if (getClass () == LibrarianSettingsActivity.class)
			mono.android.TypeManager.Activate ("SSDMobileApp.Views.LibrarianSettingsActivity, SSDMobileApp", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public boolean onOptionsItemSelected (android.view.MenuItem p0)
	{
		return n_onOptionsItemSelected (p0);
	}

	private native boolean n_onOptionsItemSelected (android.view.MenuItem p0);

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
