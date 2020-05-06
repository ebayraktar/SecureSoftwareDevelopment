using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSDWebService.Controllers
{
    public class BorrowsController : BaseController
    {
        public BorrowsController() : this(new BaseManager(new BaseService()))
        {

        }

        public BorrowsController(BaseManager manager) : base(manager)
        {

        }
    }
}
