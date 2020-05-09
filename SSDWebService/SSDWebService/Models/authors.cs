using SQLite;

namespace SSDWebService.Models
{
    public class Authors
    {
        [PrimaryKey]
        public string AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}