using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;

namespace SSDWebService.Controllers
{
    public class RequestsController : BaseController<Requests>
    {
        public RequestsController() : this(new RequestManager(new RequestService()))
        {

        }
        public RequestsController(BaseManager manager) : base(manager)
        {

        }
    }
}
