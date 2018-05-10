using System;
using System.Collections.Generic;

namespace GymTracker.Models
{
    public partial class TraineeMeasurements
    {
        public int MeasurementId { get; set; }
        public string TraineeId { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int FatRatio { get; set; }
        public DateTime Date { get; set; }

        public ApplicationUser Trainee { get; set; }
    }
}
