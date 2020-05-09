using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace SSDWebService.Controllers
{
    [System.Web.Http.AllowAnonymous]
    [ValidateAntiForgeryToken]
    public class BooksController : BaseController<Books>
    {
        //BookManager manager;
        public BooksController() : this(new BookManager(new BookService()))
        {
            //manager = new BookManager(new BookService());
            //base manager;
        }
        public BooksController(BaseManager manager) : base(manager)
        {

        }

        //private void SetManager() 
        //{
        //    this(manager);
        //}

        //[System.Web.Http.Route("books/{bookId}/borrows")]
        //public ResponseModel GetBorrowsByBook(string bookId)
        //{
        //    if (manager.Get<T>(id, out object resultData))
        //    {
        //        responseModel.OpCode = 0;
        //        responseModel.Message = "Başarılı";
        //    }
        //    responseModel.Result = resultData;
        //    return responseModel;
        //    //return Constants.Connection.Table<Book>().Where(x => x.ID.Equals(id)).FirstOrDefault();
        //}

    }
}
