using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class UserService : BaseService
    {
        public override bool Post(object data, out object resultData)
        {
            try
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(data.ToString());
                if (user != null)
                {
                    int lastID = Constants.Connection.Table<Users>().OrderByDescending(x => x.UserId).Select(y => y.UserId).FirstOrDefault() + 1;
                    resultData = user.UserId = lastID;
                    return Constants.Connection.Insert(user) > 0 ? true : false;
                }
                resultData = "invalid argument: " + data;
                return false;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public override bool Put(string id, object data, out object resultData)
        {
            try
            {
                var tempUser = Constants.Connection.Find<Users>(id);
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<Users>(data.ToString());
                if (user != null && tempUser != null)
                {
                    resultData = user;
                    tempUser = user;
                    return Constants.Connection.Update(user) > 0 ? true : false;
                }

                resultData = "invalid argument: " + data;
                return false;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }
    }
}