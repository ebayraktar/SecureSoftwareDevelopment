using System.Threading.Tasks;
using SSDMobileApp.Models;

namespace SSDMobileApp.REST
{
    public class RestManager
    {
        readonly IRest service;
        public RestManager(IRest service)
        {
            this.service = service;
        }
        public async Task<MobileResult> RegisterAsync(RegisterModel data)
        {
            return await service.RegisterAsync(data);
        }
        public async Task<MobileResult> LoginAsync(LoginModel data)
        {
            return await service.LoginAsync(data);
        }
        public async Task<MobileResult> Requests(string id = "")
        {
            return await service.Requests(id);
        }
        public async Task<MobileResult> Roles(string id = "")
        {
            return await service.Roles(id);
        }
        public async Task<MobileResult> Authors(string id = "")
        {
            return await service.Authors(id);
        }
        public async Task<MobileResult> Books(string id = "")
        {
            return await service.Books(id);
        }

        public async Task<MobileResult> Borrows(string id = "")
        {
            return await service.Borrows(id);
        }

        public async Task<MobileResult> Students(string id = "")
        {
            return await service.Students(id);
        }
        public async Task<MobileResult> Types(string id = "")
        {
            return await service.Types(id);
        }
        public async Task<MobileResult> Users(string id = "")
        {
            return await service.Users(id);
        }
    }
}