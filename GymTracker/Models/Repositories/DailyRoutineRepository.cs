using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class DailyRoutineRepository : IDailyRoutineRepository
    {
        private readonly GymTrackerContext _aspnetGymTrackerContext;

        public DailyRoutineRepository(GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<DailyRoutine> DailyRoutines(string traineeId)
        {
            IEnumerable<DailyRoutine> result =  _aspnetGymTrackerContext.DailyRoutine.Where(f => f.TraineeId == traineeId && f.Status != "passive").ToList();
            foreach(DailyRoutine routine in result){
                routine.Exercise = _aspnetGymTrackerContext.Exercise.Select(e => new Exercise(e.ExerciseId, e.Name, e.Category, e.CalorieBySet) { ExerciseId = e.ExerciseId, Name = e.Name, Category = e.Category, CalorieBySet = e.CalorieBySet }).Where(e => e.ExerciseId == routine.ExerciseId).FirstOrDefault();
            }
            return result;
        }

        public int CreateDailyRoutine(DailyRoutine dailyRoutine)
        {
            _aspnetGymTrackerContext.DailyRoutine.Add(dailyRoutine);

            _aspnetGymTrackerContext.SaveChanges();

            return dailyRoutine.RoutineId;

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
            _aspnetGymTrackerContext.DailyProgress.RemoveRange(_aspnetGymTrackerContext.DailyProgress.Where(c => c.RoutineId == routineId && c.Date >= DateTime.Today));
            var result = _aspnetGymTrackerContext.DailyRoutine.SingleOrDefault(b => b.RoutineId == routineId);
            if(result != null)
            {
                result.Status = "passive";
            }
            _aspnetGymTrackerContext.SaveChanges();
        }
    }
}
