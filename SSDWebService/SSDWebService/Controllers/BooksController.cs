using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;
using System.Web.Http;

namespace SSDWebService.Controllers
{
    public class BooksController : BaseController
    {
        public BooksController() : this(new BookManager(new BookService()))
        {

        }
        public BooksController(BaseManager manager) : base(manager)
        {

        }
    }
}
