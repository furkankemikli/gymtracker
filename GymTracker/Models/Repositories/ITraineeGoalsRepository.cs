using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface ITraineeGoalsRepository
    {
        TraineeGoals GetTraineeGoals(string TraineeId);

        void NewGoalInsert();
    }
}
