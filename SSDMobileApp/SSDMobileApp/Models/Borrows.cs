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
    public class Borrows
    {

        public string BorrowId { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
    }
}