using SQLite;
using SSDWebService.Models;
using System;
using System.IO;

namespace SSDWebService
{
    public class DBConnection
    {
        public static DBConnection instance;

        private readonly string DBNAME = "Library.db3";
        public SQLiteConnection connection;
        public DBConnection()
        {
            instance = this;
            if (connection == null)
            {
                connection = new SQLiteConnection(Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DBNAME));
                //connection.CreateTable<books>();
            }
        }

    }
}