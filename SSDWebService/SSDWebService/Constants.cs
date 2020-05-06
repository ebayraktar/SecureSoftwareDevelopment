using SQLite;

namespace SSDWebService
{
    public static class Constants
    {
        public static SQLiteConnection Connection = DBConnection.instance.connection;
    }
}