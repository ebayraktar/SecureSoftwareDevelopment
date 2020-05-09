using SSDWebService.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;

namespace SSDWebService.Controllers
{
    [System.Web.Http.AllowAnonymous]
    [ValidateAntiForgeryToken]
    public class RegisterController : ApiController
    {
        private ResponseModel responseModel;
        public RegisterController()
        {
            responseModel = new ResponseModel
            {
                OpCode = -1,
                Message = "Başarısız"
            };
        }

        public ResponseModel Post([FromBody]RegisterModel registerModel)
        {
            if (registerModel == null)
            {
                responseModel.Result = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                return responseModel;
            }
            var user = Constants.Connection.Table<Users>().Where(x => x.UserName.Equals(registerModel.UserName)).FirstOrDefault();
            if (user != null)
            {
                responseModel.Result = "Kullanıcı zaten mevcut";
            }
            else
            {
                int lastID = Constants.Connection.Table<Users>().OrderByDescending(x => x.UserId).Select(y => y.UserId).FirstOrDefault() + 1;
                responseModel.Result = registerModel.UserId = lastID;
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(registerModel);
                Users tempUser = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(json);
                tempUser.Password = Cryptohraphy.Crypthography.EncrypteData(tempUser.Password);
                Constants.Connection.Insert(tempUser);

                responseModel.Message = "Başarılı";
                responseModel.OpCode = 0;
            }
            return responseModel;
        }
    }
}