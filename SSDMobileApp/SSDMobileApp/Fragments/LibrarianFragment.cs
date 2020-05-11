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
using Android.Content;

namespace SSDMobileApp.Fragments
{
    public class LibrarianFragment : Android.Support.V4.App.Fragment
    {
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
            View.FindViewById<CardView>(Resource.Id.cvEditBook).Click += (s, e) =>
            {
                EditBook();
            };
        }

        private async void EditBook()
        {
            var result = await Constants.ServiceManager.Books();
            List<Books> books = null;
            Books selectedBook = null;
            if (result != null && result.OpCode == 0)
            {
                books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Books>>(result.Result.ToString());
            }
            string[] bookNames = books.Select(x => x.Name).ToArray();

            Android.App.AlertDialog dialog = new Android.App.AlertDialog.Builder(View.Context)
                .SetTitle("Düzenlemek istediğiniz kitabı seçin")
                .SetItems(bookNames, (s, e) =>
                {
                    selectedBook = books[e.Which];
                    OnItemSelected(selectedBook);
                })
                .SetNegativeButton("IPTAL", (Android.Content.IDialogInterfaceOnClickListener)null).Create();


            dialog.Show();
        }

        private void OnItemSelected(Books book)
        {
            Intent intent = new Intent(View.Context, typeof(AddBookActivity));
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(book);
            intent.PutExtra(Constants.BOOK_EXTRA, json);
            StartActivity(intent);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_librarian, container, false);
        }
    }
}