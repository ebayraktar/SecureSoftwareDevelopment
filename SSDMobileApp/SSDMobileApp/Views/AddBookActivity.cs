
using Android.App;
using Android.OS;
using Android.Service.Carrier;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AndroidX.CardView.Widget;
using Google.Android.Material.TextField;
using Org.Apache.Http.Authentication;
using SSDMobileApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace SSDMobileApp.Views
{
    [Activity(Label = "Kitap Ekle", Theme = "@style/AppTheme.NoActionBar")]
    public class AddBookActivity : AppCompatActivity
    {
        TextInputLayout tilBookName, tilPageCount, tilPoint;
        TextInputEditText etBookName, etPageCount, etPoint;

        TextInputLayout tilAuthor, tilType;
        Spinner spnAuthor, spnType;

        TextInputEditText[] editTexts;
        TextInputLayout[] layouts;

        List<Authors> authors;
        List<Types> types;

        Books currentBook;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_book);
            Toolbar myToolbar = (Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(myToolbar);

            // Get a support ActionBar corresponding to this toolbar
            Android.Support.V7.App.ActionBar ab = SupportActionBar;

            // Enable the Up button
            ab.SetDisplayHomeAsUpEnabled(true);

            tilBookName = FindViewById<TextInputLayout>(Resource.Id.tilBookName);
            tilPageCount = FindViewById<TextInputLayout>(Resource.Id.tilPageCount);
            tilPoint = FindViewById<TextInputLayout>(Resource.Id.tilPoint);

            etBookName = FindViewById<TextInputEditText>(Resource.Id.etBookName);
            etPageCount = FindViewById<TextInputEditText>(Resource.Id.etPageCount);
            etPoint = FindViewById<TextInputEditText>(Resource.Id.etPoint);

            tilAuthor = FindViewById<TextInputLayout>(Resource.Id.tilAuthor);
            tilType = FindViewById<TextInputLayout>(Resource.Id.tilType);

            spnAuthor = FindViewById<Spinner>(Resource.Id.spnAuthor);
            spnType = FindViewById<Spinner>(Resource.Id.spnType);

            editTexts = new TextInputEditText[] { etBookName, etPageCount, etPoint };
            layouts = new TextInputLayout[] { tilBookName, tilPageCount, tilPoint };

            Initialize();

            etBookName.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    Validation(etBookName, tilBookName);
                }
            };
            etPageCount.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    Validation(etPageCount, tilPageCount);
                }
            };
            etPoint.FocusChange += (s, e) =>
            {
                if (!e.HasFocus)
                {
                    Validation(etPoint, tilPoint);
                }
            };


            FindViewById<CardView>(Resource.Id.cvAdd).Click += (s, e) =>
            {
                if (ValidateForm())
                {
                    AddBookAsync();
                }
            };

            var json = Intent.GetStringExtra(Constants.BOOK_EXTRA);
            if (string.IsNullOrEmpty(json))
                return;
            var book = Newtonsoft.Json.JsonConvert.DeserializeObject<Books>(json);
            if (book != null)
            {
                currentBook = book;
                InitBook();
            }
        }

        private async void Initialize()
        {
            authors = await GetAuthors();
            if (authors != null)
            {
                List<string> names = authors.Select(x => x.Name + " " + x.Surname).ToList();
                spnAuthor.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, names);
            }

            types = await GetTypes();
            if (types != null)
            {
                List<string> names = types.Select(x => x.Name).ToList();
                spnType.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, names);
            }
        }

        private void InitBook()
        {
            etBookName.Text = currentBook.Name;
            etPageCount.Text = currentBook.Pagecount.ToString();
            etPoint.Text = currentBook.Point.ToString();
            spnAuthor.SetSelection(currentBook.AuthorId);
            spnType.SetSelection(currentBook.TypeId);
        }

        private async void AddBookAsync()
        {
            Books tempBook = new Books
            {
                Name = etBookName.Text,
                Pagecount = int.Parse(etPageCount.Text),
                Point = int.Parse(etPoint.Text),
                AuthorId = authors[spnAuthor.SelectedItemPosition].AuthorId,
                TypeId = types[spnType.SelectedItemPosition].TypeId

            };
            string message = "Hata oluştu";
            var result = await Constants.ServiceManager.Books(tempBook, currentBook?.BookId.ToString());
            if (result != null && result.OpCode == 0)
            {
                message = "Kitap başarıyla oluşturuldu";
                Clear();
            }

            Android.App.AlertDialog dialog = new Android.App.AlertDialog.Builder(this)
                .SetTitle(result.Message)
                .SetMessage(message)
                .SetIcon(Android.Resource.Drawable.IcDialogAlert)
                .SetNegativeButton("TAMAM", (Android.Content.IDialogInterfaceOnClickListener)null)
                .Create();

            dialog.Show();
        }

        private void Clear()
        {
            etBookName.Text = string.Empty;
            etPageCount.Text = string.Empty;
            etPoint.Text = string.Empty;
            spnAuthor.SetSelection(0);
            spnType.SetSelection(0);
            etBookName.RequestFocus();
        }

        async Task<List<Authors>> GetAuthors()
        {
            List<Authors> tempAuthors = null;
            var result = await Constants.ServiceManager.Authors();
            if (result != null && result.OpCode == 0)
            {
                tempAuthors = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Authors>>(result.Result.ToString());
            }
            return tempAuthors;
        }

        async Task<List<Types>> GetTypes()
        {
            List<Types> tempTypes = null;
            var result = await Constants.ServiceManager.Types();
            if (result != null && result.OpCode == 0)
            {
                tempTypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Types>>(result.Result.ToString());
            }
            return tempTypes;
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
            if (spnAuthor.SelectedItemPosition == -1)
            {
                isValid = false;
                tilAuthor.Error = "Seçim yapınız";
            }
            else
            {
                tilAuthor.Error = string.Empty;
            }

            if (spnType.SelectedItemPosition == -1)
            {
                isValid = false;
                tilType.Error = "Seçim yapınız";
            }
            else
            {
                tilType.Error = string.Empty;
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