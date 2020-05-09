using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;

namespace SSDWebService.Controllers
{
    public class StudentsController : BaseController<Students>
    {
        public StudentsController() : this(new BaseManager(new BaseService()))
        {

        }

        public StudentsController(BaseManager manager) : base(manager)
        {

        }
    }
}
