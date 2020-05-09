using SSDWebService.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace SSDWebService.REST.Services
{
    public class AuthorService : BaseService
    {
        public override bool Post(object data, out object resultData)
        {
            try
            {
                var author = Newtonsoft.Json.JsonConvert.DeserializeObject<Authors>(data.ToString());
                if (author != null)
                {
                    string lastID = Constants.Connection.Table<Authors>().OrderByDescending(x => x.AuthorId).Select(y => y.AuthorId).FirstOrDefault();
                    if (int.TryParse(lastID, out int idCount))
                    {
                        lastID = (++idCount).ToString();
                    }
                    else
                    {
                        lastID = "0";
                    }
                    resultData = author.AuthorId = lastID;
                    return Constants.Connection.Insert(author) > 0 ? true : false;
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
                var tempAuthor = Constants.Connection.Find<Authors>(id);
                var author = Newtonsoft.Json.JsonConvert.DeserializeObject<Authors>(data.ToString());
                if (author != null && tempAuthor != null)
                {
                    resultData = author;
                    tempAuthor = author;
                    return Constants.Connection.Update(author) > 0 ? true : false;
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