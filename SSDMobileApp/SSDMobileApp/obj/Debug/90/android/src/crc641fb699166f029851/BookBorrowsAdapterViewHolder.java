package crc641fb699166f029851;


public class BookBorrowsAdapterViewHolder
	extends androidx.recyclerview.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SSDMobileApp.Adapters.BookBorrowsAdapterViewHolder, SSDMobileApp", BookBorrowsAdapterViewHolder.class, __md_methods);
	}


	public BookBorrowsAdapterViewHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == BookBorrowsAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("SSDMobileApp.Adapters.BookBorrowsAdapterViewHolder, SSDMobileApp", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
