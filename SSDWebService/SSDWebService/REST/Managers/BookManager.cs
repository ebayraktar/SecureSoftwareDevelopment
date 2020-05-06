using SSDWebService.REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Managers
{
    public class BookManager
    {
        private readonly IBook service;
        public BookManager(IBook service)
        {
            this.service = service;
        }

        public bool Get(out object resultData)
        {
            return service.Get(out resultData);
        }
        public bool Get(string id, out object resultData)
        {
            {
                return service.Get(id, out resultData);
            }
        }
        public bool Post(object data, out object resultData)
        {
            return service.Post(data, out resultData);
        }
        public bool Put(string id, object data, out object resultData)
        {
            return service.Put(id, data, out resultData);
        }
        public bool Delete(out object resultData)
        {
            return service.Delete(out resultData);
        }
        public bool Delete(string id, out object resultData)
        {
            return service.Delete(id, out resultData);
        }

    }
}