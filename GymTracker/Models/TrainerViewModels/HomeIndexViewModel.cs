using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TrainerViewModels
{
    public class HomeIndexViewModel
    {
        //for new event adding and update
        public int EventId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string StrStartDate { get; set; }

        public string StrEndDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        //To list events
        public List<Event> Events { get; set; }

        public string jsonEvents { get; set; }

        public List<ApplicationUser> TraineeList { get; set;}

    }
}
