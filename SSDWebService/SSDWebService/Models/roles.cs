using SQLite;

namespace SSDWebService.Models
{
    public class Roles
    {
        [PrimaryKey]
        public string RoleId { get; set; }
        public string Name { get; set; }
    }
}