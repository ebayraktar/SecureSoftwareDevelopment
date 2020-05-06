using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSDWebService.REST.Interfaces
{
    public interface IBase
    {
        bool Get(out object resultData);
        bool Get(string id, out object resultData);
        bool Post(object data, out object resultData);
        bool Put(string id, object data, out object resultData);
        bool Delete(out object resultData);
        bool Delete(string id, out object resultData);
    }
}
