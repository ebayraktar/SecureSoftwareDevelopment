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

        public virtual bool Get<T>(out object resultData) where T : new()
        {
            return service.Get<T>(out resultData);
        }
        public virtual bool Get<T>(string id, out object resultData) where T : new()
        {
            {
                return service.Get<T>(id, out resultData);
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
        public virtual bool Delete<T>(out object resultData)
        {
            return service.Delete<T>(out resultData);
        }
        public virtual bool Delete<T>(string id, out object resultData)
        {
            return service.Delete<T>(id, out resultData);
        }
    }
}