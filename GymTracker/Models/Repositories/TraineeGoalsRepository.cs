using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class TraineeGoalsRepository : ITraineeGoalsRepository
    {
        private readonly Aspnet_GymTrackerContext _aspnetGymTrackerContext;

        public TraineeGoalsRepository(Aspnet_GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public TraineeGoals GetTraineeGoals(string traineeId)
        {
            return _aspnetGymTrackerContext.TraineeGoals.FirstOrDefault(d => d.TraineeId == traineeId);
        }

        public void CreateGoal(TraineeGoals traineeGoals)
        {
            _aspnetGymTrackerContext.TraineeGoals.Add(traineeGoals);

            _aspnetGymTrackerContext.SaveChanges();
        }

        public void UpdateGoal(TraineeGoals traineeGoals)
        {
            var result = _aspnetGymTrackerContext.TraineeGoals.SingleOrDefault(b => b.TraineeId == traineeGoals.TraineeId);
            if (result != null)
            {
                result.Weight = traineeGoals.Weight;
                result.FatRatio = traineeGoals.FatRatio;
                result.ByDate = traineeGoals.ByDate;
                _aspnetGymTrackerContext.SaveChanges();
            }
            else
            {
                CreateGoal(traineeGoals);
            }
        }
    }
}
