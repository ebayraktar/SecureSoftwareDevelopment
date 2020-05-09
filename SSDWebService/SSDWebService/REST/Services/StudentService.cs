using SSDWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSDWebService.REST.Services
{
    public class StudentService : BaseService
    {
        public override bool Post(object data, out object resultData)
        {
            try
            {
                var Student = Newtonsoft.Json.JsonConvert.DeserializeObject<Students>(data.ToString());
                if (Student != null)
                {
                    string lastID = Constants.Connection.Table<Students>().OrderByDescending(x => x.StudentId).Select(y => y.StudentId).FirstOrDefault();
                    if (int.TryParse(lastID, out int idCount))
                    {
                        lastID = (++idCount).ToString();
                    }
                    else
                    {
                        lastID = "0";
                    }
                    resultData = Student.StudentId = lastID;
                    return Constants.Connection.Insert(Student) > 0 ? true : false;
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
                var tempStudent = Constants.Connection.Find<Students>(id);
                var student = Newtonsoft.Json.JsonConvert.DeserializeObject<Students>(data.ToString());
                if (student != null && tempStudent != null)
                {
                    resultData = student;
                    tempStudent = student;
                    return Constants.Connection.Update(student) > 0 ? true : false;
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