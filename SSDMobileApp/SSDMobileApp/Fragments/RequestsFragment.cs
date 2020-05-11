using System.Collections.Generic;
using System.Threading.Tasks;
using Android.OS;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using SSDMobileApp.Adapters;
using SSDMobileApp.Models;

namespace SSDMobileApp.Fragments
{
    public class RequestsFragment : Android.Support.V4.App.Fragment
    {

        RecyclerView rcvRequests;

        List<BookRequests> bookRequests;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            rcvRequests = View.FindViewById<RecyclerView>(Resource.Id.rcvRequests);

            Initialize();
        }
        private async void Initialize()
        {
            bookRequests = await GetRequestsAsync();

            if (bookRequests == null)
                return;
            rcvRequests.SetLayoutManager(new LinearLayoutManager(View.Context));
            rcvRequests.SetAdapter(new RequestsAdapter(bookRequests));
        }

        async Task<List<BookRequests>> GetRequestsAsync()
        {
            List<BookRequests> tempRequests = null;
            var result = await Constants.ServiceManager.Requests(Constants.StudentId.ToString());
            if (result != null && result.OpCode == 0)
            {
                tempRequests = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookRequests>>(result.Result.ToString());
            }
            return tempRequests;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_requests, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}