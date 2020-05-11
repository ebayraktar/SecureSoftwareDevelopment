
using Android.App;
using Android.OS;
using Android.Support.V4.Media.Session;
using Android.Support.V7.App;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using SSDMobileApp.Adapters;
using SSDMobileApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace SSDMobileApp.Views
{
    [Activity(Label = "Yetkiler", Theme = "@style/AppTheme.NoActionBar")]
    public class AuthoritiesActivity : AppCompatActivity
    {
        RecyclerView rcvUserAuthorities;
        UserAuthoritiesAdapter adapter;
        List<Users> users;
        Users currentUser;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.authorities_activity);
            // Create your application here
            Toolbar myToolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(myToolbar);

            // Get a support ActionBar corresponding to this toolbar
            Android.Support.V7.App.ActionBar ab = SupportActionBar;

            // Enable the Up button
            ab.SetDisplayHomeAsUpEnabled(true);

            rcvUserAuthorities = FindViewById<RecyclerView>(Resource.Id.rcvUserAuthorities);

            Initialize();
        }
        private async void Initialize()
        {
            users = await GetUsersAsync();
            if (users == null)
                return;

            adapter = new UserAuthoritiesAdapter(users);
            adapter.ItemClick += (s, e) =>
            {
                ChangeRole(e.Position);
            };
            rcvUserAuthorities.SetLayoutManager(new LinearLayoutManager(this));
            rcvUserAuthorities.SetAdapter(adapter);
        }

        private async Task<List<Users>> GetUsersAsync()
        {
            List<Users> tempUsers = null;
            var result = await Constants.ServiceManager.Users();
            if (result != null && result.OpCode == 0)
            {
                tempUsers = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(result.Result.ToString());
            }
            return tempUsers;
        }

        private async void ChangeRole(int position)
        {
            currentUser = users[position];
            var result = await Constants.ServiceManager.Roles();
            List<Roles> roles = null;
            Roles selectedRole = null;
            if (result != null && result.OpCode == 0)
            {
                roles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Roles>>(result.Result.ToString());
            }
            string[] roleNames = roles.Select(x => x.Name).ToArray();

            Android.App.AlertDialog dialog = new Android.App.AlertDialog.Builder(this)
                .SetTitle("Yetki değiştir")
                .SetItems(roleNames, (s, e) =>
                  {
                      selectedRole = roles[e.Which];
                      SetRole(selectedRole, position);
                  })
                //.SetMessage("Rol seçin")
                .SetNegativeButton("IPTAL", (Android.Content.IDialogInterfaceOnClickListener)null).Create();


            dialog.Show();
        }

        private async void SetRole(Roles role, int position)
        {
            string title = "HATA";
            string message = "Rol güncellenemedi";
            currentUser.RoleId = role.RoleId;
            var result = await Constants.ServiceManager.Users(currentUser, currentUser.UserId.ToString());
            if (result != null && result.OpCode == 0)
            {
                title = "BAŞARILI";
                message = "Rol başarıyla güncellendi";
                adapter.NotifyItemChanged(position);
            }

            Android.App.AlertDialog dialog = new Android.App.AlertDialog.Builder(this)
                .SetTitle(title)
                .SetMessage(message)
                .SetNegativeButton("TAMAM", (Android.Content.IDialogInterfaceOnClickListener)null).Create();


            dialog.Show();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                default:
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}