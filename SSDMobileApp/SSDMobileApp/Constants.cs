using SSDMobileApp.REST;

namespace SSDMobileApp
{
    public class Constants
    {
        public Constants()
        {
            if (ServiceManager == null)
            {
                ServiceManager = new RestManager(new RestService());
            }
        }
        private static readonly string HOST = "http://10.0.2.2:57571/";
        private static readonly string APIADDRESS = "api/v1.0/";
        private static string BASEADDRESS = HOST + APIADDRESS;

        public static readonly string LOGIN_URL = BASEADDRESS + "login";
        public static readonly string LOGINSTUDENT_URL = BASEADDRESS + "login/student";
        public static readonly string REGISTER_URL = BASEADDRESS + "register";
        public static readonly string REQUESTS_URL = BASEADDRESS + "requests/";
        public static readonly string ROLES_URL = BASEADDRESS + "roles/";
        public static readonly string AUTHORS_URL = BASEADDRESS + "authors/";
        public static readonly string BOOKS_URL = BASEADDRESS + "books/";
        public static readonly string BORROWS_URL = BASEADDRESS + "borrows/";
        public static readonly string STUDENTS_URL = BASEADDRESS + "students/";
        public static readonly string TYPES_URL = BASEADDRESS + "types/";
        public static readonly string USERS_URL = BASEADDRESS + "users/";

        public static readonly string BOOK_EXTRA = "_BOOK_EXTRA";

        public static RestManager ServiceManager { get; private set; }
        public static int UserId { get; set; }
        public static int StudentId { get; set; }
        public static int RoleId { get; set; }
        public static string Token { get; set; }
    }
}