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

        public string Description { get; set; 

        public string Location { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
