using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;

namespace SSDWebService.Controllers
{
    public class BorrowsController : BaseController<Borrows>
    {
        public BorrowsController() : this(new BaseManager(new BaseService()))
        {

        }

        public BorrowsController(BaseManager manager) : base(manager)
        {

        }
    }
}
