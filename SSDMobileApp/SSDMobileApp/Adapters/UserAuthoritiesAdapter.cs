using System;

using Android.Views;
using Android.Widget;

using System.Collections.Generic;
using SSDMobileApp.Models;
using AndroidX.RecyclerView.Widget;

namespace SSDMobileApp.Adapters
{
    class UserAuthoritiesAdapter : RecyclerView.Adapter
    {
        public event EventHandler<UserAuthoritiesAdapterClickEventArgs> ItemClick;
        public event EventHandler<UserAuthoritiesAdapterClickEventArgs> ItemLongClick;
        List<Users> items;

        public UserAuthoritiesAdapter(List<Users> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.authority_listitem;
            itemView = LayoutInflater.From(parent.Context).
                   Inflate(id, parent, false);

            var vh = new UserAuthoritiesAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as UserAuthoritiesAdapterViewHolder;
            holder.TvUsername.Text = item.UserName;
            holder.TvNameSurname.Text = item.Name + " " + item.Surname;
            string role = "";
            switch (item.RoleId)
            {
                case 1:
                    role = "Admin";
                    break;
                case 2:
                    role = "Kütüphaneci";
                    break;
                case 3:
                    role = "Öğretmen";
                    break;
                case 4:
                    role = "Öğrenci";
                    break;
                case 0:
                    role = "Süper Kullanıcı";
                    break;
                default:
                    break;
            }
            holder.TvRole.Text = role;
        }

        public override int ItemCount => items.Count;

        void OnClick(UserAuthoritiesAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(UserAuthoritiesAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class UserAuthoritiesAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView TvUsername { get; set; }
        public TextView TvNameSurname { get; set; }
        public TextView TvRole { get; set; }
        public ImageView IvChangeRole { get; set; }


        public UserAuthoritiesAdapterViewHolder(View itemView, Action<UserAuthoritiesAdapterClickEventArgs> clickListener,
                            Action<UserAuthoritiesAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            TvUsername = ItemView.FindViewById<TextView>(Resource.Id.tvUsername);
            TvNameSurname = ItemView.FindViewById<TextView>(Resource.Id.tvNameSurname);
            TvRole = ItemView.FindViewById<TextView>(Resource.Id.tvRole);

            IvChangeRole = ItemView.FindViewById<ImageView>(Resource.Id.ivChangeRole);

            IvChangeRole.Click += (sender, e) => clickListener(new UserAuthoritiesAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new UserAuthoritiesAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class UserAuthoritiesAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}