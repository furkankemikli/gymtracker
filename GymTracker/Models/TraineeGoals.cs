using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class TraineeGoals
    {
        public int GoalId { get; set; }
        public string TraineeId { get; set; }
        public double? Weight { get; set; }
        public double? FatRatio { get; set; }
        public DateTime? ByDate { get; set; }

        public ApplicationUser Trainee { get; set; }
    }
}
