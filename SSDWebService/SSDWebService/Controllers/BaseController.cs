using SSDWebService.Models;
using SSDWebService.REST.Managers;
using SSDWebService.REST.Services;
using System.Web.Http;

namespace SSDWebService.Controllers
{
    public class BaseController : ApiController
    {
        private readonly BaseManager manager;
        private readonly ResponseModel responseModel;
        public BaseController(BaseManager manager)
        {
            if (manager == null)
            {
                this.manager = new BaseManager(new BaseService());
            }
            else
            {
                this.manager = manager;
            }
            responseModel = new ResponseModel
            {
                OpCode = -1,
                Message = "Başarısız"
            };
        }

        // GET api/books
        public ResponseModel Get()
        {
            if (manager.Get(out object resultData))
            {
                responseModel.OpCode = 0;
                responseModel.Message = "Başarılı";
            }
            responseModel.Result = resultData;
            return responseModel;
            //Constants.Connection.Table<Book>();
        }

        // GET api/books/5
        public ResponseModel Get(string id)
        {
            if (manager.Get(id, out object resultData))
            {
                responseModel.OpCode = 0;
                responseModel.Message = "Başarılı";
            }
            responseModel.Result = resultData;
            return responseModel;
            //return Constants.Connection.Table<Book>().Where(x => x.ID.Equals(id)).FirstOrDefault();
        }

        // POST api/books
        public ResponseModel Post([FromBody]object data)
        {
            if (manager.Post(data, out object resultData))
            {
                responseModel.OpCode = 0;
                responseModel.Message = "Başarılı";
            }
            responseModel.Result = resultData;
            return responseModel;
            //Constants.Connection.InsertOrReplace(data);
        }

        // PUT api/books/5
        public ResponseModel Put(string id, [FromBody]object data)
        {
            if (manager.Put(id, data, out object resultData))
            {
                responseModel.OpCode = 0;
                responseModel.Message = "Başarılı";
            }
            responseModel.Result = resultData;
            return responseModel;
            //var tempBook = Constants.Connection.Table<Book>().Where(x => x.ID.Equals(id)).FirstOrDefault();
            //tempBook = data;
            //if (tempBook != null)
            //{
            //    Constants.Connection.InsertOrReplace(tempBook);
            //}
        }

        // DELETE api/books
        public ResponseModel Delete()
        {
            if (manager.Delete(out object resultData))
            {
                responseModel.OpCode = 0;
                responseModel.Message = "Başarılı";
            }
            responseModel.Result = resultData;
            return responseModel;
            //Constants.Connection.Delete<Book>(id);
        }

        // DELETE api/books/5
        public ResponseModel Delete(string id)
        {
            if (manager.Delete(id, out object resultData))
            {
                responseModel.OpCode = 0;
                responseModel.Message = "Başarılı";
            }
            responseModel.Result = resultData;
            return responseModel;
            //Constants.Connection.Delete<Book>(id);
        }
    }
}
