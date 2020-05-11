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
using System;
using Android.Telephony;

namespace SSDMobileApp.Fragments
{
    public class BooksFragment : Android.Support.V4.App.Fragment, ITextWatcher
    {
        TextInputEditText etBookName;
        //ImageView ivClear;
        RecyclerView rcvBooks;

        List<Books> books;
        BooksAdapter bookAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            etBookName = View.FindViewById<TextInputEditText>(Resource.Id.etBookName);
            //ivClear = View.FindViewById<ImageView>(Resource.Id.ivClear);
            rcvBooks = View.FindViewById<RecyclerView>(Resource.Id.rcvBooks);
            Initialize();

            //ivClear.Click += (s, e) =>
            //{
            //    etBookName.Text = string.Empty;
            //};
        }

        private async void Initialize()
        {

            books = await GetBooksAsync();
            if (books == null)
                return;

            bookAdapter = new BooksAdapter(View.Context, books);
            bookAdapter.ItemFavClick += (s, e) =>
            {
                bookAdapter.ReverseFavorite(e.Position);
            };
            bookAdapter.ItemRequestClick += (s, e) =>
            {
                int bookId = bookAdapter.GetId(e.Position);
                BookRequest(bookId);
            };
            bookAdapter.ItemInfoClick += (s, e) =>
            {
                int bookId = bookAdapter.GetId(e.Position);
                ShowBookPopup(bookId);

            };
            rcvBooks.SetLayoutManager(new LinearLayoutManager(View.Context));
            rcvBooks.SetAdapter(bookAdapter);

            etBookName.AddTextChangedListener(this);
        }

        async Task<List<Books>> GetBooksAsync()
        {
            var result = await Constants.ServiceManager.Books();
            if (result != null && result.OpCode == 0)
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Books>>(result.Result.ToString());
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_books, container, false);
        }

        private async void ShowBookPopup(int bookId)
        {
            LayoutInflater inflater = (LayoutInflater)View.Context.GetSystemService(Android.Content.Context.LayoutInflaterService);
            View bookDetailView = inflater.Inflate(Resource.Layout.popup_book_detail, null);
            AlertDialog alert = null;
            TextView tvBookName = bookDetailView.FindViewById<TextView>(Resource.Id.tvBookName);
            TextView tvAuthorName = bookDetailView.FindViewById<TextView>(Resource.Id.tvAuthorName);
            TextView tvTypeName = bookDetailView.FindViewById<TextView>(Resource.Id.tvTypeName);
            RecyclerView rcvBorrows = bookDetailView.FindViewById<RecyclerView>(Resource.Id.rcvBorrows);
            CardView cvCancel = bookDetailView.FindViewById<CardView>(Resource.Id.cvCancel);
            CardView cvRequest = bookDetailView.FindViewById<CardView>(Resource.Id.cvRequest);

            BookDetail bookDetail = await GetBookDetailAsync(bookId);

            tvBookName.Text = bookDetail?.Name;
            tvAuthorName.Text = bookDetail?.Author;
            tvTypeName.Text = bookDetail?.Type;
            rcvBorrows.SetLayoutManager(new LinearLayoutManager(View.Context));
            if (bookDetail != null)
                rcvBorrows.SetAdapter(new BookBorrowsAdapter(bookDetail.borrowHistory));


            cvCancel.Click += (s, e) =>
            {
                alert?.Dismiss();
            };

            cvRequest.Click += (s, e) =>
           {
               BookRequest(bookId);
           };

            alert = new AlertDialog.Builder(Context)
                .SetView(bookDetailView)
                .Create();

            alert.Show();
        }

        async Task<BookDetail> GetBookDetailAsync(int id)
        {
            BookDetail tempDetail = new BookDetail
            {
                Author = "Error",
                borrowHistory = new List<BookBorrows>(),
                Name = "Error",
                Type = "Error"
            };
            Books book = null;
            var bookResult = await Constants.ServiceManager.Books(id.ToString());
            if (bookResult != null && bookResult.OpCode == 0)
            {
                book = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Books>>(bookResult.Result.ToString()).FirstOrDefault();
            }
            if (book == null)
                return tempDetail;
            tempDetail.Name = book.Name;

            Authors author = null;
            var authorResult = await Constants.ServiceManager.Authors(book.AuthorId.ToString());
            if (authorResult != null && authorResult.OpCode == 0)
            {
                author = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Authors>>(authorResult.Result.ToString()).FirstOrDefault();
            }
            tempDetail.Author = author != null ? author.Name + " " + author.Surname : "Error";

            Types type = null;
            var typeResult = await Constants.ServiceManager.Types(book.TypeId.ToString());
            if (typeResult != null && typeResult.OpCode == 0)
            {
                type = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Types>>(typeResult.Result.ToString()).FirstOrDefault();
            }
            tempDetail.Type = type?.Name;

            List<BookBorrows> borrowList = null;
            var borrowResult = await Constants.ServiceManager.Borrows(book.BookId.ToString());
            if (borrowResult != null && borrowResult.OpCode == 0)
            {
                borrowList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BookBorrows>>(borrowResult.Result.ToString());
            }
            //if (borrowList == null || borrowList.Count == 0)
            //{
            //    borrowList = new List<BookBorrows>()
            //    {
            //        new BookBorrows{ StudentName="Student 360", TakenDate="2015-08-09 13:26:00.000", BroughtDate="2015-08-20 06:59:00.000"},
            //        new BookBorrows{ StudentName="Student 308", TakenDate="2015-08-10 19:44:00.000", BroughtDate="2015-08-15 10:46:00.000"},
            //        new BookBorrows{ StudentName="Student 288", TakenDate="2015-08-10 22:05:00.000", BroughtDate="2015-08-19 17:28:00.000"},
            //        new BookBorrows{ StudentName="Student 157", TakenDate="2015-08-24 20:06:00.000", BroughtDate="2015-08-20 06:59:00.000"},
            //        new BookBorrows{ StudentName="Student 451", TakenDate="2015-08-11 02:32:00.000", BroughtDate="2015-08-17 15:12:00.000"},
            //        new BookBorrows{ StudentName="Student 214", TakenDate="2015-08-12 12:05:00.000", BroughtDate="2015-08-21 07:16:00.000"},
            //    };
            //}
            tempDetail.borrowHistory = borrowList;

            return tempDetail;
        }

        async void BookRequest(int bookId)
        {
            Requests tempRequest = new Requests
            {
                BookId = bookId,
                RequestDate = DateTime.Now.ToShortDateString(),
                Statu = 0,
                StudentId = Constants.StudentId
            };
            string message = "Hata oluştu";
            var result = await Constants.ServiceManager.Requests(tempRequest);
            if (result != null && result.OpCode == 0)
            {
                message = "İstek talebi gönderildi";
            }

            Android.App.AlertDialog dialog = new Android.App.AlertDialog.Builder(View.Context)
                .SetTitle(result.Message)
                .SetMessage(message)
                .SetIcon(Android.Resource.Drawable.IcDialogAlert)
                .SetNegativeButton("TAMAM", (Android.Content.IDialogInterfaceOnClickListener)null)
                .Create();

            dialog.Show();
        }

        public void AfterTextChanged(IEditable s)
        {
            //throw new System.NotImplementedException();
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            //throw new System.NotImplementedException();
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            //if (string.IsNullOrEmpty(s.ToString()))
            //{
            //    ivClear.Visibility = ViewStates.Gone;
            //}
            //else
            //{
            //    ivClear.Visibility = ViewStates.Visible;
            //}
            bookAdapter.Filter?.InvokeFilter(s);
        }
    }
}