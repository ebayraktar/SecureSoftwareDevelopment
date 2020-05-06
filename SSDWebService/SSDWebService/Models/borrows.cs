using SQLite;

namespace SSDWebService.Models
{
    public class borrows
    {
        [PrimaryKey]
        public string borrowId { get; set; }
        public string studentId { get; set; }
        public string bookId { get; set; }
        public string takenDate { get; set; }
        public string broughtDate { get; set; }
    }
}