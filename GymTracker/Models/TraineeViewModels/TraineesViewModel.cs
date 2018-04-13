using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class TraineesViewModel
    {
        public IEnumerable<ApplicationUser> Trainees { get; set; }
    }
}
