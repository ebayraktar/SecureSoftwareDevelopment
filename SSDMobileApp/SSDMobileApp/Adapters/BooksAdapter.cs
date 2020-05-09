using System;

using Android.Views;
using Android.Widget;
using SSDMobileApp.Models;
using System.Collections.Generic;
using Android.Content.Res;
using Android.Content;
using Android.Support.V4.Content;
using Java.Lang;
using System.Linq;
using AndroidX.RecyclerView.Widget;

namespace SSDMobileApp.Adapters
{
    public class BooksAdapter : RecyclerView.Adapter, IFilterable
    {
        public event EventHandler<BooksAdapterClickEventArgs> ItemClick;
        public event EventHandler<BooksAdapterClickEventArgs> ItemFavClick;
        public event EventHandler<BooksAdapterClickEventArgs> ItemInfoClick;
        public event EventHandler<BooksAdapterClickEventArgs> ItemRequestClick;
        public event EventHandler<BooksAdapterClickEventArgs> ItemLongClick;

        private List<Books> items;
        private List<Books> originalData;
        private readonly Context context;

        public BooksAdapter(Context context, List<Books> data)
        {
            items = data;
            this.context = context;
            Filter = new BooksFilter(this);
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.books_listitem;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new BooksAdapterViewHolder(itemView, OnClick, OnFavClick, OnInfoClick, OnRequestClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as BooksAdapterViewHolder;


            holder.TvBookName.Text = item.Name;
            holder.TvPageCount.Text = item.Pagecount.ToString() + " Sayfa";

            if (!item.IsFavorite)
            {
                holder.IvFavorite.SetColorFilter(context.Resources.GetColor(Resource.Color.colorPrimary));
            }
            else
            {
                holder.IvFavorite.ClearColorFilter();
            }
        }

        public void ReverseFavorite(int position)
        {
            var item = items[position];
            item.IsFavorite = !item.IsFavorite;
            NotifyItemChanged(position);
        }

        public int GetId(int position)
        {
            return items[position].BookId;
        }

        public override int ItemCount => items.Count;

        public Filter Filter { get; private set; }

        void OnClick(BooksAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnFavClick(BooksAdapterClickEventArgs args) => ItemFavClick?.Invoke(this, args);
        void OnInfoClick(BooksAdapterClickEventArgs args) => ItemInfoClick?.Invoke(this, args);
        void OnRequestClick(BooksAdapterClickEventArgs args) => ItemRequestClick?.Invoke(this, args);
        void OnLongClick(BooksAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

        private class BooksFilter : Filter
        {
            private readonly BooksAdapter _adapter;
            public BooksFilter(BooksAdapter adapter)
            {
                _adapter = adapter;
            }

            protected override FilterResults PerformFiltering(ICharSequence constraint)
            {
                var returnObj = new FilterResults();
                var results = new List<Books>();
                if (_adapter.originalData == null)
                    _adapter.originalData = _adapter.items;

                if (constraint == null) return returnObj;

                if (_adapter.originalData != null && _adapter.originalData.Any())
                {
                    // Compare constraint to all names lowercased. 
                    // It they are contained they are added to results.
                    results.AddRange(
                        _adapter.originalData.Where(
                            book => book.Name.ToLower().Contains(constraint.ToString())));
                }

                // Nasty piece of .NET to Java wrapping, be careful with this!
                returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
                returnObj.Count = results.Count;

                constraint.Dispose();

                return returnObj;
            }

            protected override void PublishResults(ICharSequence constraint, FilterResults results)
            {
                using (var values = results.Values)
                    _adapter.items = values.ToArray<Java.Lang.Object>()
                        .Select(r => r.ToNetObject<Books>()).ToList();

                _adapter.NotifyDataSetChanged();

                // Don't do this and see GREF counts rising
                constraint.Dispose();
                results.Dispose();
            }
        }

    }

    public class BooksAdapterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView IvFavorite { get; set; }
        public TextView TvBookName { get; set; }
        public TextView TvPageCount { get; set; }
        public ImageView IvRequest { get; set; }
        public ImageView IvInfo { get; set; }

        public BooksAdapterViewHolder(View itemView, Action<BooksAdapterClickEventArgs> clickListener,
            Action<BooksAdapterClickEventArgs> favClickListener,
            Action<BooksAdapterClickEventArgs> infoClickListener,
            Action<BooksAdapterClickEventArgs> requestClickListener,
            Action<BooksAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;

            IvFavorite = itemView.FindViewById<ImageView>(Resource.Id.ivFavorite);
            TvBookName = itemView.FindViewById<TextView>(Resource.Id.tvBookName);
            TvPageCount = itemView.FindViewById<TextView>(Resource.Id.tvPageCount);
            IvInfo = itemView.FindViewById<ImageView>(Resource.Id.ivInfo);
            IvRequest = itemView.FindViewById<ImageView>(Resource.Id.ivRequest);

            itemView.Click += (sender, e) => clickListener(new BooksAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            IvFavorite.Click += (sender, e) => favClickListener(new BooksAdapterClickEventArgs { View = IvFavorite, Position = AdapterPosition });
            IvInfo.Click += (sender, e) => infoClickListener(new BooksAdapterClickEventArgs { View = IvInfo, Position = AdapterPosition });
            IvRequest.Click += (sender, e) => requestClickListener(new BooksAdapterClickEventArgs { View = IvRequest, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new BooksAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class BooksAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}