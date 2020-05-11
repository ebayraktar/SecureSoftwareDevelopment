using System;

using Android.Views;
using Android.Widget;
using SSDMobileApp.Models;
using System.Collections.Generic;
using AndroidX.RecyclerView.Widget;

namespace SSDMobileApp.Adapters
{
    class RequestsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<RequestsAdapterClickEventArgs> ItemClick;
        public event EventHandler<RequestsAdapterClickEventArgs> ItemLongClick;

        readonly List<BookRequests> items;

        public RequestsAdapter(List<BookRequests> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.requests_listitem;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new RequestsAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as RequestsAdapterViewHolder;
            holder.TvBookName.Text = item.BookName;
            holder.TvRequestDate.Text = item.RequestDate;
            holder.TvStatu.Text = item.Statu;
        }

        public override int ItemCount => items.Count;

        void OnClick(RequestsAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(RequestsAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class RequestsAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView TvBookName { get; set; }
        public TextView TvRequestDate { get; set; }
        public TextView TvStatu { get; set; }


        public RequestsAdapterViewHolder(View itemView, Action<RequestsAdapterClickEventArgs> clickListener,
                            Action<RequestsAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            TvBookName = ItemView.FindViewById<TextView>(Resource.Id.tvBookName);
            TvRequestDate = ItemView.FindViewById<TextView>(Resource.Id.tvRequestDate);
            TvStatu = ItemView.FindViewById<TextView>(Resource.Id.tvStatu);

            itemView.Click += (sender, e) => clickListener(new RequestsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RequestsAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class RequestsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}