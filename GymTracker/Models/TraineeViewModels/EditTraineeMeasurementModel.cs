using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class EditTraineeMeasurementModel
    {
        public string TraineeId { get; set; }

        public int MeasurementId { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int FatRatio { get; set; }
    }
}
