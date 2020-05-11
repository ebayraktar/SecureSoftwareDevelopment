using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using AndroidX.CardView.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using SSDMobileApp.Views;

namespace SSDMobileApp.Fragments
{
    public class AdminFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);


            View.FindViewById<CardView>(Resource.Id.cvEditAuthorities).Click += (s, e) =>
            {
                StartActivity(new Android.Content.Intent(View.Context, typeof(AuthoritiesActivity)));
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_admin, container, false);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}