using Android.OS;
using Android.Views;
using Android.Widget;
using SSDMobileApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSDMobileApp.Fragments
{
    public class MainFragment : Android.Support.V4.App.Fragment
    {
        TextView tvAuthorCount, tvBookCount, tvStudentCount, tvBorrowCount, tvTypeCount;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            tvAuthorCount = View.FindViewById<TextView>(Resource.Id.tvAuthorCount);
            tvBookCount = View.FindViewById<TextView>(Resource.Id.tvBookCount);
            tvStudentCount = View.FindViewById<TextView>(Resource.Id.tvStudentCount);
            tvBorrowCount = View.FindViewById<TextView>(Resource.Id.tvBorrowCount);
            tvTypeCount = View.FindViewById<TextView>(Resource.Id.tvTypeCount);
            Initialize();
        }
        private async void Initialize()
        {
            tvAuthorCount.Text = await AuthorsAsync();
            tvBookCount.Text = await BooksAsync();
            tvStudentCount.Text = await StudentsAsync();
            tvBorrowCount.Text = await BorrowsAsync();
            tvTypeCount.Text = await TypesAsync();
        }

        private async Task<string> AuthorsAsync()
        {
            var result = await Constants.ServiceManager.Authors();
            if (result != null && result.OpCode == 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Authors>>(result.Result.ToString()).Count.ToString();
            }
            return "Hata! " + result.Result.ToString();
        }
        private async Task<string> BooksAsync()
        {
            var result = await Constants.ServiceManager.Books();
            if (result != null && result.OpCode == 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Books>>(result.Result.ToString()).Count.ToString();
            }
            return "Hata! " + result.Result.ToString();
        }
        private async Task<string> StudentsAsync()
        {
            var result = await Constants.ServiceManager.Students();
            if (result != null && result.OpCode == 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Students>>(result.Result.ToString()).Count.ToString();
            }
            return "Hata! " + result.Result.ToString();
        }
        private async Task<string> BorrowsAsync()
        {
            var result = await Constants.ServiceManager.Borrows();
            if (result != null && result.OpCode == 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Borrows>>(result.Result.ToString()).Count.ToString();
            }
            return "Hata! " + result.Result.ToString();
        }
        private async Task<string> TypesAsync()
        {
            var result = await Constants.ServiceManager.Types();
            if (result != null && result.OpCode == 0)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Types>>(result.Result.ToString()).Count.ToString();
            }
            return "Hata! " + result.Result.ToString();
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_main, container, false);
        }
    }
}