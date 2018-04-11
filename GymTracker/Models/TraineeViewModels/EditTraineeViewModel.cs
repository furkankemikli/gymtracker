using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class EditTraineeViewModel
    {
        public Trainee Trainee { get; set; }

        public ApplicationUser TraineePersonalInfo { get; set; }
    }
}
