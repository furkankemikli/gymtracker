using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTracker.Models.Repositories
{
    public interface IDailyProgressRepository
    {
        IEnumerable<DailyProgress> DailyProgresses(string traineeId);

        void CreateDailyProgress(DailyRoutine dailyRoutine);

        void UpdateDailyProgress(DailyRoutine dailyRoutine);
    }
}
