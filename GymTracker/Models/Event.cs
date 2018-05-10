using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class Event
    {
        public Event()
        {
            InverseHolderEvent = new HashSet<Event>();
        }

        public int EventId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ApporavalStatus { get; set; }
        public int? HolderEventId { get; set; }

        public Event HolderEvent { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Event> InverseHolderEvent { get; set; }
    }
}
