using SQLite;

namespace SSDWebService.Models
{
    public class books
    {
        [PrimaryKey]
        [Unique]
        public string bookId { get; set; }
        public string name { get; set; }
        public string pagecount { get; set; }
        public string point { get; set; }
        public string authorId { get; set; }
        public string typeId { get; set; }
    }
}