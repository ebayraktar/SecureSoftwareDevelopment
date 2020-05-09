
using Android.App;
using Android.OS;
using Android.Support.V4.Media.Session;
using Android.Support.V7.App;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using SSDMobileApp.Adapters;
using SSDMobileApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace SSDMobileApp.Views
{
    [Activity(Label = "İstekler", Theme = "@style/AppTheme.NoActionBar")]
    public class BookRequestsActivity : AppCompatActivity
    {
        RecyclerView rcvBookRequests;
        BookRequestsAdapter adapter;
        List<BookRequests> requests;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_book_requests);
            // Create your application here
            Toolbar myToolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(myToolbar);

            // Get a support ActionBar corresponding to this toolbar
            Android.Support.V7.App.ActionBar ab = SupportActionBar;

            // Enable the Up button
            ab.SetDisplayHomeAsUpEnabled(true);

            rcvBookRequests = FindViewById<RecyclerView>(Resource.Id.rcvBookRequests);

            Initialize();
        }

        private async void Initialize()
        {
            requests = await GetRequestsAsync();
            rcvBookRequests.SetLayoutManager(new LinearLayoutManager(this));
            adapter = new BookRequestsAdapter(requests);
            adapter.ItemAcceptClick += (s, e) =>
            {
                Action(e.Position, true, e.Position);
            };
            adapter.ItemRejectClick += (s, e) =>
            {
                Action(e.Position, false, e.Position);
            };
            rcvBookRequests.SetAdapter(adapter);
        }

        private void Action(int requestId, bool isAccept, int position)
        {
            requests.RemoveAt(position);
            adapter.NotifyItemRemoved(position);
            if (isAccept)
            {
                Accept(requestId);
            }
            else
            {
                Reject(requestId);
            }
        }
        private async void Accept(int requestId)
        {
            await Task.Delay(1000);
        }
        private async void Reject(int requestId)
        {
            await Task.Delay(1000);
        }

        private async Task<List<BookRequests>> GetRequestsAsync()
        {
            List<BookRequests> tempRequests = null;
            var result = await Constants.ServiceManager.Requests();
            if (result != null && result.OpCode == 0)
            {
                tempRequests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookRequests>>(result.Result.ToString());
            }
            return tempRequests;
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