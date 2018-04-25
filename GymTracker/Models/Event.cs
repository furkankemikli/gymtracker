using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string TrainerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ApplicationUser Trainer { get; set; }
    }
}
