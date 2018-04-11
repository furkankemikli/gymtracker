using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class DailyRoutine
    {
        public int RoutineId { get; set; }
        public string TraineeId { get; set; }
        public int ExerciseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Interval { get; set; }
        public double Sets { get; set; }

        public Exercise Exercise { get; set; }
        public ApplicationUser Trainee { get; set; }
    }
}
