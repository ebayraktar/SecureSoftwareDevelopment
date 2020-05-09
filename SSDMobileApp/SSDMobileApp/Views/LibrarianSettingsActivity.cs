using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace SSDMobileApp.Views
{
    [Activity(Label = "Kütüphaneci Ayarları", Theme = "@style/AppTheme.NoActionBar")]
    public class LibrarianSettingsActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_librarian_settings);
            // Create your application here
            Toolbar myToolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(myToolbar);

            // Get a support ActionBar corresponding to this toolbar
            Android.Support.V7.App.ActionBar ab = SupportActionBar;

            // Enable the Up button
            ab.SetDisplayHomeAsUpEnabled(true);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}