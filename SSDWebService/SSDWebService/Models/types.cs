using SQLite;

namespace SSDWebService.Models
{
    public class Types
    {
        [PrimaryKey]
        public string TypeId { get; set; }
        public string Name { get; set; }
    }
}