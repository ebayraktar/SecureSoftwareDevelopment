using SQLite;

namespace SSDWebService.Models
{
    public class roles
    {
        [PrimaryKey]
        public string roleId { get; set; }
        public string name { get; set; }
    }
}