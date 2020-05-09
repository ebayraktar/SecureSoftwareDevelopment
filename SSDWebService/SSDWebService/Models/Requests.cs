using SQLite;

namespace SSDWebService.Models
{
    public class Requests
    {
        [PrimaryKey]
        public int RequestId { get; set; }
        public int StudentId { get; set; }
        public int BookId { get; set; }
        public string RequestDate { get; set; }
        public int Statu { get; set; }
    }
}