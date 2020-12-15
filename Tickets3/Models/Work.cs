using SQLite;
using System;

namespace Tickets3.Models
{
    public class Work
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ClientName { get; set; }
        public int ClientID { get; set; }
        public string StaffName { get; set; }
        public int StaffID { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan Total
        {
            get => End - Start;
        }

    }
}
