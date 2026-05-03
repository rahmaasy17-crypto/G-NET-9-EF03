using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entitys
{
    public class Registration  // from m-m relationship
    {
        public int AttendeeId { get; set; }
        public Attendee Attendee { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public string? Note { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
