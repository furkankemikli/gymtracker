using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class Trainee
    {
        public string TraineeId { get; set; }
        public string TrainerId { get; set; }
        public DateTime? Birthday { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string Gender { get; set; }
        public double? FatRatio { get; set; }
        public DateTime EntryDate { get; set; }

        public ApplicationUser TraineeNavigation { get; set; }
        public ApplicationUser Trainer { get; set; }
    }
}
