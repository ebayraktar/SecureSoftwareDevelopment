using SQLite;

namespace SSDWebService.Models
{
    public class Students
    {
        [PrimaryKey]
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Birthdate { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public string Point { get; set; }
    }
}