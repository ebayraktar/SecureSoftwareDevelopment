using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;

namespace SSDWebService.Controllers
{
    public class AuthorsController : BaseController<Authors>
    {
        public AuthorsController() : this(new BaseManager(new BaseService()))
        {

        }
        public AuthorsController(BaseManager manager) : base(manager)
        {

        }
    }
}
