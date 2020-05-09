using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace SSDWebService.REST.Services
{
    public class RequestService : BaseService
    {
        public override bool Get<T>(out object resultData)
        {
            try
            {
                var requests = Constants.Connection.Table<Requests>().ToList();
                List<BookRequests> tempList = new List<BookRequests>();
                if (requests != null && requests.Count > 0)
                {
                    foreach (var request in requests)
                    {
                        string tempBook = Constants.Connection.Table<Books>().Where(x => x.BookId.Equals(request.BookId)).Select(x => x.Name).FirstOrDefault();
                        string tempStudent = Constants.Connection.Table<Students>().Where(x => x.StudentId.Equals(request.StudentId)).Select(x => x.Name + " " + x.Surname).FirstOrDefault();
                        tempList.Add(new BookRequests { RequestId = request.RequestId, BookName = tempBook, StudentName = tempStudent, RequestDate = request.RequestDate, Statu = request.Statu.ToString() });
                    }
                }
                resultData = tempList;
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }
        public override bool Get<T>(string id, out object resultData)
        {
            try
            {
                List<BookRequests> tempList = new List<BookRequests>();
                var requests = Constants.Connection.Table<Requests>().Where(x => x.StudentId.Equals(id)).ToList();
                if (requests != null && requests.Count > 0)
                {
                    foreach (var request in requests)
                    {
                        string tempBook = Constants.Connection.Table<Books>().Where(x => x.BookId.Equals(request.BookId)).Select(x => x.Name).FirstOrDefault();
                        string tempStudent = Constants.Connection.Table<Students>().Where(x => x.StudentId.Equals(request.StudentId)).Select(x => x.Name + " " + x.Surname).FirstOrDefault();
                        tempList.Add(new BookRequests { RequestId = request.RequestId, BookName = tempBook, StudentName = tempStudent, RequestDate = request.RequestDate, Statu = request.Statu == 0 ? "Bekliyor" : request.Statu == 1 ? "Kabul Edildi" : "Reddedildi" });
                    }
                }
                resultData = tempList;
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public override bool Post(object data, out object resultData)
        {
            try
            {
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<Requests>(data.ToString());
                if (request != null)
                {
                    int lastID = Constants.Connection.Table<Requests>().OrderByDescending(x => x.RequestId).Select(y => y.RequestId).FirstOrDefault();
                    resultData = request.RequestId = ++lastID;
                    return Constants.Connection.Insert(request) > 0 ? true : false;
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
                var tempRequest = Constants.Connection.Find<Requests>(id);
                var request = Newtonsoft.Json.JsonConvert.DeserializeObject<Requests>(data.ToString());
                if (request != null && tempRequest != null)
                {
                    resultData = request;
                    tempRequest.Statu = request.Statu;
                    return Constants.Connection.Update(tempRequest) > 0 ? true : false;
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