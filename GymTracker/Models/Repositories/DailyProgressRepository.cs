using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public class DailyProgressRepository : IDailyProgressRepository
    {
        private readonly GymTrackerContext _aspnetGymTrackerContext;

        public DailyProgressRepository(GymTrackerContext aspnet_GymTrackerContext)
        {
            _aspnetGymTrackerContext = aspnet_GymTrackerContext;
        }

        public IEnumerable<DailyProgress> DailyProgresses(string traineeId)
        {
            IEnumerable<DailyProgress> result = _aspnetGymTrackerContext.DailyProgress.Where(f => f.TraineeId == traineeId && f.Date < DateTime.Today).OrderByDescending(f => f.Date).ToList();
            foreach (DailyProgress progress in result)
            {
                progress.Exercise = _aspnetGymTrackerContext.Exercise.Select(e => new Exercise(e.ExerciseId, e.Name, e.Category, e.CalorieBySet) { ExerciseId = e.ExerciseId, Name = e.Name, Category = e.Category, CalorieBySet = e.CalorieBySet }).Where(e => e.ExerciseId == progress.ExerciseId).FirstOrDefault();
            }
            return result;
        }

        public void CreateDailyProgress(DailyRoutine dailyRoutine)
        {
            DateTime date = dailyRoutine.StartDate;
            while (date < DateTime.Today)
                date = date.AddDays(dailyRoutine.Interval);
            while (date <= dailyRoutine.EndDate)
            {
                DailyProgress dailyProgress = new DailyProgress
                {
                    ExerciseId = dailyRoutine.ExerciseId,
                    RoutineId = dailyRoutine.RoutineId,
                    AssignedSets = dailyRoutine.Sets,
                    Date = date,
                    TraineeId = dailyRoutine.TraineeId
                };
                _aspnetGymTrackerContext.DailyProgress.Add(dailyProgress);
                date = date.AddDays(dailyRoutine.Interval);
            }

            _aspnetGymTrackerContext.SaveChanges();
        }

        public void UpdateDailyProgress(DailyRoutine dailyRoutine)
        {
            _aspnetGymTrackerContext.DailyProgress.RemoveRange(_aspnetGymTrackerContext.DailyProgress.Where(c => c.RoutineId == dailyRoutine.RoutineId && c.Date >= DateTime.Today));

            _aspnetGymTrackerContext.SaveChanges();

            CreateDailyProgress(dailyRoutine);
        }

    }
}
