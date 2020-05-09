using SSDWebService.Models;
using System.Web.Http;
using System.Web.Mvc;

namespace SSDWebService.Controllers
{
    [System.Web.Http.AllowAnonymous]
    [ValidateAntiForgeryToken]
    public class LoginController : ApiController
    {
        private ResponseModel responseModel;
        public LoginController()
        {
            responseModel = new ResponseModel
            {
                OpCode = -1,
                Message = "Başarısız"
            };
        }

        public ResponseModel Post([FromBody]LoginModel loginModel)
        {

            if (loginModel == null)
            {
                responseModel.Result = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                return responseModel;
            }
            string password = Cryptohraphy.Crypthography.EncrypteData(loginModel.Password);
            var user = Constants.Connection.Table<Users>().Where(x => x.UserName.Equals(loginModel.Username) & x.Password.Equals(password)).FirstOrDefault();
            if (user != null)
            {
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(loginModel);
                LoginResponseModel lrModel = new LoginResponseModel
                {
                    UserId = user.UserId,
                    RoleId = user.RoleId,
                    Token = GenerateToken(jsonString)
                };
                responseModel.Result = Newtonsoft.Json.JsonConvert.SerializeObject(lrModel);
                responseModel.Message = "Başarılı";
                responseModel.OpCode = 0;
            }
            return responseModel;
        }



        private string GenerateToken(string plainText) => FTH.Extension.Encrypter.Encrypt(plainText, Constants.FTHPassword);
    }
}
