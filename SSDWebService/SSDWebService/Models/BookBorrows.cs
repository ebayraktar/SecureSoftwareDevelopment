using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.Models
{
    public class BookBorrows
    {
        public string StudentName { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
    }
}