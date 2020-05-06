using SQLite;

namespace SSDWebService.Models
{
    public class students
    {
        [PrimaryKey]
        public string studentId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthdate { get; set; }
        public string gender { get; set; }
        public string @class { get; set; }
        public string point { get; set; }
    }
}