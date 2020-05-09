using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.Models
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Token { get; set; }
    }
}