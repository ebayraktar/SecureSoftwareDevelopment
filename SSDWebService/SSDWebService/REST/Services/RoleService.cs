using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class RoleService : BaseService
    {
        public override bool Post(object data, out object resultData)
        {
            try
            {
                var role = Newtonsoft.Json.JsonConvert.DeserializeObject<Roles>(data.ToString());
                if (role != null)
                {
                    string lastID = Constants.Connection.Table<Roles>().OrderByDescending(x => x.RoleId).Select(y => y.RoleId).FirstOrDefault();
                    if (int.TryParse(lastID, out int idCount))
                    {
                        lastID = (++idCount).ToString();
                    }
                    else
                    {
                        lastID = "0";
                    }
                    resultData = role.RoleId = lastID;
                    return Constants.Connection.Insert(role) > 0 ? true : false;
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
                var tempRole = Constants.Connection.Find<Roles>(id);
                var role = Newtonsoft.Json.JsonConvert.DeserializeObject<Roles>(data.ToString());
                if (role != null && tempRole != null)
                {
                    resultData = role;
                    tempRole = role;
                    return Constants.Connection.Update(role) > 0 ? true : false;
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