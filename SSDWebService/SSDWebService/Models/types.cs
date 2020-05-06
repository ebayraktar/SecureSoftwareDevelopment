using SQLite;

namespace SSDWebService.Models
{
    public class types
    {
        [PrimaryKey]
        public string typeId { get; set; }
        public string name { get; set; }
    }
}