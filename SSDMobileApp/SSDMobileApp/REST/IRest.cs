using System.Threading.Tasks;
using SSDMobileApp.Models;

namespace SSDMobileApp.REST
{
    public interface IRest
    {
        Task<MobileResult> RegisterAsync(RegisterModel data);
        Task<MobileResult> LoginAsync(LoginModel data);
        Task<MobileResult> LoginAsStudentAsync(LoginModel data);
        Task<MobileResult> Authors(string id = "");
        Task<MobileResult> Books(string id = "");
        Task<MobileResult> Books(Books data, string id = "");
        Task<MobileResult> Borrows(string id = "");
        Task<MobileResult> Roles(string id = "");
        Task<MobileResult> Students(string id = "");
        Task<MobileResult> Types(string id = "");
        Task<MobileResult> Users(string id = "");
        Task<MobileResult> Users(Users data, string id = "");
        Task<MobileResult> Requests(string id = "");
        Task<MobileResult> Requests(Requests data, string id = "");
    }
}