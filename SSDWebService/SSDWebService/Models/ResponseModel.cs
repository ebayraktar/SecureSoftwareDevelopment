using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.Models
{
    public class ResponseModel
    {
        public int OpCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}