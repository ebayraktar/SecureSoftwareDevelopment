using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using SSDMobileApp.Fragments;
using SSDMobileApp.Views;
using Fragment = Android.Support.V4.App.Fragment;

namespace SSDMobileApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        internal MainFragment mainFragment;
        internal BooksFragment booksFragment;
        internal RequestsFragment requestsFragment;
        internal AdminFragment adminFragment;
        internal LibrarianFragment librarianFragment;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            Title = "Secure Software Kütüphanesi";
            fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            mainFragment = new MainFragment();
            Initialize();
        }

        private void Initialize()
        {
            var trans = SupportFragmentManager.BeginTransaction();
            trans.Add(Resource.Id.flytMainContent, mainFragment, mainFragment.Tag);
            trans.Commit();

            currentFragment = mainFragment;
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                if (doubleBackToExitPressedOnce)
                {
                    base.OnBackPressed();
                    return;
                }
                doubleBackToExitPressedOnce = true;
                Toast.MakeText(this, "Çıkış yapmak için tekrar bas", ToastLength.Short).Show();
                new Handler().PostDelayed(() =>
                {
                    doubleBackToExitPressedOnce = false;
                }, 2000);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Bizimle iletişime geç!", Snackbar.LengthLong)
                .SetAction("Email", (s) =>
                {
                    Toast.MakeText(view.Context, "Action!", ToastLength.Short).Show();
                }).Show();
        }

        public bool LoadFragment(Fragment fragment)
        {
            if (fragment == null || fragment == currentFragment)
                return false;

            InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
            imm.HideSoftInputFromWindow(CurrentFocus?.WindowToken, HideSoftInputFlags.None);

            var trans = SupportFragmentManager.BeginTransaction();
            trans.Replace(Resource.Id.flytMainContent, fragment, fragment.Tag);
            trans.Commit();
            currentFragment = fragment;
            return true;
        }

        private void Exit()
        {
            Constants.UserId = -1;
            Constants.StudentId = -1;
            Constants.RoleId = -1;
            Constants.Token = string.Empty;
            StartActivity(new Intent(this, typeof(LoginActivity)));
            OverridePendingTransition(Android.Resource.Animation.FadeIn, Android.Resource.Animation.FadeOut);
            Finish();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            string title = "";
            bool valid = true;
            int id = item.ItemId;
            Fragment frm = null;
            fab.Visibility = ViewStates.Gone;
            if (id == Resource.Id.nav_home)
            {
                if (mainFragment == null)
                {
                    mainFragment = new MainFragment();
                }
                fab.Visibility = ViewStates.Visible;
                frm = mainFragment;
                title = "Secure Software Kütüphanesi";
            }
            else if (id == Resource.Id.nav_books)
            {
                /* SECURE 3
                if (Constants.RoleId != 4)
                {
                    ShowMessage(this, "Yetkilendirme hatası", "Yönetici hesabıyla istekte bulunamazsınız");
                    return false;
                }
                */
                if (booksFragment == null)
                {
                    booksFragment = new BooksFragment();
                }
                frm = booksFragment;
                title = "Kitaplar";
            }
            else if (id == Resource.Id.nav_requests)
            {
                /* SECURE 3
                if (Constants.RoleId != 4)
                {
                    ShowMessage(this, "Yetkilendirme hatası", "Yönetici hesabıyla istekte bulunamazsınız");
                    return false;
                }
                */
                if (requestsFragment == null)
                {
                    requestsFragment = new RequestsFragment();
                }
                frm = requestsFragment;
                title = "Taleplerim";
            }
            else if (id == Resource.Id.nav_librarian)
            {
                /* SECURE 3
                if (Constants.RoleId == 4 || Constants.RoleId == 3)
                {
                    ShowMessage(this, "Yetkilendirme hatası", "Bu sayfaya erişebilmek için yetkiniz yok");
                    return false;
                }
                */
                if (librarianFragment == null)
                {
                    librarianFragment = new LibrarianFragment();
                }
                frm = librarianFragment;
                title = "Kütüphaneci Paneli";
            }
            else if (id == Resource.Id.nav_admin)
            {
                if (Constants.RoleId == 4 || Constants.RoleId == 3 || Constants.RoleId == 2)
                {
                    ShowMessage(this, "Yetkilendirme hatası", "Bu sayfaya erişebilmek için yetkiniz yok");
                    return false;
                }
                if (adminFragment == null)
                {
                    adminFragment = new AdminFragment();
                }
                frm = adminFragment;
                title = "Admin Paneli";
            }
            else if (id == Resource.Id.nav_exit)
            {
                Exit();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            LoadFragment(frm);
            Title = title;
            return valid;
        }

        public void ShowMessage(Context context, string title, string message)
        {
            Android.App.AlertDialog dialog = new Android.App.AlertDialog.Builder(context)
                .SetTitle(title)
                .SetMessage(message)
                .SetNegativeButton("TAMAM", (IDialogInterfaceOnClickListener)null)
                .Create();

            dialog.Show();

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        FloatingActionButton fab;
        private Fragment currentFragment;
        private bool doubleBackToExitPressedOnce = false;
    }
}

