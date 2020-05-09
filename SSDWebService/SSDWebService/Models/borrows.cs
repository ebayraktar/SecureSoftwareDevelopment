using SQLite;

namespace SSDWebService.Models
{
    public class Borrows
    {
        [PrimaryKey]
        public string BorrowId { get; set; }
        public string StudentId { get; set; }
        public string BookId { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
    }
}