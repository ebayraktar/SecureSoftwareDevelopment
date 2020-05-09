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
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Token { get; set; }
    }
}