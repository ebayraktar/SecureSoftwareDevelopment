using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using SSDMobileApp.Models;
using SSDMobileApp.REST;

namespace SSDMobileApp.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", Icon = "@drawable/library_ico", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        //TextInputEditText currentTIET;
        //TextInputLayout currentTIL;

        TextInputEditText etUsername, etPassword;
        TextInputLayout tilUsername, tilPassword;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            // Create your application here

            Initialize();

            FindViewById(Resource.Id.cvLogin).Click += (s, e) =>
            {
                if (ValidateForm())
                {
                    LoginAsync();
                }
            };
            FindViewById(Resource.Id.tvSignUp).Click += (s, e) =>
            {
                Intent signUpIntent = new Intent(this, typeof(SignupActivity));
                StartActivity(signUpIntent);
            };
        }

        private void Initialize()
        {
            //App INIT
            new Constants();
            //

            etUsername = FindViewById<TextInputEditText>(Resource.Id.etUsername);
            etPassword = FindViewById<TextInputEditText>(Resource.Id.etPassword);
            tilUsername = FindViewById<TextInputLayout>(Resource.Id.tilUsername);
            tilPassword = FindViewById<TextInputLayout>(Resource.Id.tilPassword);

            etUsername.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    if (string.IsNullOrEmpty(etUsername.Text))
                    {
                        tilUsername.Error = "Boş olamaz";
                    }
                    else
                    {
                        tilUsername.Error = string.Empty;
                    }
                }
            };
            etPassword.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    if (string.IsNullOrEmpty(etUsername.Text))
                    {
                        tilPassword.Error = "Boş olamaz";
                    }
                    else
                    {
                        tilPassword.Error = string.Empty;
                    }
                }
            };
        }
        private async void LoginAsync()
        {
            LoginModel model = new LoginModel
            {
                Username = etUsername.Text,
                Password = etPassword.Text
            };
            var result = await Constants.ServiceManager.LoginAsync(model);
            if (result != null && result.OpCode == 0)
            {
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponseModel>(result.Result.ToString());
                Constants.UserId = response.UserId;
                Constants.RoleId = response.RoleId;
                Constants.Token = response.Token;
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
            else
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Giriş yapılamadı")
                    .SetMessage(result.Message)
                    .SetNegativeButton("TAMAM", (IDialogInterfaceOnClickListener)null)
                    .Create().Show();
            }

        }
        private bool ValidateForm()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(etUsername.Text))
            {
                tilUsername.Error = "Boş olamaz";
                isValid = false;
            }
            else
            {
                tilUsername.Error = string.Empty;
            }
            if (string.IsNullOrEmpty(etPassword.Text))
            {
                tilPassword.Error = "Boş Olamaz!";
                isValid = false;
            }
            else
            {
                tilPassword.Error = string.Empty;
            }
            return isValid;
        }
    }
}