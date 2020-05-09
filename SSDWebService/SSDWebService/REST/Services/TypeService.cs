using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class TypeService : BaseService
    {
        public override bool Post(object data, out object resultData)
        {
            try
            {
                var type = Newtonsoft.Json.JsonConvert.DeserializeObject<Types>(data.ToString());
                if (type != null)
                {
                    string lastID = Constants.Connection.Table<Types>().OrderByDescending(x => x.TypeId).Select(y => y.TypeId).FirstOrDefault();
                    if (int.TryParse(lastID, out int idCount))
                    {
                        lastID = (++idCount).ToString();
                    }
                    else
                    {
                        lastID = "0";
                    }
                    resultData = type.TypeId = lastID;
                    return Constants.Connection.Insert(type) > 0 ? true : false;
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
                var tempType = Constants.Connection.Find<Types>(id);
                var type = Newtonsoft.Json.JsonConvert.DeserializeObject<Types>(data.ToString());
                if (type != null && tempType != null)
                {
                    resultData = type;
                    tempType = type;
                    return Constants.Connection.Update(type) > 0 ? true : false;
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