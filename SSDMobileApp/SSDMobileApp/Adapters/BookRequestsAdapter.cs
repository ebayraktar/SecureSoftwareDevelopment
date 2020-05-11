using System;

using Android.Views;
using Android.Widget;

using System.Collections.Generic;
using SSDMobileApp.Models;
using AndroidX.RecyclerView.Widget;

namespace SSDMobileApp.Adapters
{
    class BookRequestsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<BookRequestsAdapterClickEventArgs> ItemAcceptClick;
        public event EventHandler<BookRequestsAdapterClickEventArgs> ItemRejectClick;

        readonly List<BookRequests> items;

        public BookRequestsAdapter(List<BookRequests> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.book_request_listitem;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new BookRequestsAdapterViewHolder(itemView, OnAcceptClick, OnRejectClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as BookRequestsAdapterViewHolder;
            holder.TvBookName.Text = items[position].BookName;
            holder.TvStudentName.Text = items[position].StudentName;
            DateTime requestDate = DateTime.Parse(item?.RequestDate);
            holder.TvRequestDate.Text = requestDate.ToString("yyyy/MM/dd");
        }

        public override int ItemCount => items.Count;

        void OnAcceptClick(BookRequestsAdapterClickEventArgs args) => ItemAcceptClick?.Invoke(this, args);
        void OnRejectClick(BookRequestsAdapterClickEventArgs args) => ItemRejectClick?.Invoke(this, args);

    }

    public class BookRequestsAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView TvStudentName { get; set; }
        public TextView TvBookName { get; set; }
        public TextView TvRequestDate { get; set; }
        public ImageView IvReject { get; set; }
        public ImageView IvAccept { get; set; }


        public BookRequestsAdapterViewHolder(View itemView, Action<BookRequestsAdapterClickEventArgs> acceptClickListener,
                            Action<BookRequestsAdapterClickEventArgs> recejtClickListener) : base(itemView)
        {
            TvStudentName = itemView.FindViewById<TextView>(Resource.Id.tvStudentName);
            TvBookName = itemView.FindViewById<TextView>(Resource.Id.tvBookName);
            TvRequestDate = itemView.FindViewById<TextView>(Resource.Id.tvRequestDate);

            IvReject = itemView.FindViewById<ImageView>(Resource.Id.ivReject);
            IvAccept = itemView.FindViewById<ImageView>(Resource.Id.ivAccept);

            IvAccept.Click += (sender, e) => acceptClickListener(new BookRequestsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            IvReject.Click += (sender, e) => recejtClickListener(new BookRequestsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class BookRequestsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}