using SSDWebService.Models;
using System;
using System.Linq;

namespace SSDWebService.REST.Services
{
    public class BookService : BaseService
    {
        public override bool Post(object data, out object resultData)
        {
            try
            {
                var book = Newtonsoft.Json.JsonConvert.DeserializeObject<Books>(data.ToString());
                if (book != null)
                {
                    //INSERT INTO BOOK VALUES ('a', 'A','A','A')
                    string lastID = Constants.Connection.Table<Books>().OrderByDescending(x => x.BookId).Select(y => y.BookId).FirstOrDefault();
                    if (int.TryParse(lastID, out int idCount))
                    {
                        lastID = (++idCount).ToString();
                    }
                    else
                    {
                        lastID = "0";
                    }
                    resultData = book.BookId = lastID;
                    string query = $"INSERT INTO books VALUES('{book.BookId}','{book.Name}','{book.Pagecount}','{book.Point}','{book.AuthorId}','{book.TypeId}')";
                    var cmd = Constants.Connection.CreateCommand(query);
                    return Constants.Connection.Execute(query) > 0 ? true : false;
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
                var tempBook = Constants.Connection.Find<Books>(id);
                var book = Newtonsoft.Json.JsonConvert.DeserializeObject<Books>(data.ToString());
                if (book != null && tempBook != null)
                {
                    resultData = book;
                    book.BookId = tempBook.BookId;
                    //tempBook = book;
                    return Constants.Connection.Update(book) > 0 ? true : false;
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