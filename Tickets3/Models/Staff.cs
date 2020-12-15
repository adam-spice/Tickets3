using SQLite;

namespace Tickets3.Models
{
    public class Staff
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
