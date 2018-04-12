using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class TraineeDetailsPageViewModel
    {
        //to reach trainee info
        public string Name { get; set; }

        public string Surame { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public byte[] Image { get; set; }

        public string Phone { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double FatRatio { get; set; }

        public string City { get; set; }

        public string Gender { get; set; }

        //to list done exercises with percentage
        public IEnumerable<DailyProgress> DailyProgresses { get; set; }

        //to list exercises assigned to the trainee(current and passed 15 days after since enddate)
        public IEnumerable<DailyRoutine> DailyRoutines { get; set; }

        //for listing them to assign to the trainee
        public IEnumerable<Exercise> Exercises { get; set; }

        //to show and edit trainee goals
        public double GoalWeight { get; set; }

        public double GoalFatRatio { get; set; }

        public DateTime GoalDate { get; set; }

    }
}
