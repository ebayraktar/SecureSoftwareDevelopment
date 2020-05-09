using Android.OS;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Java.Lang;
using SSDMobileApp.Adapters;
using SSDMobileApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AndroidX.CardView.Widget;
using Android.App;
using System.Linq;
using SSDMobileApp.Views;

namespace SSDMobileApp.Fragments
{
    public class LibrarianFragment : Android.Support.V4.App.Fragment
    {
        CardView cvRequests, cvAddBook, cvSettings;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            View.FindViewById<CardView>(Resource.Id.cvRequests).Click += (s, e) =>
            {
                StartActivity(new Android.Content.Intent(View.Context, typeof(BookRequestsActivity)));
            };
            View.FindViewById<CardView>(Resource.Id.cvAddBook).Click += (s, e) =>
            {
                StartActivity(new Android.Content.Intent(View.Context, typeof(AddBookActivity)));

            };
            View.FindViewById<CardView>(Resource.Id.cvSettings).Click += (s, e) =>
            {
                StartActivity(new Android.Content.Intent(View.Context, typeof(LibrarianSettingsActivity)));

            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.librarian_fragment, container, false);
        }
    }
}