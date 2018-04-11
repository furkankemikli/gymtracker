using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class TraineeDetailsPageViewModel
    {
        //to reach trainee info
        public Trainee Trainee { get; set; }

        //to list done exercises with percentage
        public IEnumerable<DailyProgress> DailyProgresses { get; set; }

        //to list exercises assigned to the trainee(current and passed 15 days after since enddate)
        public IEnumerable<DailyRoutine> DailyRoutines { get; set; }

        //for listing them to assign to the trainee
        public IEnumerable<Exercise> Exercises { get; set; }

        //to show and edit trainee goals
        public TraineeGoals TraineeGoals { get; set; }

    }
}
