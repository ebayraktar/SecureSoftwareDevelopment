using SSDWebService.REST.Interfaces;

namespace SSDWebService.REST.Managers
{
    public class BaseManager
    {
        private readonly IBase service;
        public BaseManager(IBase service)
        {
            this.service = service;
        }

        public virtual bool Get(out object resultData)
        {
            return service.Get(out resultData);
        }
        public virtual bool Get(string id, out object resultData)
        {
            {
                return service.Get(id, out resultData);
            }
        }
        public virtual bool Post(object data, out object resultData)
        {
            return service.Post(data, out resultData);
        }
        public virtual bool Put(string id, object data, out object resultData)
        {
            return service.Put(id, data, out resultData);
        }
        public virtual bool Delete(out object resultData)
        {
            return service.Delete(out resultData);
        }
        public virtual bool Delete(string id, out object resultData)
        {
            return service.Delete(id, out resultData);
        }
    }
}