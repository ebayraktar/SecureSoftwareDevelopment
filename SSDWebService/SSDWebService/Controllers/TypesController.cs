using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;


namespace SSDWebService.Controllers
{
    public class TypesController : BaseController<Types>
    {
        public TypesController() : this(new BaseManager(new BaseService()))
        {

        }

        public TypesController(BaseManager manager) : base(manager)
        {

        }
    }
}
