using SQLite;

namespace SSDWebService.Models
{
    public class users
    {
        [PrimaryKey]
        public string userId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}