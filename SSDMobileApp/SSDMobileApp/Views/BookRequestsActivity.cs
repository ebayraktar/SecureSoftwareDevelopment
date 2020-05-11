
using Android.App;
using Android.OS;
using Android.Support.V4.Media.Session;
using Android.Support.V7.App;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using SSDMobileApp.Adapters;
using SSDMobileApp.Models;
using System.Collections.Generic;
using System.Linq;
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
            if (requests == null)
                return;
            adapter = new BookRequestsAdapter(requests);
            adapter.ItemAcceptClick += (s, e) =>
            {
                Action(e.Position, true);
            };
            adapter.ItemRejectClick += (s, e) =>
            {
                Action(e.Position, false);
            };
            rcvBookRequests.SetAdapter(adapter);
        }

        private async void Action(int position, bool isAccept)
        {
            int requestId = requests[position].RequestId;

            Requests tempRequest = new Requests
            {
                RequestId = requestId,
                Statu = isAccept ? 1 : 2
            };

            string message = "Hata oluştu";
            var result = await Constants.ServiceManager.Requests(tempRequest, requestId.ToString());
            if (result != null && result.OpCode == 0)
            {
                message = "Istek " + (isAccept ? "kabul edildi" : "reddedildi");
            }

            Android.App.AlertDialog dialog = new Android.App.AlertDialog.Builder(this)
                .SetTitle(result.Message)
                .SetMessage(message)
                .SetNegativeButton("TAMAM", (Android.Content.IDialogInterfaceOnClickListener)null)
                .Create();

            dialog.Show();
            requests.RemoveAt(position);
            adapter.NotifyItemRemoved(position);
        }

        private async Task<List<BookRequests>> GetRequestsAsync()
        {
            List<BookRequests> tempRequests = null;
            var result = await Constants.ServiceManager.Requests();
            if (result != null && result.OpCode == 0)
            {
                tempRequests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookRequests>>(result.Result.ToString())?.Where(x => x.Statu.Equals("0")).ToList();
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