using SQLite;

namespace SSDWebService.Models
{
    public class Users
    {
        [PrimaryKey]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}