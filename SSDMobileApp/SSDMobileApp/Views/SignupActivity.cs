using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using SSDMobileApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSDMobileApp.Views
{
    [Activity(Label = "@string/sign_up", Theme = "@style/AppTheme.NoActionBar")]
    public class SignupActivity : Activity
    {
        TextInputEditText etName, etSurname, etUsername, etPassword;
        TextInputLayout tilName, tilSurname, tilUsername, tilPassword, tilRole;
        Spinner spnRole;

        TextInputEditText[] editTexts;
        TextInputLayout[] layouts;
        List<Roles> roles;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_signup);
            // Create your application here
            Initialize();

            FindViewById(Resource.Id.imgBtnBack).Click += (s, e) =>
            {
                OnBackPressed();
            };
            FindViewById(Resource.Id.cvSignUp).Click += (s, e) =>
            {
                if (ValidateForm())
                {
                    SignupAsync();
                }
            };

            etName.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    Validation(etName, tilName);
                }
            };
            etSurname.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    Validation(etSurname, tilSurname);
                }
            };
            etUsername.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    Validation(etUsername, tilUsername);
                }
            };
            etPassword.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    Validation(etPassword, tilPassword);
                }
            };

        }
        private void Initialize()
        {
            etName = FindViewById<TextInputEditText>(Resource.Id.etName);
            etSurname = FindViewById<TextInputEditText>(Resource.Id.etSurname);
            etUsername = FindViewById<TextInputEditText>(Resource.Id.etUsername);
            etPassword = FindViewById<TextInputEditText>(Resource.Id.etPassword);

            tilName = FindViewById<TextInputLayout>(Resource.Id.tilName);
            tilSurname = FindViewById<TextInputLayout>(Resource.Id.tilSurname);
            tilUsername = FindViewById<TextInputLayout>(Resource.Id.tilUsername);
            tilPassword = FindViewById<TextInputLayout>(Resource.Id.tilPassword);

            tilRole = FindViewById<TextInputLayout>(Resource.Id.tilRole);
            spnRole = FindViewById<Spinner>(Resource.Id.spnRole);

            editTexts = new TextInputEditText[] { etName, etSurname, etUsername, etPassword };
            layouts = new TextInputLayout[] { tilName, tilSurname, tilUsername, tilPassword };

            GetRoles();
        }

        private async void GetRoles()
        {
            var result = await Constants.ServiceManager.Roles();
            if (result != null && result.OpCode == 0)
            {
                roles = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Roles>>(result.Result.ToString());
                List<string> roleNames = roles.Select(x => x.Name).ToList();
                ArrayAdapter roleAdapter = new ArrayAdapter(this, Resource.Layout.support_simple_spinner_dropdown_item, roleNames);
                spnRole.Adapter = roleAdapter;
            }
            else
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetTitle("Rol bilgileri alınırken hata oluştu")
                    .SetMessage(result.Message)
                    .SetNegativeButton("TAMAM", (IDialogInterfaceOnClickListener)null)
                    .Create().Show();
            }

        }

        private async void SignupAsync()
        {
            RegisterModel model = new RegisterModel
            {
                Name = etName.Text,
                Password = etPassword.Text,
                RoleId = roles[spnRole.SelectedItemPosition].RoleId,
                Surname = etSurname.Text,
                UserId = -1,
                UserName = etUsername.Text
            };
            var result = await Constants.ServiceManager.RegisterAsync(model);
            if (result != null && result.OpCode == 0)
            {
                result.Result = "Kullanıcı oluşturuldu";
            }
            var builder = new AlertDialog.Builder(this);
            builder.SetTitle(result.Message)
                .SetMessage(result.Result.ToString())
                .SetNegativeButton("TAMAM", (IDialogInterfaceOnClickListener)null)
                .Create().Show();
        }
        private bool ValidateForm()
        {
            bool isValid = true;
            for (int i = 0; i < editTexts.Length; i++)
            {
                if (!Validation(editTexts[i], layouts[i]))
                {
                    isValid = false;
                }
            }
            if (spnRole.SelectedItemPosition == -1)
            {
                isValid = false;
                tilRole.Error = "Seçim yapınız";
            }
            else
            {
                tilRole.Error = string.Empty;
            }
            return isValid;
        }
        private bool Validation(TextInputEditText editText, TextInputLayout layout)
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(editText.Text))
            {
                layout.Error = "*Bol olamaz";
                isValid = false;
            }
            else
            {
                layout.Error = string.Empty;
            }
            return isValid;
        }
    }
}