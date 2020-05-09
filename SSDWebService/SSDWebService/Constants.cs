using SQLite;

namespace SSDWebService
{
    public static class Constants
    {
        public static string FTHPassword = "_SSDWebServicePassword";
        public static SQLiteConnection Connection = DBConnection.instance.connection;
    }
}