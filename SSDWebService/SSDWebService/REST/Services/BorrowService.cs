using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class BorrowService : BaseService
    {
        public override bool Delete(out object resultData)
        {
            return base.Delete(out resultData);
        }

        public override bool Delete(string id, out object resultData)
        {
            return base.Delete(id, out resultData);
        }

        public override bool Get(out object resultData)
        {
            return base.Get(out resultData);
        }

        public override bool Get(string id, out object resultData)
        {
            return base.Get(id, out resultData);
        }

        public override bool Post(object data, out object resultData)
        {
            return base.Post(data, out resultData);
        }

        public override bool Put(string id, object data, out object resultData)
        {
            return base.Put(id, data, out resultData);
        }
    }
}