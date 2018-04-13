using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TrainerViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Event> Events { get; set; }

        //for new event adding
        public string Name { get; set; }

        public string Description { get; set; } 

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //editing event
        public int EventId { get; }

        public string EditName { get; set; }

        public string EditDescription { get; set; }

        public string EditLocation { get; set; }

        public DateTime EditStartDate { get; set; }

        public DateTime EditEndDate { get; set; }

    }
}
