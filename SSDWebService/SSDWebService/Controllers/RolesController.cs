using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;

namespace SSDWebService.Controllers
{
    public class RolesController : BaseController<Roles>
    {
        public RolesController() : this(new BaseManager(new BaseService()))
        {

        }

        public RolesController(BaseManager manager) : base(manager)
        {

        }
    }
}
