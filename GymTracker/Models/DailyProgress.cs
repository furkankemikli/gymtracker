using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class DailyProgress
    {
        public int ProgressId { get; set; }
        public string TraineeId { get; set; }
        public int ExerciseId { get; set; }
        public double AssignedSets { get; set; }
        public double CompletedSets { get; set; }
        public DateTime Date { get; set; }

        public Exercise Exercise { get; set; }
        public ApplicationUser Trainee { get; set; }
    }
}
