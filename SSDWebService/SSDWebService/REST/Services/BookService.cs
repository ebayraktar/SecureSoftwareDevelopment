using SSDWebService.Models;
using SSDWebService.REST.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SSDWebService.REST.Services
{
    public class BookService : IBook
    {
        public bool Delete(out object resultData)
        {
            try
            {
                resultData = Constants.Connection.DeleteAll<books>();
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public bool Delete(string id, out object resultData)
        {
            try
            {
                resultData = Constants.Connection.Delete<books>(id);
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public bool Get(out object resultData)
        {
            try
            {
                resultData = Constants.Connection.Table<books>();
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public bool Get(string id, out object resultData)
        {
            try
            {

                //resultData = Constants.Connection.Table<Book>().Where(x => x.ID.Equals(id)).FirstOrDefault();
                //SQL INJECTION
                /*
                 http://localhost:64215/apiv1/Books/a' OR 1=1--
                 */
                string query = $"SELECT * FROM BOOK WHERE ID='" + id + "'";
                resultData = Constants.Connection.Query<books>(query);
                return true;
            }
            catch (Exception ex)
            {
                resultData = ex.Message;
                return false;
            }
        }

        public bool Post(object data, out object resultData)
        {
            try
            {
                //if (data is Book book)
                //{
                //    resultData = book.ID = Guid.NewGuid().ToString();
                //    return Constants.Connection.Insert(book) > 0 ? true : false;
                //}
                //resultData = "invalid argument: " + data;
                //return false;
                if (data is books book)
                {
                    //INSERT INTO BOOK VALUES ('a', 'A','A','A')
                    resultData = book.bookId = Guid.NewGuid().ToString();
                    string query = $"INSERT INTO BOOK VALUES('{book.bookId}','{book.name}','{book.pagecount}','{book.point}')";
                    //var result = Constants.Connection.Query<Book>(query);
                    //return result.Count > 0 ? true : false;
                    var cmd = Constants.Connection.CreateCommand(query);
                    var dat = cmd.ExecuteQuery<books>();//.ExecuteNonQuery();
                    return dat.Count > 0 ? true : false;
                    //return Constants.Connection.Execute(query) > 0 ? true : false;
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

        public bool Put(string id, object data, out object resultData)
        {
            try
            {
                var tempBook = Constants.Connection.Find<books>(id);
                if (data is books book && tempBook != null)
                {
                    resultData = book;
                    tempBook = book;
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