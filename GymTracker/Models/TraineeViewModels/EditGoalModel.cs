using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.TraineeViewModels
{
    public class EditGoalModel
    {
        public string Id { get; set; }

        public double GoalWeight { get; set; }

        public double GoalFatRatio { get; set; }

        public DateTime GoalDate { get; set; }

    }
}
