using SSDWebService.REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class BaseService : IBase
    {
        public virtual bool Delete(out object resultData)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(string id, out object resultData)
        {
            throw new NotImplementedException();
        }

        public virtual bool Get(out object resultData)
        {
            throw new NotImplementedException();
        }

        public virtual bool Get(string id, out object resultData)
        {
            throw new NotImplementedException();
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