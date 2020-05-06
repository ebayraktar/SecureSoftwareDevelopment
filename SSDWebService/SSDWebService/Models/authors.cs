using SQLite;

namespace SSDWebService.Models
{
    public class authors
    {
        [PrimaryKey]
        public string authorId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}