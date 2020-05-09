using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using SSDMobileApp.Models;

namespace SSDMobileApp.Adapters
{
    class BookBorrowsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<BookBorrowsAdapterClickEventArgs> ItemClick;
        public event EventHandler<BookBorrowsAdapterClickEventArgs> ItemLongClick;

        readonly List<BookBorrows> items;

        public BookBorrowsAdapter(List<BookBorrows> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.borrows_listitem;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new BookBorrowsAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as BookBorrowsAdapterViewHolder;
            holder.TvStudentName.Text = item?.StudentName;

            DateTime takenDate = DateTime.Parse(item?.TakenDate);
            DateTime broughtDate = DateTime.Parse(item?.BroughtDate);
            holder.TvBorrowsTakenDate.Text = takenDate.ToString("yyyy/MM/dd");
            holder.TvBorrowBroughtDate.Text = broughtDate.ToString("yyyy/MM/dd");
        }

        public override int ItemCount => items.Count;

        void OnClick(BookBorrowsAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(BookBorrowsAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class BookBorrowsAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView TvStudentName { get; set; }
        public TextView TvBorrowsTakenDate { get; set; }
        public TextView TvBorrowBroughtDate { get; set; }


        public BookBorrowsAdapterViewHolder(View itemView, Action<BookBorrowsAdapterClickEventArgs> clickListener,
                            Action<BookBorrowsAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            TvStudentName = itemView.FindViewById<TextView>(Resource.Id.tvStudentName);
            TvBorrowsTakenDate = itemView.FindViewById<TextView>(Resource.Id.tvBorrowsTakenDate);
            TvBorrowBroughtDate = itemView.FindViewById<TextView>(Resource.Id.tvBorrowBroughtDate);
            itemView.Click += (sender, e) => clickListener(new BookBorrowsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new BookBorrowsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class BookBorrowsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}