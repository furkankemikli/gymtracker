using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class DailyRoutineRepository : IDailyRoutineRepository
    {
        private readonly Aspnet_GymTrackerContext _aspnetGymTrackerContext;

        public DailyRoutineRepository(Aspnet_GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<DailyRoutine> DailyRoutines(string traineeId)
        {
            return _aspnetGymTrackerContext.DailyRoutine.Where(f => f.TraineeId == traineeId).ToList();
        }

        public void CreateDailyRoutine(DailyRoutine dailyRoutine)
        {
            _aspnetGymTrackerContext.DailyRoutine.Add(dailyRoutine);

            _aspnetGymTrackerContext.SaveChanges();
        }

        public void UpdateDailyRoutine(DailyRoutine dailyRoutine)
        {
            var result = _aspnetGymTrackerContext.DailyRoutine.SingleOrDefault(b => b.RoutineId == dailyRoutine.RoutineId);
            if (result != null)
            {
                result.StartDate = dailyRoutine.StartDate;
                result.EndDate = dailyRoutine.EndDate;
                result.Interval = dailyRoutine.Interval;
                result.Sets = dailyRoutine.Sets;
                _aspnetGymTrackerContext.SaveChanges();
            }
        }

        public void DeleteDailyRoutine(int routineId)
        {
            _aspnetGymTrackerContext.DailyRoutine.Remove(_aspnetGymTrackerContext.DailyRoutine.Where(c => c.RoutineId == routineId).FirstOrDefault());
            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
