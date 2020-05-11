using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;


namespace SSDWebService.Controllers
{
    public class UsersController : BaseController<Users>
    {
        public UsersController() : this(new UserManager(new UserService()))
        {

        }

        public UsersController(BaseManager manager) : base(manager)
        {

        }
    }
}
