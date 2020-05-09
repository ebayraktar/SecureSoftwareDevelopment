using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class BorrowService : BaseService
    {
        public override bool Post(object data, out object resultData)
        {
            try
            {
                var borrow = Newtonsoft.Json.JsonConvert.DeserializeObject<Borrows>(data.ToString());
                if (borrow != null)
                {
                    string lastID = Constants.Connection.Table<Borrows>().OrderByDescending(x => x.BorrowId).Select(y => y.BorrowId).FirstOrDefault();
                    if (int.TryParse(lastID, out int idCount))
                    {
                        lastID = (++idCount).ToString();
                    }
                    else
                    {
                        lastID = "0";
                    }
                    resultData = borrow.BorrowId = lastID;
                    return Constants.Connection.Insert(borrow) > 0 ? true : false;
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
                var tempBorrow = Constants.Connection.Find<Borrows>(id);
                var borrow = Newtonsoft.Json.JsonConvert.DeserializeObject<Borrows>(data.ToString());
                if (borrow != null && tempBorrow != null)
                {
                    resultData = borrow;
                    tempBorrow = borrow;
                    return Constants.Connection.Update(borrow) > 0 ? true : false;
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