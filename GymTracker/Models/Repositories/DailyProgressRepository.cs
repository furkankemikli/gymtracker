using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class DailyProgressRepository : IDailyProgressRepository
    {
        private readonly Aspnet_GymTrackerContext _aspnetGymTrackerContext;

        public DailyProgressRepository(Aspnet_GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<DailyProgress> DailyProgresses(string traineeId)
        {
            return _aspnetGymTrackerContext.DailyProgress.Where(f => f.TraineeId == traineeId).ToList();
        }

    }
}
