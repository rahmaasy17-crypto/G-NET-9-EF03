using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Entitys
{
    public class Event
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }// optional

        public int MaxAttendees { get; set; }
        public Organizer Organizer { get; set; } //1-m with Organizer

        public int OrganizerId { get; set; }//FK




        public int? ParentEventId { get; set; }
        public Event ParentEvent { get; set; }
        public ICollection<Event> Sessions { get; set; } = new List<Event>(); //self relationship

        public ICollection<Registration> Registrations { get; set; } = new HashSet<Registration>();


    }
}
