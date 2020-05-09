using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SSDMobileApp.Models
{
    public class Books
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int Pagecount { get; set; }
        public int Point { get; set; }
        public int AuthorId { get; set; }
        public int TypeId { get; set; }
        public bool IsFavorite { get; set; }
    }
}