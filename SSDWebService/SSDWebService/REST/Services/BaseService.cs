using SSDWebService.REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class BaseService : IBase
    {
        public virtual bool Delete<T>(out object resultData)
        {
            try
            {
                resultData = Constants.Connection.DeleteAll<T>();
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public virtual bool Delete<T>(string id, out object resultData)
        {
            try
            {
                resultData = Constants.Connection.Delete<T>(id);
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public virtual bool Get<T>(out object resultData) where T : new()
        {
            try
            {
                resultData = Constants.Connection.Table<T>();
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }


        public virtual bool Get<T>(string id, out object resultData) where T : new()
        {
            try
            {
                var ty = typeof(T);
                string query = $"SELECT * FROM {ty.Name} WHERE {ty.Name.Remove(ty.Name.Length - 1, 1)}Id='" + id + "'";
                resultData = Constants.Connection.Query<T>(query);
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }


        public virtual bool Post(object data, out object resultData)
        {
            throw new NotImplementedException();
        }

        public virtual bool Put(string id, object data, out object resultData)
        {
            throw new NotImplementedException();
        }
    }
}