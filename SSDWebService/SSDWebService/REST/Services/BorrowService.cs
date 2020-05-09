using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class BorrowService : BaseService
    {
        public override bool Get<T>(string id, out object resultData)
        {
            try
            {
                var borrows = Constants.Connection.Table<Borrows>().Where(x => x.BookId.Equals(id)).ToList();
                List<BookBorrows> tempList = new List<BookBorrows>();
                if (borrows != null && borrows.Count > 0)
                {
                    foreach (var borrow in borrows)
                    {
                        string tempStudent = Constants.Connection.Table<Students>().Where(x => x.StudentId.Equals(borrow.StudentId)).Select(x => x.Name + " " + x.Surname).FirstOrDefault();
                        tempList.Add(new BookBorrows { StudentName = tempStudent, BroughtDate = borrow.BroughtDate, TakenDate = borrow.TakenDate });
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